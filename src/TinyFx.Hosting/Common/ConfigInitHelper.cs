using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Extensions.Nacos;

namespace TinyFx.Hosting.Common
{
    internal class ConfigInitHelper
    {
        private IHostBuilder _builder { get; }
        public string EnvString { get; }
        private FileConfigBuilder _fileConfigBuilder = new();
        public ConfigInitHelper(IHostBuilder builder, string envString)
        {
            _builder = builder;

            if (string.IsNullOrEmpty(envString))
                envString = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            if (string.IsNullOrEmpty(envString))
                envString = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(envString))
                envString = System.Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            EnvString = envString;
        }

        public IConfiguration GetConfiguration()
        {
            var fileConfig = _fileConfigBuilder.Build(EnvString);
            var builder = new NacosConfigSourceProvider().CreateConfigBuilder(_builder, fileConfig);

            var configuration = builder != null ? builder.Build() : fileConfig;
            return configuration;
        }
    }
}
