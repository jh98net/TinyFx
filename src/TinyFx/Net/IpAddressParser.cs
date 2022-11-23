using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.Net
{
    /// <summary>
    /// IP地址解析器
    /// 用于获取IP地址类型，整值，区域信息
    /// </summary>
    public class IpAddressParser
    {
        /// <summary>
        /// 获取当前的IP地址
        /// </summary>
        public string Ip { get; internal set; }

        private long _longValue;

        /// <summary>
        /// 获取IP对应的long值
        /// </summary>
        public long LongValue
        {
            get
            {
                if (_longValue == 0)
                    _longValue = NetUtil.GetIpLong(Ip);
                return _longValue;
            }
        }

        private IpAddressMode _ipMode;
        private bool _isSetIpMode = false;

        /// <summary>
        /// 获取IP地址类型
        /// </summary>
        public IpAddressMode IpMode
        {
            get
            {
                if (!_isSetIpMode)
                {
                    _ipMode = GetIpMode(Ip);
                    _isSetIpMode = true;
                }
                return _ipMode;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip"></param>
        public IpAddressParser(string ip)
        {
            Ip = (ip == "::1") ? "127.0.0.1" : ip;
        }

        #region GetIpMode
        private static List<IpModeValue> _ipModeCache;
        private static object _locker = new object();
        /// <summary>
        /// 获取IP地址类型
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public static IpAddressMode GetIpMode(string ip)
        {
            if (ip == "::1")
                return IpAddressMode.Loopback;
            if (!StringUtil.IsIpAddress(ip))
                return IpAddressMode.Unknown;
                //throw new ArgumentException("非法IP地址。", "ip");

            IpAddressMode ret = IpAddressMode.Unknown;
            if (_ipModeCache == null)
            {
                lock (_locker)
                {
                    if (_ipModeCache == null)
                    {
                        _ipModeCache = new List<IpModeValue>();
                        IpModeValue item = null;

                        item = new IpModeValue();
                        item.Min = GetIpModeLong("10.0.0.0");
                        item.Max = GetIpModeLong("10.255.255.255");
                        item.Mode = IpAddressMode.Intranet;
                        _ipModeCache.Add(item);

                        item = new IpModeValue();
                        item.Min = GetIpModeLong("172.16.0.0");
                        item.Max = GetIpModeLong("172.31.255.255");
                        item.Mode = IpAddressMode.Intranet;
                        _ipModeCache.Add(item);

                        item = new IpModeValue();
                        item.Min = GetIpModeLong("192.168.0.0");
                        item.Max = GetIpModeLong("192.168.255.255");
                        item.Mode = IpAddressMode.Intranet;
                        _ipModeCache.Add(item);

                        item = new IpModeValue();
                        item.Min = GetIpModeLong("127.0.0.1");
                        item.Max = GetIpModeLong("127.255.255.255");
                        item.Mode = IpAddressMode.Loopback;
                        _ipModeCache.Add(item);

                        item = new IpModeValue();
                        item.Min = GetIpModeLong("224.0.0.0");
                        item.Max = GetIpModeLong("239.255.255.255");
                        item.Mode = IpAddressMode.Multicast;
                        _ipModeCache.Add(item);
                    }
                }
            }
            long ipValue = GetIpModeLong(ip);
            foreach (IpModeValue value in _ipModeCache)
            {
                if (ipValue >= value.Min && ipValue <= value.Max)
                {
                    ret = value.Mode;
                    break;
                }
            }
            return ret;
        }
        private static long GetIpModeLong(string ip)
        {
            string[] arr = ip.Split('.');
            string ret = string.Empty;
            foreach (string item in arr)
            {
                ret += item.PadLeft(3, '0');
            }
            return long.Parse(ret);
        }

        /// <summary>
        /// IP值区间段对应的地址类型
        /// </summary>
        private class IpModeValue
        {
            /// <summary>
            /// 最小值
            /// </summary>
            public long Min { get; set; }

            /// <summary>
            /// 最大值
            /// </summary>
            public long Max { get; set; }

            /// <summary>
            /// IP地址类型
            /// </summary>
            public IpAddressMode Mode { get; set; }
        }
        #endregion

        /// <summary>
        /// 重写输出IP地址
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => Ip;
    }

    /// <summary>
    /// IP地址类型
    /// </summary>
    public enum IpAddressMode
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 公网地址
        /// </summary>
        Public,

        /// <summary>
        /// 内网地址
        /// 10.0.0.0 - 10.255.255.255 
        /// 172.16.0.0 - 172.31.255.255 
        /// 192.168.0.0 - 192.168.255.555  
        /// </summary>
        Intranet,

        /// <summary>
        /// 环回地址 127.0.0.1 - 127.255.255.254
        /// </summary>
        Loopback,

        /// <summary>
        /// 多播地址 224.0.0.0到239.255.255.255
        /// </summary>
        Multicast
    }
}
