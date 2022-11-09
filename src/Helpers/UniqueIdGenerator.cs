using Newtonsoft.Json;
using System;

namespace DevUtils.Helpers
{
    public static class UniqueIdGenerator
    {
        private static readonly string _encode32Chars = "0123456789abcdefghjkmnpqrstvwxyz";
        private static readonly string _encode54Chars = "0123456789ABCDEFGHJKMNPQRSTVWXYZabcdefghjkmnpqrstvwxyz";

        public static string GenerateBase32()
        {
            var guid = Guid.NewGuid().ToByteArray();

            var hs = BitConverter.ToInt64(guid, 0);
            var ls = BitConverter.ToInt64(guid, 8);

            return ToBase32(hs, ls);
        }

        public static string GenerateInBase(char[] encodeChars)
        {
            var guid = Guid.NewGuid().ToByteArray();

            var hs = BitConverter.ToUInt64(guid, 0);
            var ls = BitConverter.ToUInt64(guid, 8);

            var hsStr = LongToString(hs, encodeChars);
            var lsStr = LongToString(ls, encodeChars);

            return hsStr + lsStr;
        }

        public static string GenerateBase54()
        {
            return GenerateInBase(_encode54Chars.ToCharArray());
        }

        private static string LongToString(ulong value, char[] baseChars)
        {
            var i = 64;
            var buffer = new char[i];
            var targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % (ulong)targetBase];
                value /= (ulong)targetBase;
            }
            while (value > 0);
            var result = new char[64 - i];
            Array.Copy(buffer, i, result, 0, 64 - i);
            return new string(result);
        }

        private static string ToBase32(long hs, long ls)
        {
            var charBuffer = new char[26];

            charBuffer[0] = _encode32Chars[(int)(hs >> 60) & 31];
            charBuffer[1] = _encode32Chars[(int)(hs >> 55) & 31];
            charBuffer[2] = _encode32Chars[(int)(hs >> 50) & 31];
            charBuffer[3] = _encode32Chars[(int)(hs >> 45) & 31];
            charBuffer[4] = _encode32Chars[(int)(hs >> 40) & 31];
            charBuffer[5] = _encode32Chars[(int)(hs >> 35) & 31];
            charBuffer[6] = _encode32Chars[(int)(hs >> 30) & 31];
            charBuffer[7] = _encode32Chars[(int)(hs >> 25) & 31];
            charBuffer[8] = _encode32Chars[(int)(hs >> 20) & 31];
            charBuffer[9] = _encode32Chars[(int)(hs >> 15) & 31];
            charBuffer[10] = _encode32Chars[(int)(hs >> 10) & 31];
            charBuffer[11] = _encode32Chars[(int)(hs >> 5) & 31];
            charBuffer[12] = _encode32Chars[(int)hs & 31];

            charBuffer[13] = _encode32Chars[(int)(ls >> 60) & 31];
            charBuffer[14] = _encode32Chars[(int)(ls >> 55) & 31];
            charBuffer[15] = _encode32Chars[(int)(ls >> 50) & 31];
            charBuffer[16] = _encode32Chars[(int)(ls >> 45) & 31];
            charBuffer[17] = _encode32Chars[(int)(ls >> 40) & 31];
            charBuffer[18] = _encode32Chars[(int)(ls >> 35) & 31];
            charBuffer[19] = _encode32Chars[(int)(ls >> 30) & 31];
            charBuffer[20] = _encode32Chars[(int)(ls >> 25) & 31];
            charBuffer[21] = _encode32Chars[(int)(ls >> 20) & 31];
            charBuffer[22] = _encode32Chars[(int)(ls >> 15) & 31];
            charBuffer[23] = _encode32Chars[(int)(ls >> 10) & 31];
            charBuffer[24] = _encode32Chars[(int)(ls >> 5) & 31];
            charBuffer[25] = _encode32Chars[(int)ls & 31];

            return new string(charBuffer);
        }

        public static string GenerateBase64()
        {
            var guid = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(guid);
        }
    }
}