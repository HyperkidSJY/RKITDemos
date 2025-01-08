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

            string generatedKey = "0123456789ABCDEF0123456789ABCDEF";//BLRijndael.GenerateKey(keySizeInBits);
            string iv = "0123456789ABCDEF";

            // Encrypt and decrypt using Rijndael
            encriptData = Rijndael.EncryptedByRijndael(data, generatedKey, iv);
            Console.WriteLine($"{data} after Encrypted By Rijndael : {encriptData} ");
            Console.WriteLine($"{encriptData} after Decrypted By Rijndael : {Rijndael.DecryptByRijndael(encriptData, generatedKey, iv)}");
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

    public class Rijndael
    {
        /// <summary>
        /// Encrypts the input text using Rijndael algorithm.
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <param name="key">Encryption key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns>Encrypted text as a Base64-encoded string</returns>

        public static string EncryptedByRijndael(string plainText, string key, string iv)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Key = Encoding.UTF8.GetBytes(key);
                rijndael.Mode = CipherMode.CFB;
                rijndael.Padding = PaddingMode.PKCS7; // Use PKCS7 padding
                rijndael.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts the input cipher text using Rijndael algorithm.
        /// </summary>
        /// <param name="cipherText">Cipher text as a Base64-encoded string</param>
        /// <param name="key">Decryption key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns>Decrypted text</returns>
        public static string DecryptByRijndael(string cipherText, string key, string iv)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Key = Encoding.UTF8.GetBytes(key);
                rijndael.Mode = CipherMode.CFB;
                rijndael.Padding = PaddingMode.PKCS7; // Use PKCS7 padding
                rijndael.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

