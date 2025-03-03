using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    /// <summary>
    /// Custom exception filter to handle unhandled exceptions in the application.
    /// This filter catches exceptions thrown in action methods and provides a custom response.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        #region OnException Method
        /// <summary>
        /// This method is invoked when an exception occurs in an action method.
        /// It allows you to customize the response sent to the client when an unhandled exception is thrown.
        /// </summary>
        /// <param name="context">The exception context, which contains information about the exception.</param>
        public void OnException(ExceptionContext context)
        {
            // Create a custom response with a message and the exception message.
            context.Result = new ObjectResult(new
            {
                message = "An unexpected error occurred.",   // A generic message indicating an error occurred.
                error = context.Exception.Message           // The actual exception message to help with debugging.
            })
            {
                StatusCode = 500 // Set the status code to 500 (Internal Server Error).
            };

            // Optionally, you can log the exception here or perform other actions.
        }
        #endregion
    }
}
