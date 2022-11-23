using System;
using System.Linq;
using System.Text;

namespace TinyFx.ShortId
{
    /// <summary>
    /// 生成不重复短随机数ID辅助类
    /// </summary>
    public static class ShortIdUtil
    {
        // app variables
        private static Random _random = new Random();
        private const string Bigs = "ABCDEFGHIJKLMNPQRSTUVWXY";
        private const string Smalls = "abcdefghjklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string Specials = "_-";
        private static string _pool = $"{Smalls}{Bigs}";

        private static readonly object ThreadLock = new object();

        /// <summary>
        /// 生成随机字符串以匹配指定的选项。长度范围8-14位
        /// </summary>
        /// <param name="options">选项</param>
        /// <returns>A random string.</returns>
        /// <exception cref="ArgumentNullException">选项为空时抛出</exception>
        /// <exception cref="ArgumentException">options.Length 小于 8 时抛出。</exception>
        public static string Generate(GenerationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.Length < Constants.MinimumAutoLength)
            {
                throw new ArgumentException(
                    $"The specified length of {options.Length} is less than the lower limit of {Constants.MinimumAutoLength} to avoid conflicts.");
            }

            var characterPool = _pool;
            var poolBuilder = new StringBuilder(characterPool);
            if (options.UseNumbers)
            {
                poolBuilder.Append(Numbers);
            }

            if (options.UseSpecialCharacters)
            {
                poolBuilder.Append(Specials);
            }

            var pool = poolBuilder.ToString();

            var output = new char[options.Length];
            for (var i = 0; i < options.Length; i++)
            {
                lock (ThreadLock)
                {
                    var charIndex = _random.Next(0, pool.Length);
                    output[i] = pool[charIndex];
                }
            }

            return new string(output);
        }

        /// <summary>
        /// 更改生成ID的字符集
        /// </summary>
        /// <param name="characters">新的字符集</param>
        /// <exception cref="ArgumentException">当参数 <paramref name="characters"/> 为空.</exception>
        /// <exception cref="InvalidOperationException">当新字符集少于50个字符时抛</exception>
        public static void SetCharacters(string characters)
        {
            if (string.IsNullOrWhiteSpace(characters))
            {
                throw new ArgumentException("替换字符不能为空或空.");
            }

            var charSet = characters
                .ToCharArray()
                .Where(x => !char.IsWhiteSpace(x))
                .Distinct()
                .ToArray();

            if (charSet.Length < Constants.MinimumCharacterSetLength)
            {
                throw new InvalidOperationException(
                    $"替换字符的长度必须至少为 {Constants.MinimumCharacterSetLength} 个字母且不能有空格。");
            }

            lock (ThreadLock)
            {
                _pool = new string(charSet);
            }
        }

        /// <summary>
        /// 设置随机生成器使用的种子。
        /// </summary>
        /// <param name="seed">随机数生成器的种子</param>
        public static void SetSeed(int seed)
        {
            lock (ThreadLock)
            {
                _random = new Random(seed);
            }
        }

        /// <summary>
        /// 重置随机数生成器和字符集
        /// </summary>
        public static void Reset()
        {
            lock (ThreadLock)
            {
                _random = new Random();
                _pool = $"{Smalls}{Bigs}";
            }
        }
    }
}
