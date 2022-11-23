﻿#if !NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TinyFx.Security
{
    /// <summary>
    /// 公钥加密私钥解密
    /// 私钥签名公钥验证
    /// </summary>
    public static class EncryptExtensions
    {
        static readonly Dictionary<RSAEncryptionPadding,int> PaddingLimitDic=new Dictionary<RSAEncryptionPadding, int>()
        {
            [RSAEncryptionPadding.Pkcs1]=11,
            [RSAEncryptionPadding.OaepSHA1]=42,
            [RSAEncryptionPadding.OaepSHA256]=66,
            [RSAEncryptionPadding.OaepSHA384]=98,
            [RSAEncryptionPadding.OaepSHA512]=130,
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="dataStr"></param>
        /// <param name="padding"></param>
        /// <param name="connChar"></param>
        /// <returns></returns>
        public static string EncryptBigData(this RSA rsa,string dataStr, RSAEncryptionPadding padding,char connChar='$')
        {
            var data = Encoding.UTF8.GetBytes(dataStr);
            var modulusLength = rsa.KeySize / 8;
            var splitLength = modulusLength - PaddingLimitDic[padding];

            var sb=new StringBuilder();

            var splitsNumber = Convert.ToInt32(Math.Ceiling(data.Length * 1.0 / splitLength));

            var pointer = 0;
            for (int i = 0; i < splitsNumber; i++)
            {
                byte[] current = pointer + splitLength < data.Length ? data[pointer..(pointer+splitLength)] : data[pointer..];

                sb.Append(Convert.ToBase64String(rsa.Encrypt(current, padding)));
                sb.Append(connChar);
                pointer += splitLength;
            }

            return sb.ToString().TrimEnd(connChar);
        }

        public static string DecryptBigData(this RSA rsa, string dataStr, RSAEncryptionPadding padding, char connChar = '$')
        {
            var data = dataStr.Split(connChar, StringSplitOptions.RemoveEmptyEntries);
            var byteList = new List<byte>();

            foreach (var item in data)
            {
                byteList.AddRange(rsa.Decrypt(Convert.FromBase64String(item), padding));
            }

            return Encoding.UTF8.GetString(byteList.ToArray());
        }
    }
}
#endif