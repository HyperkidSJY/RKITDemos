namespace AdvanceAPI.PartialClass
{
    /// <summary>
    /// Represents a person with basic properties like Name and Age.
    /// This class is split into multiple parts to demonstrate the use of partial classes in C#.
    /// </summary>
    public partial class Person
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
        /// Initializes a new instance of the Person class with the specified name and age.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="age">The age of the person.</param>
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}