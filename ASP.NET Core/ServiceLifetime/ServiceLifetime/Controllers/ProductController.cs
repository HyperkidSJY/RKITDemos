using Microsoft.AspNetCore.Mvc;
using ServiceLifetime.Interface;
using ServiceLifetime.Models;

namespace ServiceLifetime.Controllers
{
    /// <summary>
    /// Controller responsible for handling product-related HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the ProductController with the specified product service.
        /// </summary>
        /// <param name="productService">The product service to be injected.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region AddProducts
        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>A list of all products after the new product has been added.</returns>
        [HttpPost]
        public IActionResult AddProducts([FromBody] Product product)
        {
            // Add the product to the collection
            _productService.AddProducts(product);

            // Retrieve all products
            List<Product> list = _productService.GetAllProducts();

            // Return the list of products
            return Ok(list);
        }
        #endregion

        #region GetName
        /// <summary>
        /// Retrieves the name from the product service.
        /// </summary>
        /// <returns>The name of the product service.</returns>
        [HttpGet]
        public IActionResult GetName()
        {
            return Ok(_productService.GetName());
        }
        #endregion
    }
}
