using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TinyFx.Net;

namespace TinyFx.Text
{
    /// <summary>
    /// 时间戳ID序列生成器，产生唯一数
    /// 序列号生成格式：时间标识（默认12位yyMMddHHmmss）+ 序列累计数（默认4位，表示每单位时间可同时产生10000个序列号）+ 服务器标识（默认3位，取IP最后一位值）
    /// 通常用于生成数据库主键，注意每个数据库表必须创建且只创建一个实例用于生成它的ID，不能交叉使用，存在群集的通过ServerId区别，线程安全
    /// </summary>
    public class TimestampIDGenerator
    {
        /// <summary>
        /// 获取生成时间标识的格式yyyyMMddHHmmssffff
        /// </summary>
        public string DateFormat { get; private set; }
        /// <summary>
        /// 获取序列数长度
        /// </summary>
        public int SerialLength { get; private set; }
        /// <summary>
        /// 获取服务器标识
        /// </summary>
        public string ServerId { get; private set; }


        private object _locker = new object();
        private string _lastDate; //最后一次的日期值
        private int _lastSerialValue;//最后一次序列数
        private int _serialMaxValue; //序列数最大值

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dateFormat">获取生成时间标识的格式，默认yyMMddHHmmss</param>
        /// <param name="serialLength">获取序列数长度，默认4位</param>
        /// <param name="serverId">获取服务器标识，如果为null则取IP地址最后3位</param>
        public TimestampIDGenerator(string dateFormat = "yyMMddHHmmss", int serialLength = 4, string serverId = null)
        {
            DateFormat = dateFormat;
            SerialLength = serialLength;
            ServerId = serverId ?? GetServerId();

            _serialMaxValue = Convert.ToInt32(Math.Pow(10, SerialLength)) - 1;
        }

        //获得IP最后一位值
        private string GetServerId()
        {
            string ret = string.Empty;
            IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (IPAddress ip in ips)
            {
                string strIp = ip.ToString();
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && IpAddressParser.GetIpMode(strIp) == IpAddressMode.Intranet)
                {
                    ret = strIp.Substring(strIp.LastIndexOf('.') + 1);
                    break;
                }
            }
            return ret.PadLeft(3, '0');
        }
        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            lock (_locker)
            {
                string nowDate = DateTime.Now.ToString(DateFormat);
                if (nowDate != _lastDate)
                {
                    _lastDate = nowDate;
                    _lastSerialValue = -1;
                }
                if (_lastSerialValue >= _serialMaxValue)
                {
                    System.Threading.Thread.Sleep(100);
                    Generate();
                }
                else
                {
                    _lastSerialValue++;
                }
                return _lastDate + _lastSerialValue.ToString().PadLeft(SerialLength, '0') + ServerId;
            }
        }

        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <returns></returns>
        public long GenerateInt64()
            => long.Parse(Generate());
    }
}
