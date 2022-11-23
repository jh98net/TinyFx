#if !NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Security
{
    /// <summary>
    /// RSA辅助类
    /// </summary>
    public static class RSAUtil
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="source">源文本</param>
        /// <param name="keyType">密钥类型</param>
        /// <param name="privateKey">密钥</param>
        /// <param name="hashAlgorithm">加密类型</param>
        /// <param name="cipher">返回签名的编码类型</param>
        /// <param name="padding">填充模式和参数。默认:RSASignaturePadding.Pkcs1</param>
        /// <returns></returns>
        public static string Sign(string source, RSAKeyType keyType, string privateKey, HashAlgorithmName hashAlgorithm, CipherEncode cipher = CipherEncode.Base64, RSASignaturePadding padding = null)
        {
            var rsa = RSA.Create();
            rsa.ImportPrivateKey(keyType, privateKey);
            return rsa.SignData(source, hashAlgorithm, cipher, padding);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="source">源文本</param>
        /// <param name="sign">待验证的签名</param>
        /// <param name="keyType">密钥类型</param>
        /// <param name="publicKey">密钥</param>
        /// <param name="hashAlgorithm">加密类型</param>
        /// <param name="cipher">返回签名的编码类型</param>
        /// <param name="padding">填充模式和参数。默认:RSASignaturePadding.Pkcs1</param>
        /// <returns></returns>
        public static bool Verify(string source, string sign, RSAKeyType keyType, string publicKey, HashAlgorithmName hashAlgorithm, CipherEncode cipher = CipherEncode.Base64, RSASignaturePadding padding = null)
        {
            var rsa = RSA.Create();
            rsa.ImportPublicKey(keyType, publicKey);
            return rsa.VerifyData(source, sign, hashAlgorithm, cipher, padding);
        }
    }
}
#endif