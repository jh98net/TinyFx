using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx
{
    public static class TinyFxHostExtensions
    {
        public static IHost UseTinyFxEx(this IHost host)
        {
            DIUtil.SetServiceProvider(host.Services);
            return host;
        }
    }
}
