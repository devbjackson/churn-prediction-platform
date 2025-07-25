using System.Text.Json.Serialization;

namespace ChurnPlatform.Backend.DTOs;

public class CustomerDataDto
{
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("SeniorCitizen")]
    public int SeniorCitizen { get; set; }

    [JsonPropertyName("Partner")]
    public string Partner { get; set; }

    [JsonPropertyName("Dependents")]
    public string Dependents { get; set; }

    [JsonPropertyName("tenure")]
    public int Tenure { get; set; }

    [JsonPropertyName("PhoneService")]
    public string PhoneService { get; set; }

    [JsonPropertyName("MultipleLines")]
    public string MultipleLines { get; set; }

    [JsonPropertyName("InternetService")]
    public string InternetService { get; set; }

    [JsonPropertyName("OnlineSecurity")]
    public string OnlineSecurity { get; set; }

    [JsonPropertyName("OnlineBackup")]
    public string OnlineBackup { get; set; }

    [JsonPropertyName("DeviceProtection")]
    public string DeviceProtection { get; set; }

    [JsonPropertyName("TechSupport")]
    public string TechSupport { get; set; }

    [JsonPropertyName("StreamingTV")]
    public string StreamingTV { get; set; }

    [JsonPropertyName("StreamingMovies")]
    public string StreamingMovies { get; set; }

    [JsonPropertyName("Contract")]
    public string Contract { get; set; }

    [JsonPropertyName("PaperlessBilling")]
    public string PaperlessBilling { get; set; }

    [JsonPropertyName("PaymentMethod")]
    public string PaymentMethod { get; set; }

    [JsonPropertyName("MonthlyCharges")]
    public decimal MonthlyCharges { get; set; }

    [JsonPropertyName("TotalCharges")]
    public decimal TotalCharges { get; set; }
}