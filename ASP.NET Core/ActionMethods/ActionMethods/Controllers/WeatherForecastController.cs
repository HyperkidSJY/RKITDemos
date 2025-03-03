using Microsoft.AspNetCore.Mvc;

namespace ActionMethods.Controllers
{
    /// <summary>
    /// Controller that handles weather forecast-related API requests.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Sample weather data for testing
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        #region Action Method 1: GetOk (200 OK)
        /// <summary>
        /// Retrieves a list of sample weather forecasts with a 200 OK response.
        /// </summary>
        /// <returns>An ActionResult containing a list of weather forecasts.</returns>
        [HttpGet("ok")]
        public ActionResult<IEnumerable<WeatherForecast>> GetOk()
        {
            var forecasts = new[]
            {
                new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" },
                new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), TemperatureC = 28, Summary = "Hot" }
            };

            return Ok(forecasts);  // 200 OK
        }
        #endregion

        #region Action Method 2: GetNotFound (404 Not Found)
        /// <summary>
        /// Retrieves a weather forecast by ID, returning a 404 Not Found if the forecast doesn't exist.
        /// </summary>
        /// <param name="id">The ID of the forecast to retrieve.</param>
        /// <returns>An ActionResult containing the weather forecast or a 404 Not Found response.</returns>
        [HttpGet("notfound/{id}")]
        public ActionResult<WeatherForecast> GetNotFound(int id)
        {
            var forecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" };

            if (id != 1)  // Simulate resource not found for id != 1
            {
                return NotFound("Weather forecast not found for the given ID");  // 404 Not Found
            }

            return Ok(forecast);  // 200 OK
        }
        #endregion

        #region Action Method 3: CreateBadRequest (400 Bad Request)
        /// <summary>
        /// Creates a weather forecast, returning a 400 Bad Request if the provided data is invalid.
        /// </summary>
        /// <param name="forecast">The weather forecast data to create.</param>
        /// <returns>An ActionResult indicating the result of the creation process.</returns>
        [HttpPost("badrequest")]
        public ActionResult CreateBadRequest([FromBody] WeatherForecast forecast)
        {
            if (forecast == null || string.IsNullOrEmpty(forecast.Summary))
            {
                return BadRequest("Invalid data provided for the forecast.");  // 400 Bad Request
            }

            // Simulate saving the data to a database
            return CreatedAtAction(nameof(GetOk), new { id = 1 }, forecast);  // 201 Created
        }
        #endregion

        #region Action Method 4: Delete (204 No Content)
        /// <summary>
        /// Deletes a weather forecast by ID, returning a 204 No Content if successful.
        /// </summary>
        /// <param name="id">The ID of the forecast to delete.</param>
        /// <returns>An ActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id != 1)  // Simulate resource not found for id != 1
            {
                return NotFound();  // 404 Not Found
            }

            // Simulate deleting the resource
            return NoContent();  // 204 No Content (no body is returned)
        }
        #endregion

        #region Action Method 5: GetJsonResult (Returning Raw JSON)
        /// <summary>
        /// Retrieves weather data as raw JSON.
        /// </summary>
        /// <returns>A JsonResult containing a list of weather forecasts.</returns>
        [HttpGet("jsonresult")]
        public JsonResult GetJsonResult()
        {
            var weatherData = new[]
            {
                new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 30, Summary = "Hot" },
                new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), TemperatureC = 25, Summary = "Warm" }
            };

            return new JsonResult(weatherData);  // Returns the data as raw JSON
        }
        #endregion

        #region Action Method 6: GetUnauthorized (401 Unauthorized)
        /// <summary>
        /// Returns a 401 Unauthorized response indicating the user is not authorized.
        /// </summary>
        /// <returns>An IActionResult indicating unauthorized access.</returns>
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized("You are not authorized to access this resource.");  // 401 Unauthorized
        }
        #endregion

        #region Action Method 7: GetForbidden (403 Forbidden)
        /// <summary>
        /// Returns a 403 Forbidden response, indicating the user does not have permission.
        /// </summary>
        /// <returns>An IActionResult indicating forbidden access.</returns>
        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {
            return Forbid();  // 403 Forbidden
        }
        #endregion

        #region Action Method 8: Create (201 Created)
        /// <summary>
        /// Creates a new weather forecast, returning a 201 Created response with the location of the new resource.
        /// </summary>
        /// <param name="forecast">The weather forecast data to create.</param>
        /// <returns>An ActionResult containing the created weather forecast.</returns>
        [HttpPost("create")]
        public ActionResult<WeatherForecast> Create([FromBody] WeatherForecast forecast)
        {
            if (forecast == null || string.IsNullOrEmpty(forecast.Summary))
            {
                return BadRequest("Invalid forecast data provided.");
            }

            // Simulate saving the forecast
            var newForecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 22, Summary = forecast.Summary };

            // Returns 201 Created along with the location of the newly created resource
            return CreatedAtAction(nameof(GetOk), new { id = 1 }, newForecast);
        }
        #endregion
    }
}
