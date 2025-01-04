using System.Numerics;

namespace AdvanceAPI
{
    /// <summary>
    /// A generic class that represents a better list with the ability to add items and print messages.
    /// </summary>
    public class BetterList<T>
    {
        private List<T> data = new();

        /// <summary>
        /// Adds a value to the list and prints a confirmation message.
        /// </summary>
        /// <param name="value">The value to add to the list.</param>
        public void AddToList(T value)
        {
            data.Add(value);
            Console.WriteLine($"{value} has been added to the list");
        }
    }

    /// <summary>
    /// An interface to define the concept of an "important" item.
    /// </summary>
    /// <typeparam name="T">The type of the items to compare for importance.</typeparam>
    public interface IImportant<T>
    {
        /// <summary>
        /// Determines which of the two items is the most important.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>The most important item.</returns>
        T MostImportant(T a, T b);
    }

    /// <summary>
    /// A class that implements the IImportant interface for both int and string types, defining what makes each type important.
    /// </summary>
    public class EvaluateImportance : IImportant<int>, IImportant<string>
    {
        /// <summary>
        /// Determines the most important integer by selecting the greater of the two.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The greater of the two integers.</returns>
        public int MostImportant(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        /// <summary>
        /// Determines the most important string by selecting the longer of the two.
        /// </summary>
        /// <param name="a">The first string.</param>
        /// <param name="b">The second string.</param>
        /// <returns>The longer of the two strings.</returns>
        public string MostImportant(string a, string b)
        {
            if (a.Length > b.Length)
            {
                return a;
            }
            return b;
        }
    }

    /// <summary>
    /// A generic class with constraints that ensures the type T is a reference type and has a parameterless constructor.
    /// </summary>
    /// <typeparam name="T">The type that must be a reference type and have a parameterless constructor.</typeparam>
    /// <typeparam name="U">The second type in the generic class, not constrained in this example.</typeparam>
    public class SampleClass<T, U> where T : class, new()
    {
    }

    /// <summary>
    /// A class that performs mathematical operations on numbers, constrained to types implementing the INumber<T> interface.
    /// </summary>
    /// <typeparam name="T">The type that must implement INumber<T>.</typeparam>
    public class MathOperations<T> where T : INumber<T>
    {
        /// <summary>
        /// Adds two numbers and returns the result.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The sum of x and y.</returns>
        public T Add(T x, T y)
        {
            return x + y;
        }
    }

    /// <summary>
    /// The entry point for the program, demonstrating the usage of generics, constraints, and type checking.
    /// </summary>
    public class GenericDemo
    {
        static void Main()
        {
            // Example of generic type checking using TypeChecker method
            TypeChecker(1); // Output: System.Int32 1
            TypeChecker("Tim"); // Output: System.String Tim

            // Example of using BetterList with integers
            BetterList<int> betterNumbers = new();
            betterNumbers.AddToList(5); // Output: 5 has been added to the list

            // Example of using BetterList with strings
            BetterList<string> betterStrings = new();
            betterStrings.AddToList("Shivam"); // Output: Shivam has been added to the list

            // Example of using EvaluateImportance class with int and string
            EvaluateImportance imp = new();
            Console.WriteLine(imp.MostImportant(5, 4)); // Output: 5
            Console.WriteLine(imp.MostImportant("Hello", "World!")); // Output: World!

            // Method to check the type of a given value
            void TypeChecker<T>(T value)
            {
                Console.WriteLine(typeof(T)); // Displays the type of T
                Console.WriteLine(value); // Displays the value of the parameter
            }
        }
    }
}
