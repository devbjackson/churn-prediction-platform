using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using ChurnPlatform.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

app.MapControllers();

app.Run();