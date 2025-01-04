using System;

namespace AdvanceAPI
{
    /// <summary>
    /// Represents a sealed class that cannot be inherited.
    /// </summary>
    sealed class FinalClass
    {
        /// <summary>
        /// Method to greet from the sealed class.
        /// </summary>
        public void Greet()
        {
            Console.WriteLine("Hello from a sealed class!");
        }
    }

    /// <summary>
    /// The main class that demonstrates the usage of the sealed class FinalClass.
    /// </summary>
    public class SealedClass
    {
        /// <summary>
        /// Main entry point of the application.
        /// Demonstrates creating an instance of the sealed class and calling its method.
        /// </summary>
        static void Main()
        {
            // Create an instance of the sealed FinalClass
            FinalClass fc = new FinalClass();

            // Call the Greet method from the FinalClass
            fc.Greet();  // Output: Hello from a sealed class!
        }
    }
}
