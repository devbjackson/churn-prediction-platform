using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ChurnPlatform.Backend.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PredictionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> GetChurnPrediction([FromBody] JsonElement customerData)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var pythonApiUrl = "http://python-api/predict";
            var content = new StringContent(customerData.ToString(), Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(pythonApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var predictionResult = await response.Content.ReadFromJsonAsync<object>();
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