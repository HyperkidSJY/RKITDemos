using System.Security.Cryptography;
using System.Text;

namespace AdvanceAPI
{
    public class SecandCryp
    {
        static void Main()
        {
            Console.WriteLine("Enter Input : ");
            string data = Console.ReadLine();

            // Encrypt and decrypt using AES
            string encriptData = AES.EncryptedByAES(data);
            Console.WriteLine($"{data} after Encrypted By AES : {encriptData} ");
            Console.WriteLine($"{encriptData} after Decrypted By AES : {AES.DecryptByAES(encriptData)}");

            Console.WriteLine();

            // Encrypt and decrypt using RSA
            encriptData = RSA.EncryptedByRSA(data);
            Console.WriteLine($"{data} after Encrypted By RSA : {encriptData} ");
            Console.WriteLine($"{encriptData} after Decrypted By RSA : {RSA.DecryptByRSA(encriptData)}");
        }
    }

    public class RSA
    {
        private static RSACryptoServiceProvider _objRsa = new RSACryptoServiceProvider();

        /// <summary>
        /// Encrypts the input data using RSA.
        /// </summary>
        /// <param name="data">Input data to be encrypted</param>
        /// <returns>Encrypted data as a Base64-encoded string</returns>
        public static string EncryptedByRSA(string data)
        {
            // Convert input data to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            // Encrypt the bytes using RSA
            byte[] encryptedBytes = _objRsa.Encrypt(bytes, true);

            // Convert the encrypted bytes to a Base64-encoded string
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts the input data using RSA.
        /// </summary>
        /// <param name="data">Encrypted data as a Base64-encoded string</param>
        /// <returns>Decrypted data as a UTF-8 encoded string</returns>
        public static string DecryptByRSA(string data)
        {
            // Convert Base64-encoded string to bytes
            byte[] bytes = Convert.FromBase64String(data);

            // Decrypt the bytes using RSA
            byte[] decryptedBytes = _objRsa.Decrypt(bytes, true);

            // Convert the decrypted bytes to a UTF-8 encoded string
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    public class AES
    {
        private static AesCryptoServiceProvider _objAes = new AesCryptoServiceProvider();
        private static string _generatedKey = "0123456789ABCDEF0123456789ABCDEF";
        private static string _iv = "0123456789ABCDEF";
        /// <summary>
        /// Encrypts the input data using AES.
        /// </summary>
        /// <param name="data">Input data to be encrypted</param>
        /// <returns>Encrypted data as a Base64-encoded string</returns>
        public static string EncryptedByAES(string data)
        {
            // Convert input data to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] key = Encoding.UTF8.GetBytes(_generatedKey);
            byte[] iv = Encoding.UTF8.GetBytes(_iv);


            using (ICryptoTransform encript = _objAes.CreateEncryptor(key, iv))
            {
                // Encrypt the bytes using AES
                byte[] encriptedBytes = encript.TransformFinalBlock(bytes, 0, bytes.Length);

                // Convert the encrypted bytes to a Base64-encoded string
                return Convert.ToBase64String(encriptedBytes);
            }
        }

        /// <summary>
        /// Decrypts the input data using AES.
        /// </summary>
        /// <param name="data">Encrypted data as a Base64-encoded string</param>
        /// <returns>Decrypted data as a UTF-8 encoded string</returns>
        public static string DecryptByAES(string data)
        {
            // Convert Base64-encoded string to bytes
            byte[] bytes = Convert.FromBase64String(data);
            byte[] key = Encoding.UTF8.GetBytes(_generatedKey);
            byte[] iv = Encoding.UTF8.GetBytes(_iv);

            using (ICryptoTransform decript = _objAes.CreateDecryptor(key, iv))
            {
                // Decrypt the bytes using AES
                byte[] decriptBytes = decript.TransformFinalBlock(bytes, 0, bytes.Length);

                // Convert the decrypted bytes to a UTF-8 encoded string
                return Encoding.UTF8.GetString(decriptBytes);
            }
        }
    }
}

