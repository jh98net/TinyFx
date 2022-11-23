using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;

namespace TinyFx.Extensions.DotNetty
{
    public class ClientOptions: IOnlyKeyConfigElement
    {
        public string Name { get; set; }
        public ProtocolMode Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public bool Ssl { get; set; }

        public string GetConfigElementKey()
        {
            return Name;
        }
    }
}
