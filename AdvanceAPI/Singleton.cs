using System;

namespace AdvanceAPI
{
    /// <summary>
    /// Singleton class to manage a database connection.
    /// Ensures that only one instance of the DatabaseConnection class is created.
    /// </summary>
    public class DatabaseConnection
    {
        // Private static field to hold the only instance of the class
        private static DatabaseConnection instance = null;

        // Lock object for thread-safe instance creation
        private static readonly object lockObject = new object();

        /// <summary>
        /// Private constructor to prevent instantiation from outside the class.
        /// </summary>
        private DatabaseConnection()
        {
            Console.WriteLine("DatabaseConnection created.");
        }

        /// <summary>
        /// Public static property to access the single instance of DatabaseConnection.
        /// </summary>
        public static DatabaseConnection Instance
        {
            get
            {
                // Double-check locking pattern for thread safety
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseConnection();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Method to simulate a connection to the database.
        /// </summary>
        public void Connect()
        {
            Console.WriteLine("Connecting to the database...");
        }

        /// <summary>
        /// Method to simulate disconnecting from the database.
        /// </summary>
        public void Disconnect()
        {
            Console.WriteLine("Disconnecting from the database...");
        }
    }

    /// <summary>
    /// Main class demonstrating the use of the Singleton pattern.
    /// </summary>
    public class Singleton
    {
        /// <summary>
        /// Main entry point to demonstrate the Singleton pattern in action.
        /// </summary>
        static void Main()
        {
            // Try to create multiple instances of the Singleton class
            DatabaseConnection connection1 = DatabaseConnection.Instance;
            DatabaseConnection connection2 = DatabaseConnection.Instance;

            // Both variables should point to the same instance
            if (connection1 == connection2)
            {
                Console.WriteLine("Both connections are the same instance.");
            }

            // Use the Singleton instance
            connection1.Connect();
            connection2.Disconnect();
        }
    }
}
