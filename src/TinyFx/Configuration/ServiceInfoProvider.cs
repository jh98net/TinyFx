using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TinyFx.Configuration
{
    internal class ServiceInfoProvider
    {
        public const string ENV_HOST_IP = "ENV_HOST_IP";
        public const string ENV_HOST_PORT = "ENV_HOST_PORT";
        public string HostIp { get; set; }
        public int HostPort { get; set; }

        public static readonly string ServiceGuid = ObjectId.NewId();
        public string ServiceId { get; set; }
        public string ServiceUrl { get; set; }
        public TinyFxHostType HostType { get; set; } = TinyFxHostType.Unknow;

        public ServiceInfoProvider()
        {
            HostIp = Environment.GetEnvironmentVariable(ENV_HOST_IP);
            var port = Environment.GetEnvironmentVariable(ENV_HOST_PORT);
            if (!string.IsNullOrEmpty(port))
                HostPort = int.Parse(port);
        }

        #region GetECSHostIp
        private async Task<string> GetECSHostIp()
        {
            string ret = null;
            var client = new HttpClient();
            try
            {
                var url = Environment.GetEnvironmentVariable("ECS_CONTAINER_METADATA_URI");
                if (!string.IsNullOrEmpty(url))
                {
                    var rsp = await client.GetStringAsync($"{url}/task");
                    if (!string.IsNullOrEmpty(rsp))
                    {
                        var json = SerializerUtil.DeserializeJsonNet<ECSMetaData>(rsp);
                        if (json?.Containers?.Count > 0)
                        {
                            var container = json.Containers.OrderByDescending(x => x.StartedAt).First();
                            ret = container?.Networks?.FirstOrDefault()?.IPv4Addresses?.FirstOrDefault();
                        }
                    }
                }
            }
            catch
            {
            }
            return ret;
        }

        #region ECSMetaData
        class ECSMetaData
        {
            public List<ECSContainer> Containers { get; set; }
        }
        class ECSContainer
        {
            public string StartedAt { get; set; }
            public List<ECSNetworks> Networks { get; set; }
        }
        class ECSNetworks
        {
            public List<string> IPv4Addresses { get; set; }
        }
        #endregion
        #endregion
    }
    public enum TinyFxHostType
    {
        Unknow = 0,
        Console = 1,
        AspNet = 2,
        DotNetty = 3
    }
}
