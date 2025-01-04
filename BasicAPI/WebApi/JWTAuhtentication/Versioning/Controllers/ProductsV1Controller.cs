using System.Web.Http;

namespace Versioning.Controllers
{
    /// <summary>
    /// Handles product-related requests for API version 1.
    /// </summary>
    [RoutePrefix("api/v1/products")]
    public class ProductsV1Controller : ApiController
    {
        /// <summary>
        /// Retrieves all products for version 1 of the API.
        /// </summary>
        /// <returns>
        /// An <see cref="IHttpActionResult"/> containing a message indicating version 1 of the API.
        /// </returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            return Ok("This is version 1 of the product API.");
        }

        /// <summary>
        /// Retrieves a specific product by ID for version 1 of the API.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>
        /// An <see cref="IHttpActionResult"/> containing the requested product's details.
        /// </returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            return Ok($"This is product {id} from version 1.");
        }
    }
}