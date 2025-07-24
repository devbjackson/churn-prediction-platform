using ChurnPlatform.Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChurnPlatform.Backend.Data;

public class PredictionContext : DbContext
{
    public PredictionContext(DbContextOptions<PredictionContext> options) : base(options)
    {
    }

    public DbSet<PredictionLog> PredictionLogs { get; set; }
} 