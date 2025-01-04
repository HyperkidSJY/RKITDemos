using System;
using System.IO;
using System.Text.Json;

namespace AdvanceAPI
{
    /// <summary>
    /// Represents a purchase with a product name, date, and price.
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Gets or sets the name of the product being purchased.
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the purchase was made.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal ProductPrice { get; set; }
    }

    /// <summary>
    /// This class demonstrates the process of serializing and deserializing an object to/from JSON.
    /// </summary>
    public class JSONSand
    {
        /// <summary>
        /// Main method to execute JSON serialization and deserialization of a Purchase object.
        /// </summary>
        static void Main()
        {
            // Create a new Purchase object with sample data
            Purchase purchase = new Purchase
            {
                ProductName = "Orange Juice",
                DateTime = DateTime.UtcNow,
                ProductPrice = 2.49m
            };

            // Create JsonSerializerOptions to customize serialization behavior
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Allows case-insensitive property matching during deserialization
                WriteIndented = true // Formats the JSON string with indentation (pretty-printing)
            };

            // Serialize the Purchase object to a JSON string with the specified options
            string jsonString = JsonSerializer.Serialize(purchase, options);
            Console.WriteLine(jsonString); // Output the serialized JSON string

            // Save the JSON string to a file named "purchase.json"
            File.WriteAllText("purchase.json", jsonString);

            // Read the JSON string back from the file
            var purchaseJson = File.ReadAllText("purchase.json");

            // Deserialize the JSON string back into a Purchase object
            Purchase purchaseD = JsonSerializer.Deserialize<Purchase>(purchaseJson);

            // Output the deserialized object's ProductName to verify the deserialization
            Console.WriteLine(purchaseD.ProductName);
        }
    }
}
