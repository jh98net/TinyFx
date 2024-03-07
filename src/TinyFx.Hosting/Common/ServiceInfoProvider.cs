using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.Hosting.Common
{
    internal class HostIpGetter
    {
        private List<string> _envs = new List<string>()
        {
            "ENV_HOST_IP"
        };
        public string Get()
        {
            string ret = null;
            foreach (var env in _envs)
            {
                ret = Environment.GetEnvironmentVariable(env);
                if (!string.IsNullOrEmpty(ret))
                    break;
            }
            if (string.IsNullOrEmpty(ret))
                ret = GetECSHostIp().GetTaskResult();

            if (string.IsNullOrEmpty(ret))
                ret = NetUtil.GetLocalIP();
            return ret;
        }

        #region GetECSHostIp
        private async Task<string> GetECSHostIp()
        {
            string ret = null;
            try
            {
                var url = Environment.GetEnvironmentVariable("ECS_CONTAINER_METADATA_URI");
                if (!string.IsNullOrEmpty(url))
                {
                    var client = new HttpClient();
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
    internal class HostPortGetter
    {
        private List<string> _envs = new List<string>()
        {
            "ENV_HOST_PORT",
            "HTTP_PORTS",
            "DOTNET_HTTP_PORTS",
            "ASPNETCORE_HTTP_PORTS",
        };
        private const string _envUrls = "ASPNETCORE_URLS";
        public int Get()
        {
            var ret = 0;
            foreach (var env in _envs)
            {
                var port = Environment.GetEnvironmentVariable(env);
                if (!string.IsNullOrEmpty(port))
                {
                    ret = port.Split(';').FirstOrDefault("0").ToInt32();
                    if (ret > 0) break;
                }
            }
            if (ret <= 0)
            {
                var port = Environment.GetEnvironmentVariable(_envUrls);
                if (!string.IsNullOrEmpty(port))
                {
                    ret = port.Split(":").LastOrDefault("0").ToInt32();
                }
            }
            return ret;
        }
    }
}
