using System;

namespace AdvanceAPI
{
    /// <summary>
    /// A utility class for mathematical operations that provides static methods.
    /// </summary>
    static class MathUtility
    {
        /// <summary>
        /// Adds two integers and returns the result.
        /// </summary>
        /// <param name="a">The first integer to be added.</param>
        /// <param name="b">The second integer to be added.</param>
        /// <returns>The sum of the two integers.</returns>
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Multiplies two integers and returns the result.
        /// </summary>
        /// <param name="a">The first integer to be multiplied.</param>
        /// <param name="b">The second integer to be multiplied.</param>
        /// <returns>The product of the two integers.</returns>
        public static int Multiply(int a, int b)
        {
            return a * b;
        }
    }

    /// <summary>
    /// The class containing the Main method that demonstrates the usage of static methods from the MathUtility class.
    /// </summary>
    public class StaticClass
    {
        /// <summary>
        /// The entry point of the application that demonstrates calling static methods for mathematical operations.
        /// </summary>
        static void Main()
        {
            // Calling the Add method from MathUtility to get the sum of 3 and 5
            int sum = MathUtility.Add(3, 5);  // Output: 8
            Console.WriteLine(sum);  // Output: 8

            // Calling the Multiply method from MathUtility to get the product of 4 and 6
            int product = MathUtility.Multiply(4, 6);  // Output: 24
            Console.WriteLine(product);  // Output: 24
        }
    }
}
