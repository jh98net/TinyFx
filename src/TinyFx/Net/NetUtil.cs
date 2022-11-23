using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using TinyFx.Configuration;

namespace TinyFx.Net
{
    /// <summary>
    /// 网络通用类
    /// </summary>
    public static class NetUtil
    {
        #region IP 操作
        /// <summary>
        /// 转换long类型的IP值为字符串类型的IP地址
        /// </summary>
        /// <param name="ip">long类型的IP值</param>
        /// <returns></returns>
        public static string GetIpString(long ip)
            => new IPAddress(ip).ToString();

        /// <summary>
        /// 转换字符串类型的IP地址为long类型的值
        /// </summary>
        /// <param name="ip">IP地址，如：127.0.0.1</param>
        /// <returns></returns>
        public static long GetIpLong(string ip)
        {
            long ret = 0;
            byte[] data = IPAddress.Parse(ip).GetAddressBytes();
            for (int i = 0; i < data.Length; i++)
            {
                ret += data[i] * (long)Math.Pow(256, i);
            }
            return ret;
        }

        /// <summary>
        /// 获取IP地址类型
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public static IpAddressMode GetIpMode(string ip)
            => IpAddressParser.GetIpMode(ip);

        /// <summary>
        /// 获取本机 IPV4 集合
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalIPs()
        {
            var ret = new List<string>();
            var ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in ipEntry.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ret.Add(ip.ToString());
            }
            return ret;
        }

        /// <summary>
        /// 获取一个IPv4 地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            //获取所有网卡
            NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
            //遍历数组
            foreach (var network in networks)
            {
                //单个网卡的IP对象
                IPInterfaceProperties ip = network.GetIPProperties();
                GatewayIPAddressInformationCollection gateways = ip.GatewayAddresses;
                if (gateways.Count == 0) continue;
                return ip.UnicastAddresses.Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
                        .FirstOrDefault()?.Address.ToString();
            }
            return networks.Select(p => p.GetIPProperties())
              .SelectMany(p => p.UnicastAddresses)
              .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
              .FirstOrDefault()?.Address.ToString();
        }
        /// <summary>
        /// 获得本地IP地址集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IPAddress> GetLocalIPAddressCollection()
        {
            var ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in ipEntry.AddressList)
            {
                yield return ip;
            }
        }
        #endregion

        /// <summary>
        /// 检测监听的端口通讯是否正常
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool TestListenPort(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            try
            {
                var point = new IPEndPoint(ip, port);
                using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.Connect(point);
                    sock.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断文件类型是否为WEB格式图片(注：JPG,GIF,BMP,PNG)
        /// </summary>
        /// <param name="contentType">HTTP MIME 类型</param>
        /// <returns></returns>
        public static bool IsWebImage(string contentType)
            => contentType == "image/pjpeg" || contentType == "image/jpeg" || contentType == "image/gif"
            || contentType == "image/bmp" || contentType == "image/png" || contentType == "image/x-png";

        /// <summary>
        /// StatusCode是否成功
        ///     1xx - 信息输出
        ///     2xx - 成功输出
        ///     3xx - 跳转
        ///     4xx - 客户端错误
        ///     5xx - 服务端异常
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static bool IsSuccessStatusCode(int statusCode)
            => statusCode >= 200 && statusCode < 300;
    }

}
