#if !NETSTANDARD2_0
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;

namespace TinyFx.Security
{
    public static class RSAExportExtensions
    {
        /// <summary>
        /// 导出私钥
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="type"></param>
        /// <param name="usePemFormat">仅对 PKCS#1 and PKCS#8 有效。true: 返回值包含 "-----BEGIN XXX"</param>
        /// <returns></returns>
        public static string ExportPrivateKey(this RSA rsa, RSAKeyType type, bool usePemFormat = false)
        {
            var key = type switch
            {
                RSAKeyType.Pkcs1 => Convert.ToBase64String(rsa.ExportRSAPrivateKey()),
                RSAKeyType.Pkcs8 => Convert.ToBase64String(rsa.ExportPkcs8PrivateKey()),
                RSAKeyType.Xml => rsa.ExportXmlPrivateKey(),
                _ => string.Empty
            };

            if (usePemFormat && type != RSAKeyType.Xml)
            {
                key = PemFormatUtil.GetPrivateKeyFormat(type, key);
            }

            return key;
        }

        /// <summary>
        /// 导出公钥
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="type"></param>
        /// <param name="usePemFormat">仅对 PKCS#1 and PKCS#8 有效。true: 返回值包含 "-----BEGIN XXX"</param>
        /// <returns></returns>
        public static string ExportPublicKey(this RSA rsa, RSAKeyType type, bool usePemFormat = false)
        {
            var key = type switch
            {
                RSAKeyType.Pkcs1 => Convert.ToBase64String(rsa.ExportRSAPublicKey()),
                RSAKeyType.Pkcs8 => Convert.ToBase64String(rsa.ExportPkcs8PublicKey()),
                RSAKeyType.Xml => rsa.ExportXmlPublicKey(),
                _ => string.Empty
            };

            if (usePemFormat && type != RSAKeyType.Xml)
            {
                key = PemFormatUtil.GetPublicKeyFormat(type, key);
            }

            return key;
        }

        public static byte[] ExportPkcs8PublicKey(this RSA rsa)
        {
            var pub = rsa.ExportParameters(false);
            RsaKeyParameters rsaKeyParameters = new RsaKeyParameters(false, new Org.BouncyCastle.Math.BigInteger(1, pub.Modulus), new Org.BouncyCastle.Math.BigInteger(1, pub.Exponent));
            StringWriter sw = new StringWriter();
            PemWriter pWrt = new PemWriter(sw);
            pWrt.WriteObject(rsaKeyParameters);
            pWrt.Writer.Close();
            return Convert.FromBase64String(PemFormatUtil.RemoveFormat(sw.ToString()));
        }
    }
}
#endif