namespace AdvanceAPI.PartialClass
{
    /// <summary>
    /// Main class to demonstrate the usage of the Person class, which is split into multiple parts.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that demonstrates the creation of a Person object and calling its methods.
        /// </summary>
        static void Main()
        {
            // Create a new Person object using the constructor from Person.Part1.cs
            Person person = new Person("Alice", 30);

            // Call methods from Person.Part2.cs
            person.Greet();        // Output: Hello, my name is Alice and I am 30 years old.
            person.DisplayAge();   // Output: I am 30 years old.
        }
    }
}