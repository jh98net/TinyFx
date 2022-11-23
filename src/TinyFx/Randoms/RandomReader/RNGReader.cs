using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Randoms
{
    public class RNGReader : RandomReaderBase
    {
        private RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        public override byte[] ReadBytes(int size)
        {
            var buffer = new byte[size];
            _rng.GetBytes(buffer);
            return buffer;
        }
        public override void Dispose()
        {
            _rng.Dispose();
        }
    }
}
