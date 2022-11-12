using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NETCore.Encrypt;
using System;
using System.Text.RegularExpressions;

namespace LearningPortal.Framework.Common.ExMethod
{
    public static class StringEx
    {
        public static string AesEncrypt(this string Str, string Key)
        {
            return EncryptProvider.AESEncrypt(Str, Key);
        }

        public static string AesDecrypt(this string Str, string Key)
        {
            return EncryptProvider.AESDecrypt(Str, Key);
        }

        public static string ToMd5(this string Str)
        {
            return EncryptProvider.Md5(Str);
        }

        public static string AesKeyMaker(this string Str)
        {
            return EncryptProvider.CreateAesKey().Key;
        }

        public static Guid ToGuid(this string Str)
        {
            if (Str is null)
                throw new ArgumentNullException("Value cant be null");

            return Guid.Parse(Str);
        }

        public static bool IsMatch(this string text, string Pattern, RegexOptions regexOptions = default)
        {
            return Regex.IsMatch(text, Pattern, regexOptions);
        }

        public static string ReplaceRegex(this string text, string Pattern, string NewText, RegexOptions regexOptions = RegexOptions.Singleline | RegexOptions.Multiline)
        {
            return Regex.Replace(text, Pattern, NewText, regexOptions);
        }
    }
}
