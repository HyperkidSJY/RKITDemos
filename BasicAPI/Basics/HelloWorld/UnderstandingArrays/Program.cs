namespace UnderstandingArrays
{   /// <summary>
    /// Explores different types of arrays in C# and their operations.
    /// Includes single-dimensional arrays, multi-dimensional arrays, and jagged arrays.
    /// Demonstrates operations such as accessing, iterating, sorting, searching, copying, resizing, clearing elements, 
    /// finding max/min values, reversing, and converting data into arrays.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====== Single-Dimensional Arrays ======");
            // Single-dimensional array
            int[] numbers = { 5, 2, 8, 1, 3 };
            Console.WriteLine("Original Array: " + string.Join(", ", numbers));

            // Accessing elements
            Console.WriteLine($"Element at index 2: {numbers[2]}");

            // Iterating using a for loop
            Console.WriteLine("Iterating using for loop:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"Index {i}: {numbers[i]}");
            }

            // Iterating using a foreach loop
            Console.WriteLine("Iterating using foreach loop:");
            foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }

            // Sorting the array
            Array.Sort(numbers);
            Console.WriteLine("Sorted Array: " + string.Join(", ", numbers));

            // Searching for an element
            int index = Array.IndexOf(numbers, 8);
            Console.WriteLine($"Index of 8: {index}");

            // Copying the array
            int[] copiedArray = new int[numbers.Length];
            Array.Copy(numbers, copiedArray, numbers.Length);
            Console.WriteLine("Copied Array: " + string.Join(", ", copiedArray));

            // Resizing the array
            Array.Resize(ref copiedArray, 7); // Resize to length 7
            copiedArray[5] = 10;
            copiedArray[6] = 12;
            Console.WriteLine("Resized Array: " + string.Join(", ", copiedArray));

            // Clearing elements
            Array.Clear(copiedArray, 2, 2); // Clear 2 elements starting at index 2
            Console.WriteLine("Array After Clear: " + string.Join(", ", copiedArray));


            Console.WriteLine("\n====== Multi-Dimensional Arrays ======");
            // Multi-dimensional array (2D array)
            int[,] matrix = {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            // Accessing elements
            Console.WriteLine($"Element at (1,2): {matrix[1, 2]}");

            // Iterating through a 2D array
            Console.WriteLine("Matrix Elements:");
            for (int i = 0; i < matrix.GetLength(0); i++) // Rows
            {
                for (int j = 0; j < matrix.GetLength(1); j++) // Columns
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n====== Jagged Arrays ======");
            // Jagged array
            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[] { 1, 2, 3 };
            jaggedArray[1] = new int[] { 4, 5 };
            jaggedArray[2] = new int[] { 6, 7, 8, 9 };

            // Accessing elements
            Console.WriteLine($"First element of first array: {jaggedArray[0][0]}");

            // Iterating through a jagged array
            Console.WriteLine("Jagged Array Elements:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.Write($"Row {i}: ");
                foreach (int val in jaggedArray[i])
                {
                    Console.Write(val + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n====== Additional Array Operations ======");
            // Finding the max and min values
            int max = numbers[^1]; // Last element in a sorted array
            int min = numbers[0];  // First element in a sorted array
            Console.WriteLine($"Max Value: {max}, Min Value: {min}");

            // Reverse an array
            Array.Reverse(numbers);
            Console.WriteLine("Reversed Array: " + string.Join(", ", numbers));


            string input = "1,2,3,4";

            // Split the string by commas and convert to an integer array
            int[] x = Array.ConvertAll(input.Split(','), int.Parse);

            // Print the array to verify
            Console.WriteLine("Array elements:");
            foreach (int y in x)
            {
                Console.WriteLine(y);
            }
        }
    }
}
