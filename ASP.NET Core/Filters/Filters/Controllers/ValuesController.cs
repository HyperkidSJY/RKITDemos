using Filters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{
    /// <summary>
    /// The ValuesController provides endpoints for retrieving values from both web and mobile clients.
    /// The controller demonstrates the use of action constraints (e.g., <see cref="IsMobile"/>).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        #region Default Action
        /// <summary>
        /// The default action for retrieving a value.
        /// This endpoint returns a simple string response for all clients (web).
        /// </summary>
        /// <returns>A string indicating the default index for web clients.</returns>
        [HttpGet]
        public string Index()
        {
            return "Index from web";
        }
        #endregion

        #region Mobile Action
        /// <summary>
        /// This action is used for retrieving a value specifically for mobile clients.
        /// The <see cref="IsMobile"/> action constraint ensures that only requests from mobile devices 
        /// (e.g., Android, iPhone) can access this endpoint.
        /// </summary>
        /// <returns>A string indicating the index for mobile clients.</returns>
        [HttpGet("mobile")]
        [ActionName("Index")]  // Action method name is set as "Index" for consistency
        [IsMobile]  // Custom action constraint to handle mobile requests
        public string IndexMobile()
        {
            return "Index from mobile";
        }
        #endregion
    }
}
