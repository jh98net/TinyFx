using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Text
{
    /// <summary>
    /// 生成双方约定的自定义Key算法
    /// </summary>
    public class BothKeyGenerator
    {
        private string _appendStr = "gH9sUbJq1NdBUbJ2bLmeLFaZUWwpv4sarN6r5kMYv6JU3kR1BwxQjBdzA5Zmyca7j0DrsQVGqBzk5TAbxp0AZ4a36idJrRmomXBT";
        private int[] _keyIndexs = { 7, 1, 4, 15, 5, 2, 0, 8, 13, 14, 9, 12, 11, 10, 6, 3 };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appendStr">添加的混淆字符串</param>
        /// <param name="keyIndexs">获取密钥的索引</param>
        public BothKeyGenerator(string appendStr, int[] keyIndexs)
        {
            _appendStr = appendStr;
            _keyIndexs = keyIndexs;
        }

        /// <summary>
        /// 生成BothKey
        /// </summary>
        /// <param name="stable">双方约定的固定拼接串</param>
        /// <returns></returns>
        public string Generate(string stable)
        {
            var len = _keyIndexs.Length;
            var mod = stable.Length % len;
            stable += _appendStr.Substring(0, len - mod);
            var max = stable.Length / len;
            var ret = string.Empty;
            for (int i = 0; i < _keyIndexs.Length; i++)
            {
                var idx = i % max * len;
                ret += stable[idx + _keyIndexs[i]];
            }
            return ret;
        }
    }
}
