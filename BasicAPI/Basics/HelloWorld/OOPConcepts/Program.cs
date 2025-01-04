namespace OOPConcepts
{
    /// <summary>
    /// Demonstrates Object-Oriented Programming (OOP) concepts in C# including classes, inheritance, polymorphism, encapsulation, and interfaces.
    /// The base class 'Animal' defines common properties and methods for all animals, including a static field for animal count and a virtual method for speaking.
    /// The derived class 'Dog' overrides the Speak method and adds specific behavior like fetching.
    /// The 'ITrainable' interface defines a contract, which the 'GuideDog' class implements, showcasing polymorphism and interface implementation.
    /// Also demonstrates the use of static methods and fields.
    /// </summary>

    // Base class representing a general Animal
    public class Animal
    {
        // Private static variable with underscore prefix (allowed exception)
        private static int _animalCount = 0;

        // Private field
        private string name;

        // Protected field
        protected int age;

        // Public property for accessing the private field
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Virtual method to demonstrate polymorphism
        public virtual void Speak()
        {
            Console.WriteLine("Animal makes a sound.");
        }

        // Constructor
        public Animal(string name, int age)
        {
            this.name = name;
            this.age = age;
            _animalCount++;
        }

        // Static method to demonstrate static scope
        public static int GetAnimalCount()
        {
            return _animalCount;
        }

        // Method demonstrating encapsulation
        public string GetDetails()
        {
            return $"Name: {name}, Age: {age}";
        }
    }

    // Derived class Dog inherits from Animal
    public class Dog : Animal
    {
        // Public property for the breed
        public string Breed { get; set; }

        // Constructor (uses base keyword to call the parent class constructor)
        public Dog(string name, int age, string breed) : base(name, age)
        {
            Breed = breed;
        }

        // Overriding the Speak method
        public override void Speak()
        {
            Console.WriteLine($"{Name} barks!");
        }

        // Method demonstrating polymorphism
        public void Fetch()
        {
            Console.WriteLine($"{Name} is fetching!");
        }
    }

    // Interface used to define a contract that implementing classes must follow
    public interface ITrainable
    {
        void Train(); // Method signature
    }

    // GuideDog class implements ITrainable
    public class GuideDog : Dog, ITrainable
    {
        public GuideDog(string name, int age, string breed) : base(name, age, breed) { }

        // Implementing the interface method
        public void Train()
        {
            Console.WriteLine($"{Name} is being trained as a guide dog.");
        }
    }
    public class Program
    {
        // Static method demonstrating the "static" scope
        public static void ShowScopeExample()
        {
            Console.WriteLine("Static method: Accessible without creating an instance.");
        }
        static void Main(string[] args)
        {
            var genericAnimal = new Animal("GenericAnimal", 5);
            Console.WriteLine(genericAnimal.GetDetails());
            genericAnimal.Speak();

            var myDog = new Dog("Buddy", 3, "Labrador");
            Console.WriteLine(myDog.GetDetails());
            Console.WriteLine($"Breed: {myDog.Breed}");
            myDog.Speak();
            myDog.Fetch();

            var guideDog = new GuideDog("Max", 4, "Golden Retriever");
            guideDog.Train();
            guideDog.Speak();

            ShowScopeExample();

            Console.WriteLine($"Total Animals Created: {Animal.GetAnimalCount()}");
        }
    }
}
