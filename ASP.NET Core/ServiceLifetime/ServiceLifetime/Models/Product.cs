namespace ServiceLifetime.Models
{
    /// <summary>
    /// Represents a product with an ID and name.
    /// </summary>
    public class Product
    {
        #region Properties
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}
