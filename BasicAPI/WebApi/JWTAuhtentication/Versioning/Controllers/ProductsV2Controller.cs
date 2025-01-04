using System.Web.Http;

namespace Versioning.Controllers
{
    /// <summary>
    /// Handles product-related requests for API version 2.
    /// </summary>
    [RoutePrefix("api/v2/products")]
    public class ProductV2Controller : ApiController
    {
        /// <summary>
        /// Retrieves all products for version 2 of the API.
        /// </summary>
        /// <returns>
        /// An <see cref="IHttpActionResult"/> containing a message indicating version 2 of the API.
        /// </returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            return Ok("This is version 2 of the product API.");
        }

        /// <summary>
        /// Retrieves a specific product by ID for version 2 of the API.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>
        /// An <see cref="IHttpActionResult"/> containing the requested product's details.
        /// </returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            return Ok($"This is product {id} from version 2.");
        }

        /// <summary>
        /// Retrieves the total count of products for version 2 of the API.
        /// </summary>
        /// <returns>
        /// An <see cref="IHttpActionResult"/> containing the total count of products.
        /// </returns>
        [HttpGet]
        [Route("count")]
        public IHttpActionResult GetProductCount()
        {
            return Ok("This is the count of products in version 2.");
        }
    }
}