namespace AdvanceAPI
{
    /// <summary>
    /// Main class to demonstrate usage of extension methods for strings and IEnumerable.
    /// </summary>
    public class ExtensionMethod
    {
        /// <summary>
        /// Entry point of the application.
        /// Demonstrates the usage of the <see cref="StringExtensions.Capitalize"/> method and the <see cref="IEnumerableExtensions.Average"/> method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            string name = "shivam";
            string cap = name.Capitalize();
            Console.WriteLine(cap);

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            double average = numbers.Average();
            Console.WriteLine(average);

            int[] ints = numbers.ToArray();
            double avg = ints.Average();
            Console.WriteLine(avg);
        }
    }

    /// <summary>
    /// Extension methods for <see cref="string"/> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalizes the first letter of the string, if the string is not null or empty.
        /// </summary>
        /// <param name="input">The input string to be capitalized.</param>
        /// <returns>The input string with the first letter capitalized, or the input string if it is null or empty.</returns>
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }

    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> where T is <see cref="int"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Calculates the average of a sequence of integers.
        /// </summary>
        /// <param name="input">The input sequence of integers.</param>
        /// <returns>The average of the integers in the sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input sequence is null.</exception>
        public static double Average(this IEnumerable<int> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return (double)input.Sum() / input.Count();
        }
    }
}

