using System.ComponentModel.DataAnnotations;

namespace ChurnPlatform.Backend.Data.Models;

public class PredictionLog
{
    [Key]
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    [Required]
    public string InputPayload { get; set; }

    [Required]
    public string PredictedResult { get; set; }

    public double ChurnProbability { get; set; }
}