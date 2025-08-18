using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Shedding_Shield 
{
        //PasswordHasher class to handle password encryption and decryption
        public class PasswordHasher
        {
            private readonly byte[] key;
            private readonly byte[] iv;
            public PasswordHasher(string password)
            {
                // Generate a key and IV based on the password
                using (var sha256 = SHA256.Create())
                {
                    key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                // Use the first 16 bytes of the key as the IV
                iv = new byte[16];
                Array.Copy(key, iv, iv.Length);
            }

            //Encrypt methods to handle the encryption of passwords
            public string Encrypt(string plainText)
            {
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            //Decrypt methods to handle the decryption of passwords
            public string Decrypt(string cipherText)
            {
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))

                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

    } 
    