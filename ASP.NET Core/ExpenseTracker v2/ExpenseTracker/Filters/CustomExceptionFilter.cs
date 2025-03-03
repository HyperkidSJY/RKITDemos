using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseTracker.Filters
{
    /// <summary>
    /// Custom exception filter to catch unhandled exceptions and log them.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        /// <summary>
        /// Constructor to initialize the logger.
        /// </summary>
        /// <param name="logger">Logger to log the exception details.</param>
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called when an unhandled exception occurs. Logs the exception and sets the response result.
        /// </summary>
        /// <param name="context">The context containing information about the exception.</param>
        public void OnException(ExceptionContext context)
        {
            // Log the exception details to the configured logging provider.
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            // Create a response with a generic error message and the exception details.
            context.Result = new ObjectResult(new
            {
                message = "An unexpected error occurred. Please try again later.",
                details = context.Exception.Message // Include the exception message for debugging (ensure to sanitize in production)
            })
            {
                StatusCode = 500 // HTTP Status Code for Internal Server Error
            };
        }
    }
}
