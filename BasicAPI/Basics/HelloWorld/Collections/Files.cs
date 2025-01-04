namespace Collections
{   
    public class Files
    {
        /// <summary>
        /// Demonstrates basic file operations in C# such as creating, writing, reading, appending, checking existence, and deleting files.
        /// The program:
        /// 1. Creates a file and writes a line of text.
        /// 2. Reads and displays the content of the file.
        /// 3. Appends additional text to the file.
        /// 4. Reads and displays the updated file content.
        /// 5. Checks if the file exists and informs the user.
        /// It uses exception handling to manage any potential errors during file operations.
        /// </summary>
        public static void Main(string[] args)
        {
            string filePath = "test.txt";

            try
            {
                // 1. Create a file and write text
                // sub directory and dir
                // filename 
                Console.WriteLine("Creating a file and writing text...");
                File.WriteAllText(filePath, "Hello, this is the first line.\n");
                Console.WriteLine("File created and text written successfully.\n");

                // 2. Read and display the content of the file
                Console.WriteLine("Reading file content:");
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);

                // 3. Append more text to the file
                Console.WriteLine("Appending text to the file...");
                File.AppendAllText(filePath, "This is an appended line.\n");
                Console.WriteLine("Text appended successfully.\n");

                // 4. Read the updated content
                Console.WriteLine("Reading updated file content:");
                string updatedContent = File.ReadAllText(filePath);
                Console.WriteLine(updatedContent);

                // 5. Check if the file exists
                if (File.Exists(filePath))
                {
                    Console.WriteLine("File exists.");
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }

                // 6. Delete the file
                //Console.WriteLine("Deleting the file...");
                //File.Delete(filePath);
                //Console.WriteLine("File deleted successfully.\n");

                //// Confirm deletion
                //if (!File.Exists(filePath))
                //{
                //    Console.WriteLine("File deletion confirmed. The file no longer exists.");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
