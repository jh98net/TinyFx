using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Security
{
    /// <summary>
    /// RSA 秘钥格式
    /// </summary>
    public enum RSAKeyMode
    {
        /// <summary>
        /// 微软生成的Xml格式的秘钥
        /// </summary>
        MSXml,
        /// <summary>
        /// BEGIN RSA PRIVATE KEY
        /// RSA.ImportRSAPrivateKey()
        /// </summary>
        RSAPrivateKey,
        /// <summary>
        /// BEGIN PRIVATE KEY
        /// RSA.ImportPkcs8PrivateKey()
        /// </summary>
        PrivateKey,
        /// <summary>
        /// BEGIN RSA PUBLIC KEY
        /// RSA.ImportRSAPublicKey()
        /// </summary>
        RSAPublicKey,
        /// <summary>
        /// BEGIN PUBLIC KEY
        /// RSA.ImportSubjectPublicKeyInfo()
        /// </summary>
        PublicKey
    }
}
