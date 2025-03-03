using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Filters.Filters
{
    /// <summary>
    /// Custom attribute to apply an action constraint based on the user agent in the request header.
    /// This constraint checks if the request is coming from a mobile device (Android or iPhone).
    /// </summary>
    public class IsMobileAttribute : Attribute, IActionConstraint
    {
        /// <summary>
        /// Gets the order of execution for the action constraint. This is useful when multiple constraints are applied.
        /// </summary>
        public int Order => 0; // The order of the constraint. Lower values are executed first.

        #region Accept Method
        /// <summary>
        /// Determines whether the action should be selected based on the request's user-agent.
        /// </summary>
        /// <param name="context">The context of the current action constraint execution.</param>
        /// <returns>True if the request is from a mobile device, otherwise false.</returns>
        public bool Accept(ActionConstraintContext context)
        {
            // Check if the User-Agent header contains "Android" or "iPhone"
            return context.RouteContext.HttpContext.Request.Headers["User-Agent"]
                    .Any(agent => agent.Contains("Android") || agent.Contains("iPhone"));
        }
        #endregion
    }
}
