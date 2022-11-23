using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.ShortId
{
    internal static class RandomUtils
    {
        private static readonly Random Random = new Random();
        private static readonly object ThreadLock = new object();

        public static int GenerateNumberInRange(int min, int max)
        {
            lock (ThreadLock)
            {
                return Random.Next(min, max);
            }
        }
    }

}
