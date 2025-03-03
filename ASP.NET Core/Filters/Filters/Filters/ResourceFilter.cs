using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    /// <summary>
    /// A custom resource filter that logs information before and after the action is executed.
    /// Resource filters are executed before and after the controller's action method is called, 
    /// but before the result is returned to the client.
    /// </summary>
    public class ResourceFilter : IResourceFilter
    {
        private readonly ILogger<ResourceFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger used to log messages related to resource filter execution.</param>
        public ResourceFilter(ILogger<ResourceFilter> logger)
        {
            _logger = logger;
        }

        #region OnResourceExecuting Method
        /// <summary>
        /// This method is executed before the action executes.
        /// It runs when a request is about to be processed by an action method.
        /// </summary>
        /// <param name="context">Provides information about the current resource execution context.</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Log a message before the action method is called.
            _logger.LogInformation("OnResourceExecuting: Executing resource filter before the action executes.");
        }
        #endregion

        #region OnResourceExecuted Method
        /// <summary>
        /// This method is executed after the action executes but before the result is returned to the client.
        /// It runs after the action method is called, but before the response is generated.
        /// </summary>
        /// <param name="context">Provides information about the current resource execution context after the action executes.</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Log a message after the action method has been executed.
            _logger.LogInformation("OnResourceExecuted: Executed resource filter after the action.");
        }
        #endregion
    }
}

//The Resource Filter is ideal for situations where you need 
//to handle tasks before and after an action is executed 
//but before the result is sent to the client. It’s helpful 
//for cross-cutting concerns like logging, authorization, or resource 
//initialization that need to run early in the request lifecycle.