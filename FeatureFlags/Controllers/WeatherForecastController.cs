using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlags.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFeatureManager _featureManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            var isRainEnabled = await _featureManager.IsEnabledAsync("RainEnabled");
            return  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                DateTime.Now.AddDays(index),
                Random.Shared.Next(-20, 55),
                isRainEnabled ? $"{ Random.Shared.Next(0,100)}%" : null,  
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
           
        }


        [HttpGet("advanced")]
        [FeatureGate("AdvancedEnabled")]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAdvanced()
        {
            var useNewAlgorithm = await _featureManager.IsEnabledAsync("NewAlgorithmEnabled");
            return useNewAlgorithm 
                ? await NewAlgorithm() 
                : await OldAlgorithm();

        }

        private async Task<IEnumerable<WeatherForecast>> OldAlgorithm()
        {
            var isRainEnabled = await _featureManager.IsEnabledAsync("RainEnabled");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                DateTime.Now.AddDays(index),
                Random.Shared.Next(-20, 55),
                isRainEnabled ? $"{Random.Shared.Next(0, 100)}% OLD" : null,
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
        }


        private async Task<IEnumerable<WeatherForecast>> NewAlgorithm()
        {
            var isRainEnabled = await _featureManager.IsEnabledAsync("RainEnabled");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                DateTime.Now.AddDays(index),
                Random.Shared.Next(-20, 55),
                isRainEnabled ? $"{Random.Shared.Next(0, 100)}%" : null,
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
        }

    }
}
