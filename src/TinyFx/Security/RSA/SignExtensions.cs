#if !NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TinyFx.Security
{
    public static class SignExtensions
    {
        public static string SignData(this RSA rsa, string source, HashAlgorithmName hashAlgorithm, CipherEncode cipher = CipherEncode.Base64, RSASignaturePadding padding = null)
        {
            var data = rsa.SignData(Encoding.UTF8.GetBytes(source), hashAlgorithm, padding ?? RSASignaturePadding.Pkcs1);
            return SecurityUtil.CipherEncodeString(data, cipher);

        }
        public static bool VerifyData(this RSA rsa, string source, string sign, HashAlgorithmName hashAlgorithm, CipherEncode cipher = CipherEncode.Base64, RSASignaturePadding padding = null)
        {
            var signData = SecurityUtil.CipherDecodeString(sign, cipher);
            return rsa.VerifyData(Encoding.UTF8.GetBytes(source), signData, hashAlgorithm, padding ?? RSASignaturePadding.Pkcs1);
        }
    }
}
#endif