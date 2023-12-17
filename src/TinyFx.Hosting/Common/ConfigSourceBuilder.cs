using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Configuration;
using TinyFx.Extensions.Nacos;
using TinyFx.Logging;

namespace TinyFx.Hosting.Common
{
    internal class ConfigSourceBuilder
    {
        private IHostBuilder _builder { get; }
        public string EnvString { get; }
        private FileConfigBuilder _fileConfigBuilder = new();
        public ConfigSourceBuilder(IHostBuilder builder, string envString)
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

        public IConfiguration Build()
        {
            IConfiguration ret = _fileConfigBuilder.Build(EnvString);
            var builder = CreateProvider(ret)?.CreateBuilder(_builder);
            if (builder != null)
            {
                ret = builder.Build();
            }
            else
            {
                LogUtil.Info($"配置管理 [加载文件配置源] appsettings.{EnvString}.json");
            }
            return ret;
        }

        private IConfigSourceProvider CreateProvider(IConfiguration fileConfig)
        {
            var provider = new NacosConfigSourceProvider(fileConfig);
            LogUtil.Info($"配置管理 [加载nacos配置源] ServerAddresses: {provider.GetServerAddresses(fileConfig)} Namespace: {provider.GetNamespace(fileConfig)}");
            return provider;
        }
    }
}
