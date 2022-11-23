#if !NETSTANDARD2_0
using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace TinyFx.Security
{
    public static class RSAImportExtensions
    {
        /// <summary>
        /// 导入私钥
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="type"></param>
        /// <param name="privateKey"></param>
        /// <param name="isPem">仅对 PKCS#1 and PKCS#8 有效。true: privateKey中包含 "-----BEGIN XXX"</param>
        /// <param name="password">仅对 PKCS#8 有效</param>
        /// <returns></returns>
        public static void ImportPrivateKey(this RSA rsa, RSAKeyType type,string privateKey, bool isPem = false, string password=null)
        {
            if (isPem)
            {
                privateKey = PemFormatUtil.RemoveFormat(privateKey);
            }

            switch (type)
            {
                case RSAKeyType.Pkcs1:
                    rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey),out _);
                    break;
                case RSAKeyType.Pkcs8:
                    if (string.IsNullOrEmpty(password))
                        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
                    else
                        rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(privateKey), out _);
                    break;
                case RSAKeyType.Xml:
                    rsa.ImportXmlPrivateKey(privateKey);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// 导入公钥
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="type"></param>
        /// <param name="publicKey"></param>
        /// <param name="isPem">仅对 PKCS#1 and PKCS#8 有效。true: publicKey中包含 "-----BEGIN XXX"</param>
        /// <returns></returns>
        public static void ImportPublicKey(this RSA rsa, RSAKeyType type, string publicKey, bool isPem = false)
        {
            if (isPem)
            {
                publicKey = PemFormatUtil.RemoveFormat(publicKey);
            }

            switch (type)
            {
                case RSAKeyType.Pkcs1:
                    rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
                    break;
                case RSAKeyType.Pkcs8:
                    rsa.ImportPkcs8PublicKey(Convert.FromBase64String(publicKey));
                    break;
                case RSAKeyType.Xml:
                    rsa.ImportXmlPublicKey(publicKey);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void ImportPkcs8PublicKey(this RSA rsa,byte[] publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKey);
            var pub = new RSAParameters
            {
                Modulus = publicKeyParam.Modulus.ToByteArrayUnsigned(),
                Exponent = publicKeyParam.Exponent.ToByteArrayUnsigned()
            };
            rsa.ImportParameters(pub);
        }
    }

}
#endif