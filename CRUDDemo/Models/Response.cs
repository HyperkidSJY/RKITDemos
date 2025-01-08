namespace CRUDDemo.Models
{
    /// <summary>
    /// Represents a response object used for communication between client and server.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the data to be returned in the response.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response contains an error.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or sets the message that provides additional information about the response.
        /// </summary>
        public string Message { get; set; }
    }
}
