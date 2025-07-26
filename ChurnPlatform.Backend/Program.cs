using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using ChurnPlatform.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add CORS policy
var allowedOrigins = builder.Configuration["CORS_ORIGINS"] ?? "http://localhost:3000";
var origins = allowedOrigins.Split(';');

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            // This is a temporary hardcoded value for debugging
            policy.WithOrigins("https://churnstorage1175.z19.web.core.windows.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Get the connection string from environment variables
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add the DbContext to the services container
builder.Services.AddDbContext<PredictionContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Register our example providers
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseRouting();

// 2. Enable the CORS policy
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();