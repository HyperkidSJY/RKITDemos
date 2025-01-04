namespace Collections
{   /// <summary>
    /// This program demonstrates the use of enums, collections (List and Dictionary), and switch statements.
    /// It defines an enum for Animal types and a class representing an Animal with properties like Name, Age, and Type.
    /// The program:
    /// 1. Creates a list of Animal objects with different types (Mammal, Bird, Reptile, etc.).
    /// 2. Displays the list of animals.
    /// 3. Adds a new animal to the list and displays the updated list.
    /// 4. Groups the animals by their type using a Dictionary and displays the groups.
    /// 5. Uses a switch statement to describe an AnimalType based on its enum value.
    /// </summary>
    public enum AnimalType
    {
        Mammal,
        Bird,
        Reptile,
        Amphibian,
        Fish,
        Insect
    }

    // Class representing an Animal
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public AnimalType Type { get; set; }

        public Animal(string name, int age, AnimalType type)
        {
            Name = name;
            Age = age;
            Type = type;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Type: {Type}";
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            // List collection to store Animal objects
            var animals = new List<Animal>
        {
            new Animal("Lion", 5, AnimalType.Mammal),
            new Animal("Eagle", 3, AnimalType.Bird),
            new Animal("Crocodile", 7, AnimalType.Reptile),
            new Animal("Frog", 2, AnimalType.Amphibian),
            new Animal("Shark", 12, AnimalType.Fish)
        };

            // Display all animals
            DisplayAnimals(animals);

            // Adding an animal to the list
            var newAnimal = new Animal("Butterfly", 1, AnimalType.Insect);
            animals.Add(newAnimal);
            Console.WriteLine("\nAfter adding a new animal:");
            DisplayAnimals(animals);

            // Dictionary collection to map AnimalType to a list of Animal names
            var animalGroups = GroupAnimalsByType(animals);
            Console.WriteLine("\nGrouped animals by type:");
            DisplayAnimalGroups(animalGroups);

            // Using enum in a switch case
            var animalTypeToDescribe = AnimalType.Bird;
            DescribeAnimalType(animalTypeToDescribe);
        }

        // Method to display a list of animals
        public static void DisplayAnimals(List<Animal> animals)
        {
            Console.WriteLine("List of Animals:");
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }

        // Method to group animals by their type using a dictionary
        public static Dictionary<AnimalType, List<string>> GroupAnimalsByType(List<Animal> animals)
        {
            var animalGroups = new Dictionary<AnimalType, List<string>>();

            foreach (var animal in animals)
            {
                if (!animalGroups.ContainsKey(animal.Type))
                {
                    animalGroups[animal.Type] = new List<string>();
                }
                animalGroups[animal.Type].Add(animal.Name);
            }

            return animalGroups;
        }

        // Method to display animal groups
        public static void DisplayAnimalGroups(Dictionary<AnimalType, List<string>> animalGroups)
        {
            foreach (var group in animalGroups)
            {
                Console.WriteLine($"{group.Key}: {string.Join(", ", group.Value)}");
            }
        }

        // Method to describe an AnimalType using a switch statement
        public static void DescribeAnimalType(AnimalType type)
        {
            Console.WriteLine("\nDescription of Animal Type:");
            switch (type)
            {
                case AnimalType.Mammal:
                    Console.WriteLine("Mammals are warm-blooded animals with fur or hair.");
                    break;
                case AnimalType.Bird:
                    Console.WriteLine("Birds are feathered creatures that often fly.");
                    break;
                case AnimalType.Reptile:
                    Console.WriteLine("Reptiles are cold-blooded and have scales.");
                    break;
                case AnimalType.Amphibian:
                    Console.WriteLine("Amphibians can live both in water and on land.");
                    break;
                case AnimalType.Fish:
                    Console.WriteLine("Fish are cold-blooded and live in water.");
                    break;
                case AnimalType.Insect:
                    Console.WriteLine("Insects are small, segmented creatures with exoskeletons.");
                    break;
                default:
                    Console.WriteLine("Unknown animal type.");
                    break;
            }
        }
    }
}
