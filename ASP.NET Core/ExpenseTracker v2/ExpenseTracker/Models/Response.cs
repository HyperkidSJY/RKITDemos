namespace ExpenseTracker.Models
{
    /// <summary>
    /// Represents a response object that contains data, a message, and an error flag.
    /// </summary>
    public class Response
    {
        #region Properties

        /// <summary>
        /// Gets or sets the dynamic data associated with the response.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response represents an error.
        /// </summary>
        public bool IsError { get; set; }

        #endregion
    }
}
