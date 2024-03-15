using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Hosting
{
    public class TinyFxHostEndPoint
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public bool Secure { get; set; }
        public TinyFxHostEndPoint() { }
        public TinyFxHostEndPoint(string ip, int port, bool secure = false)
        {
            Ip = ip;
            Port = port;
            Secure = secure;
        }
        public override string ToString()
        {
            return Secure ? $"https://{Ip}:{Port}" : $"http://{Ip}:{Port}";
        }
    }

}
