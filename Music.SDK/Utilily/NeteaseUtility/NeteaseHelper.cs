using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using XExten.Advance.LinqFramework;

namespace Music.SDK.Utilily.NeteaseUtility
{
    internal static class NeteaseHelper
    {
        private const string modulus = "00e0b509f6259df8642dbc35662901477df22677ec152b5ff68ace615bb7b725152b3ab17a876aea8a5aa76d2e417629ec4ee341f56135fccf695280104e0312ecbda92557c93870114af6c9d05c4f7f0c3685b7a46bee255932575cce10b424d813cfe4875d3e82047b97ddef52741d546b8e289dc6935b3ece0462db0a22b8e7";
        private const string nonce = "0CoJUm6Qyw8W8jud";
        private const string pubKey = "010001";
        private const string ivString = "0102030405060708";
        #region 私有拓展
        private static string ToRadixString(this BigInteger value, int radix)
        {
            if (radix <= 1 || radix > 36)
                throw new ArgumentOutOfRangeException(nameof(radix));
            if (value == 0)
                return "0";

            bool negative = value < 0;

            if (negative)
                value = -value;

            StringBuilder sb = new StringBuilder();

            for (; value > 0; value /= radix)
            {
                int d = (int)(value % radix);

                sb.Append((char)(d < 10 ? '0' + d : 'A' - 10 + d));
            }

            return (negative ? "-" : "") + string.Concat(sb.ToString().Reverse());
        }
        private static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
        private static string ToHex(this string hexstring)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char t in hexstring)
            {
                sb.Append(Convert.ToInt32(t).ToString("x"));
            }
            return sb.ToString();
        }
        #endregion

        #region 私有方法
        private static string RandomHexString(int digits)
        {
            Random random = new Random();
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result.ToLower();
            return result + random.Next(16).ToString("X").ToLower();
        }
        private static string AESEncrypt(string text, string password, string iv)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            rijndaelManaged.KeySize = 128;
            rijndaelManaged.BlockSize = 128;

            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelManaged.Key = keyBytes;

            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            rijndaelManaged.IV = ivBytes;

            ICryptoTransform iCryptoTransform = rijndaelManaged.CreateEncryptor();
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] targetBytes = iCryptoTransform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(targetBytes);
        }
        private static string RSAEncrypt(string text, string pubKey, string modulus)
        {
            string reversedText = text.Reverse();
            BigInteger baseNum = BigInteger.Parse(reversedText.ToHex(), NumberStyles.AllowHexSpecifier);
            BigInteger baseExp = BigInteger.Parse(pubKey, NumberStyles.AllowHexSpecifier);
            BigInteger basePow = BigInteger.Parse(modulus, NumberStyles.AllowHexSpecifier);
            BigInteger bigInteger = BigInteger.ModPow(baseNum, baseExp, basePow);
            return bigInteger.ToRadixString(16).PadLeft(256, '0').ToLower();
        }
        #endregion

        /// <summary>
        /// 加密请求参数
        /// </summary>
        /// <param name="originalData"></param>
        /// <returns></returns>
        internal static List<KeyValuePair<string, string>> EncryptRequest(this object originalData)
        {
            string secKey = RandomHexString(16);
            string encText = originalData.ToJson();
            encText = AESEncrypt(encText, nonce, ivString);
            encText = AESEncrypt(encText, secKey, ivString);
            string encSecKey = RSAEncrypt(secKey, pubKey, modulus);
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { "params", encText },
                { "encSecKey", encSecKey }
            };
            return dict.ToList();
        }
    }
}
