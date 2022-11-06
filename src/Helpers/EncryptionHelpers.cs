//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.InteropServices;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using Api.Infrastructure.Extensions;
//using Microsoft.Extensions.Caching.Memory;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Encodings;
//using Org.BouncyCastle.Crypto.Engines;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.OpenSsl;
//using Org.BouncyCastle.Security;

//namespace Api.Infrastructure.Security.Helpers
//{
//    public static class EncryptionHelpers
//    {
//        public static async Task<string> GetPublicKey([Optional] IMemoryCache cache)
//        {
//            if (cache != null &&
//                cache.TryGetValue("card-pubKey", out var cacheEntry))
//                return cacheEntry.ToString();

//            var key = (await File.OpenText("./pem_public.pem").ReadToEndAsync())
//                    .Remove("-----BEGIN PUBLIC KEY-----\r\n")
//                    .Remove("\r\n-----END PUBLIC KEY-----")
//                    .Remove("\\r\\n")
//                    .Remove("\\r")
//                    .Remove("\\n")
//                ;

//            if (cache == null) return key;

//            // Key not in cache, so get data.
//            cacheEntry = DateTime.Now;

//            // Set cache options.
//            var cacheEntryOptions = new MemoryCacheEntryOptions()
//                // Keep in cache for this time, reset time if accessed.
//                .SetSlidingExpiration(TimeSpan.FromSeconds(3600));

//            // Save data in cache.
//            cache.Set("card-pubKey", cacheEntry, cacheEntryOptions);

//            return key;
//        }

//        public static string Decrypt(this string cipherText)
//        {
//            try
//            {
//                var cipherTextBytes = Convert.FromBase64String(cipherText);

//                var pr = new PemReader(
//                    (StreamReader) File.OpenText("./pem_private.pem")
//                );
//                var keys = (AsymmetricCipherKeyPair) pr.ReadObject();

//                // Pure mathematical RSA implementation
//                //RsaEngine eng = new RsaEngine();

//                // PKCS1 v1.5 paddings
//                var eng = new Pkcs1Encoding(new RsaEngine());

//                // PKCS1 OAEP paddings
//                //OaepEncoding eng = new OaepEncoding(new RsaEngine());
//                eng.Init(false, keys.Private);

//                var length = cipherTextBytes.Length;
//                var blockSize = eng.GetInputBlockSize();
//                var plainTextBytes = new List<byte>();
//                for (var chunkPosition = 0;
//                    chunkPosition < length;
//                    chunkPosition += blockSize)
//                {
//                    var chunkSize = Math.Min(blockSize, length - chunkPosition);
//                    plainTextBytes.AddRange(eng.ProcessBlock(
//                        cipherTextBytes, chunkPosition, chunkSize
//                    ));
//                }

//                return Encoding.UTF8.GetString(plainTextBytes.ToArray());
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public static string Encrypt(this string plainText)
//        {
//            try
//            {
//                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

//                var pr = new PemReader(
//                    (StreamReader) File.OpenText("./pem_public.pem")
//                );
//                var keys = (RsaKeyParameters) pr.ReadObject();

//                // Pure mathematical RSA implementation
//                //RsaEngine eng = new RsaEngine();

//                // PKCS1 v1.5 paddings
//                var eng = new Pkcs1Encoding(new RsaEngine());

//                // PKCS1 OAEP paddings
//                //var eng = new OaepEncoding(new RsaEngine());
//                eng.Init(true, keys);

//                var length = plainTextBytes.Length;
//                var blockSize = eng.GetInputBlockSize();
//                var cipherTextBytes = new List<byte>();
//                for (var chunkPosition = 0;
//                    chunkPosition < length;
//                    chunkPosition += blockSize)
//                {
//                    var chunkSize = Math.Min(blockSize, length - chunkPosition);
//                    cipherTextBytes.AddRange(eng.ProcessBlock(
//                        plainTextBytes, chunkPosition, chunkSize
//                    ));
//                }

//                return Convert.ToBase64String(cipherTextBytes.ToArray());
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public static string ConvertDerToPem(string publicKeyInDerFormat)
//        {
//            var pem = "-----BEGIN PUBLIC KEY-----\r\n" +
//                      Convert.ToBase64String(publicKeyInDerFormat.ToByte(), Base64FormattingOptions.InsertLineBreaks) +
//                      "-----END PUBLIC KEY-----";
//            return pem;
//        }

//        private static byte[] ToByte(this string value) => Encoding.UTF8.GetBytes(value);

//        public static string Encrypt(this string value, string publicKey)
//        {
//            using var rsa = new RSACryptoServiceProvider(2048);
//            try
//            {
//                // client encrypting data with public key issued by server
//                //
//                rsa.FromXmlString(publicKey);

//                var encryptedData = rsa.Encrypt(value.ToByte(), RSAEncryptionPadding.Pkcs1);
//                var base64Encrypted = Convert.ToBase64String(encryptedData, Base64FormattingOptions.None);
//                return base64Encrypted;
//            }
//            finally
//            {
//                rsa.PersistKeyInCsp = false;
//            }
//        }

//        public static string EncryptWithDer(this string plainText, string publicKeyDerBase64)
//        {
//            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

//            var publicKeyDerRestored = Convert.FromBase64String(publicKeyDerBase64);
//            var keys = (RsaKeyParameters) PublicKeyFactory.CreateKey(publicKeyDerRestored);


//            // Pure mathematical RSA implementation
//            //RsaEngine eng = new RsaEngine();

//            // PKCS1 v1.5 paddings
//            var eng = new Pkcs1Encoding(new RsaEngine());

//            // PKCS1 OAEP paddings
//            //var eng = new OaepEncoding(new RsaEngine());
//            eng.Init(true, keys);

//            var length = plainTextBytes.Length;
//            var blockSize = eng.GetInputBlockSize();
//            var cipherTextBytes = new List<byte>();
//            for (var chunkPosition = 0;
//                chunkPosition < length;
//                chunkPosition += blockSize)
//            {
//                var chunkSize = Math.Min(blockSize, length - chunkPosition);
//                cipherTextBytes.AddRange(eng.ProcessBlock(
//                    plainTextBytes, chunkPosition, chunkSize
//                ));
//            }

//            return Convert.ToBase64String(cipherTextBytes.ToArray());
//        }
//    }
//}