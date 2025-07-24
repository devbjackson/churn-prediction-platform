// Controllers/PredictionController.cs
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ChurnPlatform.Backend.DTOs;
using Swashbuckle.AspNetCore.Filters;
using ChurnPlatform.Backend.Data; // <-- Add this
using ChurnPlatform.Backend.Data.Models; // <-- Add this
using System;

namespace ChurnPlatform.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PredictionContext _context; // <-- Add DbContext field

        // Inject both the HttpClientFactory and the PredictionContext
        public PredictionController(IHttpClientFactory httpClientFactory, PredictionContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(CustomerDataDto), typeof(SwaggerExamples.CustomerDataRequestExample))]
        public async Task<IActionResult> GetChurnPrediction([FromBody] CustomerDataDto customerData)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var pythonApiUrl = "http://python-api/predict";

            var jsonContent = JsonSerializer.Serialize(customerData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(pythonApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var predictionResult = await response.Content.ReadFromJsonAsync<JsonElement>();

                    // --- START: SAVE TO DATABASE ---
                    var log = new PredictionLog
                    {
                        Timestamp = DateTime.UtcNow,
                        InputPayload = jsonContent,
                        PredictedResult = predictionResult.GetProperty("prediction").GetString(),
                        ChurnProbability = predictionResult.GetProperty("churn_probability").GetDouble()
                    };

                    _context.PredictionLogs.Add(log);
                    await _context.SaveChangesAsync();
                    // --- END: SAVE TO DATABASE ---

                    return Ok(predictionResult);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, new { error = "ML service is unavailable.", details = ex.Message });
            }
        }
    }
}