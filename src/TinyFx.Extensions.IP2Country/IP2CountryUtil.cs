using TinyFx.Extensions.IP2Country;
using TinyFx.Extensions.IP2Country.DbIp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace TinyFx.Extensions.IP2Country
{
    /// <summary>
    /// 
    /// 代码：https://github.com/RobThree/IP2Country
    /// IP库：https://db-ip.com/db/download/ip-to-country-lite
    /// </summary>
    public class IP2CountryUtil
    {
        private const string IP_RESOURCE = "TinyFx.Extensions.IP2Country.dbip-country-lite-2023-10.csv.gz";

        private static readonly IP2CountryResolver Resolver = new IP2CountryResolver
        (
            new DbIpCSVStreamSource
            (
                Assembly.GetExecutingAssembly().GetManifestResourceStream(IP_RESOURCE)!
            )
        );

        /// <summary>
        /// 根据ip返回国家编码2位大写（ISO 3166-1）
        /// </summary>
        /// <param name="ip">ipv4</param>
        /// <returns></returns>
        public static string GetContryId(string ip)
        {
            return Resolver.Resolve(ip)?.Country;

        }
    }
}
