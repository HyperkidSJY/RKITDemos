using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    /// <summary>
    /// Custom action filter that executes before and after an action method is called.
    /// It allows you to log or modify the action context before and after an action method executes.
    /// </summary>
    public class ActionFilter : IActionFilter
    {
        private readonly ILogger<ActionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger to log information about the action methods.</param>
        public ActionFilter(ILogger<ActionFilter> logger)
        {
            _logger = logger; // Store the logger instance.
        }

        #region OnActionExecuted Method
        /// <summary>
        /// This method is invoked after the action method is executed.
        /// It is useful for tasks such as logging the execution of the action.
        /// </summary>
        /// <param name="context">The context for the action that was executed.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Log that the action method has executed.
            _logger.LogInformation("After executing action method: {Action}", context.ActionDescriptor.DisplayName);
        }
        #endregion

        #region OnActionExecuting Method
        /// <summary>
        /// This method is invoked before the action method is executed.
        /// It is useful for tasks such as logging or modifying the context before the action runs.
        /// </summary>
        /// <param name="context">The context for the action method that is about to execute.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Log that the action method is about to execute.
            _logger.LogInformation("Before executing action method: {Action}", context.ActionDescriptor.DisplayName);
        }
        #endregion
    }
}
//It allows you to log, modify, or intercept the action context before and after an 
//action method runs.