namespace AdvanceAPI
{
    /// <summary>
    /// Represents a person with basic attributes like Name and Age.
    /// </summary>
    class Person
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the person.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class with the given name and age.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="age">The age of the person.</param>
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        /// <summary>
        /// Introduces the person by printing their name and age.
        /// </summary>
        public void Introduce()
        {
            // Output the introduction message to the console
            Console.WriteLine($"Hello, my name is {Name} and I am {Age} years old.");
        }
    }

    /// <summary>
    /// Contains the Main method to test the Person class.
    /// </summary>
    public class Basic
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        static void Main()
        {
            // Creating a new instance of the Person class with name "Alice" and age 30
            Person person = new Person("Alice", 30);

            // Calling the Introduce method on the person object to display the introduction
            person.Introduce();  // Output: Hello, my name is Alice and I am 30 years old.
        }
    }
}

