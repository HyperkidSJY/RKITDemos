using ServiceLifetime.Interface;
using ServiceLifetime.Models;

namespace ServiceLifetime.BL
{
    /// <summary>
    /// Implementation of the IProductService interface for managing products.
    /// </summary>
    public class ProductService : IProductService
    {
        // In-memory storage for products
        private List<Product> products = new List<Product>();

        #region AddProducts
        /// <summary>
        /// Adds a new product to the list of products.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The ID of the added product.</returns>
        public int AddProducts(Product product)
        {
            // Assign a new ID to the product and add it to the list
            product.Id = products.Count + 1;
            products.Add(product);
            return product.Id;
        }
        #endregion

        #region GetAllProducts
        /// <summary>
        /// Retrieves all the products in the list.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public List<Product> GetAllProducts()
        {
            return products;
        }
        #endregion

        #region GetName
        /// <summary>
        /// Retrieves the name associated with the product service.
        /// </summary>
        /// <returns>The name of the product service.</returns>
        public string GetName()
        {
            return "name from product";
        }
        #endregion
    }
}
