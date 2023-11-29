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
        private const string IP_RESOURCE = "TinyFx.Extensions.IP2Country.dbip-country-lite-2023-10.csv.gz";
        private static IP2CountryResolver _resolver = null;
        private static object _sync = new();
        private static IP2CountryResolver GetResolver()
        {
            if (_resolver == null)
            {
                lock (_sync)
                {
                    if (_resolver == null)
                    {
                        var section = ConfigUtil.GetSection<IP2CountrySection>();
                        if (!string.IsNullOrEmpty(section?.DbIpSource))
                        {
                            _resolver = new IP2CountryResolver(new DbIpCSVFileSource(section.DbIpSource));
                        }
                        else
                        {
                            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(IP_RESOURCE)!;
                            _resolver = new IP2CountryResolver(new DbIpCSVStreamSource(stream));
                        }
                    }
                }

            }
            return _resolver;
        }
        /// <summary>
        /// 根据ip返回国家编码2位大写（ISO 3166-1）
        /// </summary>
        /// <param name="ip">ipv4</param>
        /// <returns></returns>
        public static string GetContryId(string ip)
        {
            return GetResolver().Resolve(ip)?.Country;
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

            var section = ConfigUtil.GetSection<IP2CountrySection>();
            // 没配置或关闭
            if (!(section?.Enabled ?? false))
                return true;
            // 配置允许
            if (section.AllowIpDict.Contains(ip) || section.AllowIpDict.Contains("*"))
                return true;
            // 测试环境 内网环境
            if (ConfigUtil.IsDebugEnvironment || NetUtil.GetIpMode(ip) != IpAddressMode.External)
                return true;
            // 白名单
            if (allowIps != null && allowIps.Any(x => x == ip))
                return true;
            return GetContryId(ip) == country.ToUpper();
        }
    }
}
