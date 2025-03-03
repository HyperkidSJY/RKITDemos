#define DEBUG_MODE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace AdvanceAPI
{
    public class FileSystem
    {
        /// <summary>
        /// Entry point of the program where different stream examples are demonstrated.
        /// </summary>
        public static void Main()
        {
            #region FileStream Example
            /// <summary>
            /// Demonstrates how to use FileStream for writing and reading a file.
            /// </summary>
            string filePath = "example.txt";

            // Writing data to a file using FileStream
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes("Hello, FileStream!");
                fs.Write(data, 0, data.Length);
            }

            // Reading data from a file using FileStream 
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                Console.WriteLine("FileStream Read: " + Encoding.UTF8.GetString(buffer));
            }
            #endregion

            #region StreamWriter Example
            /// <summary>
            /// Demonstrates how to use StreamWriter for writing and reading text to/from a file.
            /// </summary>
            string streamWriterFilePath = "streamwriter_example.txt";

            // Writing to a file using StreamWriter
            using (StreamWriter writer = new StreamWriter(streamWriterFilePath))
            {
                writer.WriteLine("Hello from StreamWriter!");
                writer.WriteLine("StreamWriter makes writing easier.");
            }

            // Reading from the file using StreamReader
            using (StreamReader reader = new StreamReader(streamWriterFilePath))
            {
                Console.WriteLine("StreamWriter Read: " + reader.ReadToEnd());
            }
            #endregion

            #region CryptoStream Example
            /// <summary>
            /// Demonstrates how to use CryptoStream for encrypting data with AES.
            /// </summary>
            string dataToEncrypt = "SensitiveData";
            string password = "securepassword";

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(password.PadRight(16)); // AES-128 requires 16 bytes key
                aesAlg.IV = new byte[16]; // Zero initialization vector

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(dataToEncrypt);
                    }

                    // Display encrypted data
                    Console.WriteLine("CryptoStream Encrypted Data: " + Convert.ToBase64String(ms.ToArray()));
                }
            }
            #endregion

            #region BufferedStream Example
            /// <summary>
            /// Demonstrates how to use BufferedStream for reading and writing with buffering to improve performance.
            /// </summary>
            string bufferedStreamFilePath = "buffered_stream_example.txt";

            using (FileStream fs = new FileStream(bufferedStreamFilePath, FileMode.Create, FileAccess.Write))
            using (BufferedStream bufferedStream = new BufferedStream(fs))
            {
                byte[] data = Encoding.UTF8.GetBytes("Hello from BufferedStream!");
                bufferedStream.Write(data, 0, data.Length);
            }

            // Reading the data using BufferedStream
            using (FileStream fs = new FileStream(bufferedStreamFilePath, FileMode.Open, FileAccess.Read))
            using (BufferedStream bufferedStream = new BufferedStream(fs))
            {
                byte[] buffer = new byte[fs.Length];
                bufferedStream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("BufferedStream Read: " + Encoding.UTF8.GetString(buffer));
            }
            #endregion

            #region MemoryStream Example
            /// <summary>
            /// Demonstrates how to use MemoryStream for working with data in memory rather than on disk.
            /// </summary>
            string memoryData = "This is data stored in memory.";

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(memoryData);
                ms.Write(byteArray, 0, byteArray.Length);

                ms.Seek(0, SeekOrigin.Begin); // Reset stream position to start
                byte[] buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);

                Console.WriteLine("MemoryStream Read: " + Encoding.UTF8.GetString(buffer));
            }
            #endregion

            #region BinaryWriter and BinaryReader Example
            /// <summary>
            /// Demonstrates how to use BinaryWriter and BinaryReader for writing and reading binary data.
            /// </summary>
            string binaryFilePath = "binary_example.dat";

            // Writing data with BinaryWriter
            using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
            {
                writer.Write(123);        // int
                writer.Write(456.78);     // double
                writer.Write("Hello Binary!"); // string
            }

            // Reading data with BinaryReader
            using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
            {
                int intVal = reader.ReadInt32();
                double doubleVal = reader.ReadDouble();
                string strVal = reader.ReadString();

                Console.WriteLine($"BinaryReader Read: {intVal}, {doubleVal}, {strVal}");
            }
            #endregion


            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Iterate through the list and check for each number
            foreach (var number in numbers)
            {
                // You can set a conditional breakpoint here on this line:
                // condition: number > 5
                Console.WriteLine($"Current number: {number}");
            }

            #if DEBUG_MODE
                    Console.WriteLine("Debug mode is enabled.");
            #else
                        Console.WriteLine("Release mode is enabled.");
            #endif

            #if WINDOWS
                    Console.WriteLine("Running on Windows.");
            #elif LINUX
                    Console.WriteLine("Running on Linux.");
            #else
                        Console.WriteLine("Unknown platform.");
            #endif

                        // Some other code
                        Console.WriteLine("Execution completed.");
        }
    }
}
