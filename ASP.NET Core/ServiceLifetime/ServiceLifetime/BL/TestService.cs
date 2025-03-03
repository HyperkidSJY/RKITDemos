using ServiceLifetime.Interface;
using ServiceLifetime.Models;

namespace ServiceLifetime.BL
{
    /// <summary>
    /// Implementation of the IProductService interface used for testing purposes.
    /// </summary>
    public class TestService : IProductService
    {
        #region AddProducts
        /// <summary>
        /// Adds a product to the collection. This method is not implemented in the TestService.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>Throws NotImplementedException.</returns>
        /// <exception cref="NotImplementedException">Thrown because the method is not implemented.</exception>
        public int AddProducts(Product product)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetAllProducts
        /// <summary>
        /// Retrieves all products. This method is not implemented in the TestService.
        /// </summary>
        /// <returns>Throws NotImplementedException.</returns>
        /// <exception cref="NotImplementedException">Thrown because the method is not implemented.</exception>
        public List<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetName
        /// <summary>
        /// Returns a test name from the TestService.
        /// </summary>
        /// <returns>A string "name from test".</returns>
        public string GetName()
        {
            return "name from test";
        }
        #endregion
    }
}
