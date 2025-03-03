using ServiceLifetime.Models;

namespace ServiceLifetime.Interface
{
    /// <summary>
    /// Defines the contract for the ProductService, including methods for adding and retrieving products.
    /// </summary>
    public interface IProductService
    {
        #region Methods
        /// <summary>
        /// Adds a new product to the collection.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The ID of the added product.</returns>
        int AddProducts(Product product);

        /// <summary>
        /// Retrieves all products from the collection.
        /// </summary>
        /// <returns>A list of all products.</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Retrieves the name associated with the product service.
        /// </summary>
        /// <returns>The name of the product service.</returns>
        string GetName();
        #endregion
    }
}
