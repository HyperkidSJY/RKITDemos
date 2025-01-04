namespace Collections
{   
    public class ExceptionHandling
    {
        /// <summary>
        /// Demonstrates basic exception handling in C# using try-catch-finally blocks.
        /// The program prompts the user to input two numbers, performs a sum, and handles exceptions such as invalid input (FormatException) and general exceptions.
        /// It also ensures that the "finally" block is executed to print a thank you message regardless of whether an error occurred or not.
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                // Prompt user for the first number
                Console.Write("Enter the first number: ");
                string input1 = Console.ReadLine();
                int number1 = int.Parse(input1);

                // Prompt user for the second number
                Console.Write("Enter the second number: ");
                string input2 = Console.ReadLine();
                int number2 = int.Parse(input2);

                // Calculate the sum
                int sum = number1 + number2;

                // Display the result
                Console.WriteLine($"The sum of {number1} and {number2} is {sum}.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Thank you for using the program!");
            }
        }
    }
}
