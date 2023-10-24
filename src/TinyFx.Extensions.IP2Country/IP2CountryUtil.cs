using TinyFx.Extensions.IP2Country;
using TinyFx.Extensions.IP2Country.DbIp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using TinyFx.Configuration;
using TinyFx.Net;

namespace TinyFx.Extensions.IP2Country
{
    /// <summary>
    /// 
    /// 代码：https://github.com/RobThree/IP2Country
    /// IP库：https://db-ip.com/db/download/ip-to-country-lite
    /// </summary>
    public class IP2CountryUtil
    {
        #region Init
        private const string IP_RESOURCE = "TinyFx.Extensions.IP2Country.dbip-country-lite-2023-10.csv.gz";

        private static readonly IP2CountryResolver Resolver = new IP2CountryResolver
        (
            new DbIpCSVStreamSource
            (
                Assembly.GetExecutingAssembly().GetManifestResourceStream(IP_RESOURCE)!
            )
        );
        static IP2CountryUtil()
        {
            var section = ConfigUtil.GetSection<IP2CountrySection>();
            if (!string.IsNullOrEmpty(section?.DbIpSource))
            {
                Resolver = new IP2CountryResolver(new DbIpCSVFileSource(section.DbIpSource));
            }
            else
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(IP_RESOURCE)!;
                Resolver = new IP2CountryResolver(new DbIpCSVStreamSource(stream));
            }
        }
        #endregion

        /// <summary>
        /// 根据ip返回国家编码2位大写（ISO 3166-1）
        /// </summary>
        /// <param name="ip">ipv4</param>
        /// <returns></returns>
        public static string GetContryId(string ip)
        {
            return Resolver.Resolve(ip)?.Country;
        }

        /// <summary>
        /// 检测访问ip是否属于指定国家--
        /// 忽略：测试环境，内网环境，白名单
        /// </summary>
        /// <param name="ip">用户IP</param>
        /// <param name="country">2位大写（ISO 3166-1）</param>
        /// <param name="allowIps">百名单</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CheckCountryIp(string ip, string country, params string[] allowIps)
        {
            if (country.Length != 2)
                throw new Exception("国家编码仅支持2位大写（ISO 3166-1）");
            // 测试环境 内网环境
            if (ConfigUtil.IsDebugEnvironment || NetUtil.GetIpMode(ip) != IpAddressMode.External)
                return true;
            // 白名单
            if (allowIps != null && allowIps.Any(x => x == ip))
                return true;
            var section = ConfigUtil.GetSection<IP2CountrySection>();
            if (section != null && (section.AllowIpDict.Contains(ip) || section.AllowIpDict.Contains("*")))
                return true;
            return GetContryId(ip) == country.ToUpper();
        }
    }
}
