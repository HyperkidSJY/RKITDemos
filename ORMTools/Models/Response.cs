namespace ORMTools.Models
{
    #region "Response Model"

    /// <summary>
    /// Represents the response object used for API results.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful or resulted in an error.
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        /// Gets or sets the message related to the response, typically used for error or status descriptions.
        /// </summary>
        public string Message { get; set; }
    }

    #endregion "Response Model"
}
