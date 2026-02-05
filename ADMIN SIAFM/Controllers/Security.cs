using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ADMIN_SIAFM.Controllers
{
    public static class Security
    {
        // Fields
        private static string pwd = "1uc3$2";
        private static string vector = "1N&70N'Inifom$8563";
        private static string saltValue = "Inifom";
        private static int iterationRef = 1;
        private static int sizeKey = 0x100;
        private static string hash = "SHA1";

        // Methods
        public static string Decrypt(string plainText)
        {
            return Decrypt(plainText, pwd, saltValue, hash, iterationRef, vector, sizeKey);
        }

        public static string Decrypt(string plainText, string keyWord)
        {
            return Decrypt(plainText, keyWord, saltValue, hash, iterationRef, vector, sizeKey);
        }

        public static string Decrypt(string plainText, string password, string saltValue, string hashAlgorithm, int passwordIterations, string initialVector, int keySize)
        {
            try
            {
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(plainText);

                using (RijndaelManaged symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC })
                {
                    using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                    using (CryptoStream securityStream = new CryptoStream(memoryStream, symmetricKey.CreateDecryptor(
                        new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations).GetBytes(keySize / 8),
                        Encoding.ASCII.GetBytes(initialVector)), CryptoStreamMode.Read))
                    {
                        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                        memoryStream.Close();
                        securityStream.Close();

                        return Encoding.UTF8.GetString(plainTextBytes, 0, securityStream.Read(plainTextBytes, 0, plainTextBytes.Length));
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, pwd, saltValue, hash, iterationRef, vector, sizeKey);
        }

        public static string Encrypt(string plainText, string keyWord)
        {
            return Encrypt(plainText, keyWord, saltValue, hash, iterationRef, vector, sizeKey);
        }

        public static string Encrypt(string plainText, string password, string saltValue, string hashAlgorithm, int passwordIterations, string initialVector, int keySize)
        {
            try
            {
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                using (RijndaelManaged symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC })
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream securityStream = new CryptoStream(memoryStream, symmetricKey.CreateEncryptor(
                    new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations).GetBytes(keySize / 8),
                    Encoding.ASCII.GetBytes(initialVector)), CryptoStreamMode.Write))
                {
                    securityStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    securityStream.FlushFinalBlock();

                    byte[] cipherTextBytes = memoryStream.ToArray();
                    memoryStream.Close();
                    securityStream.Close();

                    return Convert.ToBase64String(cipherTextBytes);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}