using System.Security.Cryptography;
using System.Text;

namespace Api.Infrastructure.Security.Helpers
{
    public static class HashingHelper
    {
        public static string Sha256(this string rawData)
        {
            using var hasher = SHA256.Create();
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }

        public static (string hashedString, string salt) Sha256WithSalt(this string rawData, string salt,
            bool useRandomSalt = false)
        {
            using var hasher = SHA256.Create();
            if (useRandomSalt || string.IsNullOrWhiteSpace(salt))
                salt = PasswordGenerator.Generate(rawData.Length);

            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes($"{rawData}{salt}"));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return (builder.ToString(), salt);
        }

        public static string Sha512(this string rawData)
        {
            using var hasher = SHA512.Create();
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }

        public static (string hashedString, string salt) Sha512WithSalt(this string rawData, string salt,
            bool useRandomSalt = false)
        {
            using var hasher = SHA512.Create();
            if (useRandomSalt || string.IsNullOrWhiteSpace(salt))
                salt = PasswordGenerator.Generate(rawData.Length);
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes($"{rawData}{salt}"));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return (builder.ToString(), salt);
        }

    }
}