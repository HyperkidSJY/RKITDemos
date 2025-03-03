using Microsoft.AspNetCore.Mvc;
using ServiceLifetime.Interface;

namespace ServiceLifetime.Controllers
{
    /// <summary>
    /// Controller responsible for handling product-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        #region GetName
        /// <summary>
        /// Retrieves the name from the product service.
        /// </summary>
        /// <param name="_productService">The product service injected via dependency injection.</param>
        /// <returns>Returns the name from the product service.</returns>
        [HttpGet("name")]
        public IActionResult GetName([FromServices] IProductService _productService)
        {
            return Ok(_productService.GetName());
        }
        #endregion

        #region GetException
        /// <summary>
        /// A test endpoint that intentionally throws an exception (divide by zero).
        /// </summary>
        /// <returns>Throws a divide by zero exception.</returns>
        [HttpGet(Name = "exception")]
        public int Get()
        {
            int a = 10, b = 0;
            return a / b; // This will throw a runtime exception (divide by zero)
        }
        #endregion
    }
}
