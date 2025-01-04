namespace AdvanceAPI.PartialClass
{
    /// <summary>
    /// Contains methods for the Person class, such as greeting and displaying age.
    /// This is the second part of the partial class.
    /// </summary>
    public partial class Person
    {
        /// <summary>
        /// Prints a greeting message with the person's name and age.
        /// </summary>
        public void Greet()
        {
            Console.WriteLine($"Hello, my name is {Name} and I am {Age} years old.");
        }

        /// <summary>
        /// Prints the person's age.
        /// </summary>
        public void DisplayAge()
        {
            Console.WriteLine($"I am {Age} years old.");
        }
    }
}