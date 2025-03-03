namespace ActionMethods
{
    /// <summary>
    /// Represents a weather forecast for a specific date.
    /// </summary>
    public class WeatherForecast
    {
        #region Properties
        /// <summary>
        /// Gets or sets the date of the weather forecast.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets the temperature in Fahrenheit based on the Celsius temperature.
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets the summary or description of the weather.
        /// </summary>
        public string? Summary { get; set; }
        #endregion
    }
}
