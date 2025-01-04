using System;
using System.Diagnostics.Contracts;

namespace AdvanceAPI
{
    /// <summary>
    /// Represents an abstract animal class with common properties and methods for animals.
    /// </summary>
    abstract class Animal
    {
        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Abstract method that must be implemented by any derived class to specify how the animal speaks.
        /// </summary>
        public abstract void Speak();

        /// <summary>
        /// Method that simulates the animal eating.
        /// </summary>
        public void Eat()
        {
            Console.WriteLine($"{Name} is eating.");
        }
    }

    /// <summary>
    /// Represents a Dog, derived from the Animal class, implementing the Speak method.
    /// </summary>
    class Dog : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Dog class with the specified name.
        /// </summary>
        /// <param name="name">The name of the dog.</param>
        public Dog(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Implements the abstract Speak method for the Dog class.
        /// </summary>
        public override void Speak()
        {
            Console.WriteLine($"{Name} says Woof!");
        }
    }

    /// <summary>
    /// The entry point class that demonstrates how to use the Animal and Dog classes.
    /// </summary>
    public class AbstractClass
    {
        /// <summary>
        /// The main entry point of the application, which demonstrates creating a Dog object
        /// and calling its Speak and Eat methods.
        /// </summary>
        static void Main()
        {
            // Creating a new Dog object
            Dog dog = new Dog("Rex");

            // Calling the Speak method for the dog object
            dog.Speak();  // Output: Rex says Woof!

            // Calling the Eat method for the dog object
            dog.Eat();    // Output: Rex is eating.
        }
    }
}

//abstract
//    Used for shared base functionality and to provide default behavior.
//interface
//    Used to define a contract that classes must implement, focusing on what the class should do rather than how.