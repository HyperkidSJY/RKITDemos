using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagement.Helpers
{
    /// <summary>
    /// Helper class to handle encryption and decryption operations.
    /// </summary>
    public class Encryption
    {
        #region Private Constants

        /// <summary>
        /// The initialization vector used for AES encryption.
        /// </summary>
        private static readonly string _iv = "0123456789ABCDEF";

        /// <summary>
        /// The encryption key used for AES encryption.
        /// </summary>
        private static readonly string _key = "0123456789ABCDEF0123456789ABCDEF";

        #endregion

        #region Private Fields

        /// <summary>
        /// AES encryption object used for cryptographic operations.
        /// </summary>
        private static readonly Aes _objAES;

        #endregion

        #region Constructor

        /// <summary>
        /// Static constructor to initialize the AES encryption object with the provided key and IV.
        /// </summary>
        static Encryption()
        {
            _objAES = Aes.Create();
            _objAES.Key = Encoding.UTF8.GetBytes(_key);
            _objAES.IV = Encoding.UTF8.GetBytes(_iv);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Encrypts a plaintext password using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext string to encrypt.</param>
        /// <returns>The encrypted password as a Base64 encoded string.</returns>
        public static string GetEncryptPassword(string plaintext)
        {
            // Create an encryptor using the AES algorithm with the provided key and IV
            ICryptoTransform encryptor = _objAES.CreateEncryptor(_objAES.Key, _objAES.IV);

            // Encrypt the plaintext
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plaintext);
                    }
                }

                // Return the encrypted password as a Base64 encoded string
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        #endregion
    }
}
