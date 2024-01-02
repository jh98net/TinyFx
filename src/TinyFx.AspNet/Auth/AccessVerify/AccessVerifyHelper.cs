using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Security;
using TinyFx.Text;

namespace TinyFx.AspNet
{
    public interface IAccessVerifyHelper
    {
        string GetAccessKeyEncrypt(string sourceKey);
        bool VerifyAccessKey(string sourceKey, string sourceData, string sign);
        bool VerifyBothKey(string sourceKey, string sourceData, string sign);
    }

    public class AccessVerifyHelper : IAccessVerifyHelper
    {
        private string BOTH_KEY_SEED = "hNMmcYykGdCluYqe";
        private int[] BOTH_KEY_INDEXES = { 7, 1, 4, 15, 5, 2, 0, 8, 13, 14, 9, 12, 11, 10, 6, 3 };

        private string ACCESS_KEY_SEED = "vMjV3VFW3SyklQeQ";
        private int[] ACCESS_KEY_INDEXES = { 8, 11, 13, 12, 9, 7, 3, 14, 5, 2, 1, 0, 4, 6, 15, 10 };
        public AccessVerifyHelper()
        {
            var section = ConfigUtil.GetSection<AccessVerifySection>();
            if (!string.IsNullOrEmpty(section?.BothKeySeed))
                BOTH_KEY_SEED = section.BothKeySeed;
            if (!string.IsNullOrEmpty(section?.BothKeyIndexes))
            {
                BOTH_KEY_INDEXES = section.BothKeyIndexes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToInt32()).ToArray();
            }

            if (!string.IsNullOrEmpty(section?.AccessKeySeed))
                ACCESS_KEY_SEED = section.AccessKeySeed;
            if (!string.IsNullOrEmpty(section?.AccessKeyIndexes))
            {
                ACCESS_KEY_INDEXES = section.AccessKeyIndexes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToInt32()).ToArray();
            }
        }

        /// <summary>
        /// 验证BothKey签名
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <param name="sourceData"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool VerifyBothKey(string sourceKey, string sourceData, string sign)
        {
            var bothKey = GetKey(BOTH_KEY_SEED, BOTH_KEY_INDEXES, sourceKey);
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(bothKey));
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(sourceData)));
            return hash == sign;
        }

        /// <summary>
        /// 获取加密的AccessKey
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public string GetAccessKeyEncrypt(string sourceKey)
        {
            var bothKey = GetKey(BOTH_KEY_SEED, BOTH_KEY_INDEXES, sourceKey);
            var accessKey = GetAccessKey(sourceKey);
            var ret = JsAesUtil.Encrypt(accessKey, bothKey);
            return ret;
        }

        public string GetAccessKey(string sourceKey)
            => GetKey(ACCESS_KEY_SEED, ACCESS_KEY_INDEXES, sourceKey);
        public string GetAccessKey(string sourceKey, string decryptAccessKey)
        {
            var bothKey = GetKey(BOTH_KEY_SEED, BOTH_KEY_INDEXES, sourceKey);
            return JsAesUtil.Decrypt(decryptAccessKey, bothKey);
        }

        /// <summary>
        /// 验证AccessKey签名
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <param name="sourceData"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool VerifyAccessKey(string sourceKey, string sourceData, string sign)
        {
            var accessKey = GetKey(ACCESS_KEY_SEED, ACCESS_KEY_INDEXES, sourceKey);
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(accessKey));
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(sourceData)));
            return hash == sign;
        }

        /// <summary>
        /// 根据双方约定的算法获取key
        /// </summary>
        /// <param name="constSeed">约定的填充source的字符串</param>
        /// <param name="constIndexes">约定的获取key的索引序列</param>
        /// <param name="sourceKey">生成key的原始字符串</param>
        /// <returns></returns>
        private static string GetKey(string constSeed, int[] constIndexes, string sourceKey)
        {
            if (constSeed.Length < constIndexes.Length)
                throw new Exception("SecurityUtil.GetBothKey()时,约定的constStr长度必须大于等于constIndexes长度");
            var len = constIndexes.Length;
            var mod = sourceKey.Length % len;
            sourceKey += constSeed.Substring(0, len - mod);
            var max = sourceKey.Length / len;
            var ret = string.Empty;
            for (int i = 0; i < constIndexes.Length; i++)
            {
                var idx = i % max * len;
                ret += sourceKey[idx + constIndexes[i]];
            }
            return ret;
            /* TypeScript代码
                class BothKeyGenerator {
                  private _constStr = 'hNMmcYykGdCluYqe';
                  private _constIndexes = [7, 1, 4, 15, 5, 2, 0, 8, 13, 14, 9, 12, 11, 10, 6, 3];
                  private getBothKey(constStr: string, constIndexes: number[], source: string) {
                    var len = constIndexes.length;
                    var mod = source.length % len;
                    source += constStr.substring(0, len - mod);
                    var max = source.length / len;
                    var ret = '';
                    for (var i = 0; i < constIndexes.length; i++) {
                      var idx = (i % max) * len;
                      ret += source[idx + constIndexes[i]];
                    }
                    return ret;
                  }
                  public get(source: string) {
                    return this.getBothKey(this._constStr, this._constIndexes, source);
                  }
                }
            */
        }
    }
}
