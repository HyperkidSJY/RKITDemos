namespace AdvanceAPI
{
    public class ExtensionMethod
    {
        static void Main(string[] args)
        {
            string name = "Shivam";
            string cap = name.Capitalize();
            Console.WriteLine(cap);

            List<int> numbers = new List<int> {1,2,3,4,5};
            double average = numbers.Average();
            Console.WriteLine(average);

            int[] ints = numbers.ToArray();
            double avg = ints.Average();
            Console.WriteLine(avg);
        }
    }

    public static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }

    public static class IEnumerableExtensions
    {
        public static double Average(this  IEnumerable<int> input)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return (double)input.Sum() / input.Count();
        }
    }
}
