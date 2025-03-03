using Filters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{
    /// <summary>
    /// The WeatherForecastController provides API endpoints for retrieving weather forecasts.
    /// It demonstrates the use of various filters like Authorization, Action, Result, 
    /// Exception, and Resource filters applied globally.
    /// </summary>
    [ApiController]
    [Route("[controller]")]

    #region Applying Filters
    [ServiceFilter(typeof(AuthorizationFilter))] // Applying Authorization Filter globally
    [ServiceFilter(typeof(ActionFilter))] // Applying Action Filter globally
    [ServiceFilter(typeof(ResultFilter))] // Applying Result Filter globally
    [ServiceFilter(typeof(ExceptionFilter))] // Applying Exception Filter globally
    [ServiceFilter(typeof(ResourceFilter))] // Applying Resource Filter globally
    #endregion

    public class WeatherForecastController : ControllerBase
    {
        // Sample weather data for testing
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initializes a new instance of the WeatherForecastController.
        /// </summary>
        /// <param name="logger">The logger to log information during execution.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _logger.LogInformation("WeatherForecastController Started");
        }

        #region Weather Forecast Action
        /// <summary>
        /// Gets a collection of weather forecasts.
        /// The method generates weather forecasts for the next 5 days.
        /// </summary>
        /// <returns>An array of weather forecasts.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // You can uncomment the following line to test the Exception filter.
            // throw new Exception("Something went wrong!");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        #endregion

        #region Logger Action
        /// <summary>
        /// A simple action to demonstrate logging.
        /// </summary>
        /// <returns>Returns an OK response.</returns>
        [HttpGet]
        [Route("logger")]
        public IActionResult GetLogger()
        {
            _logger.LogInformation("Executing WeatherForecastController.GetLogger Method");
            return Ok();
        }
        #endregion
    }
}
