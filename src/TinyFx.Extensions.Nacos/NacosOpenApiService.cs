using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.Extensions.Nacos
{
    public class NacosOpenApiService
    {
        public const string HTTP_CLIENT_NAME = "NacosOpenApi";
        private List<string> _serverUrls;
        public NacosOpenApiService() 
        {
            _serverUrls = NacosUtil.Section.ServerAddresses;
        }
        private HttpClient GetClient()
        {
            var client = DIUtil.GetRequiredService<IHttpClientFactory>()
                .CreateClient(HTTP_CLIENT_NAME);

            return client;
        }
    }
}
