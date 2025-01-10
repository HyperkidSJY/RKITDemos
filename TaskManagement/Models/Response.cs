namespace TaskManagement.Models
{
    /// <summary>
    /// Represents a standard response structure used for communication between the service layer and clients.
    /// </summary>
    public class Response
    {
        #region Properties

        /// <summary>
        /// Gets or sets the dynamic data returned in the response.
        /// The data can be of any type, depending on the context.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets the message providing additional information about the response.
        /// This can indicate success, failure, or additional details.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response indicates an error.
        /// A value of true indicates an error, while false indicates success.
        /// </summary>
        public bool IsError { get; set; }

        #endregion
    }
}
