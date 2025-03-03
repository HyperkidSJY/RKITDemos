using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            // Set the result for the response
            context.Result = new ObjectResult(new
            {
                message = "An unexpected error occurred. Please try again later.",
                details = context.Exception.Message
            })
            {
                StatusCode = 500 // HTTP Status Code for Internal Server Error
            };
        }
    }
}
