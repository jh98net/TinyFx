using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Configuration;

namespace TinyFx.Net
{
    public class JsonHttpClientSection : ConfigSection
    {
        public override string SectionName => "JsonHttpClient";

        public Dictionary<string, JsonHttpClientElement> Clients = new();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            Clients = configuration
                .Get<Dictionary<string, JsonHttpClientElement>>() ?? new();
            Clients.ForEach(x =>
            {
                x.Value.RequestHeaders = configuration.GetSection($"{x.Key}:RequestHeaders")
                    .Get<Dictionary<string, string>>() ?? new();
                x.Value.Settings = configuration.GetSection($"{x.Key}:Settings")
                    .Get<Dictionary<string, string>>() ?? new();
            });
        }
        public JsonHttpClientElement GetElement(string name)
            => Clients.TryGetValue(name, out var ret) ? ret : null;

        public string GetSettingValue(string name, string key)
        {
            return Clients.TryGetValue(name, out var value)
                && value.Settings.TryGetValue(key, out var ret)
                ? ret : null;
        }
    }
    public class JsonHttpClientElement
    {
        public string BaseAddress { get; set; }
        public int Timeout { get; set; } = 30000;
        public int Retry { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; } = new();
        public Dictionary<string, string> Settings = new();
    }
}
