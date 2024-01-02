using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Text;

namespace TinyFx.Security
{
    public class BothKeyHelper
    {
        private string _constStr = "hNMmcYykGdCluYqe";
        private int[] _constIndexes = { 7, 1, 4, 15, 5, 2, 0, 8, 13, 14, 9, 12, 11, 10, 6, 3 };
        public BothKeyHelper(string constStr, int[] constIndexes)
        {
            _constStr = constStr;
            _constIndexes = constIndexes;
        }
        public bool Verify(string sourceKey, string sourceData, string sign)
        {
            var key = GetBothKey(sourceKey);
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(sourceData)));
            return hash == sign;
        }

        public string GetAccessKeyEncrypt(string sourceKey)
        {
            var key = GetBothKey(sourceKey);
            var accessKey = ObjectId.NewId();
            var ret = JsAesUtil.Encrypt(accessKey, key);
            return ret;
        }

        private string GetBothKey(string source)
            => SecurityUtil.GetBothKey(_constStr, _constIndexes, source);
    }
}
