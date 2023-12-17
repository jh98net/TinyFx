using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 外部配置源提供者
    /// </summary>
    public interface IConfigSourceProvider
    {
        IConfigurationBuilder CreateBuilder(IHostBuilder hostBuilder);
    }
    public abstract class BaseConfigSourceProvider: IConfigSourceProvider
    {
        public IConfiguration InitConfiguration { get; }
        public BaseConfigSourceProvider(IConfiguration config)
        {
            InitConfiguration = config;
        }

        public abstract IConfigurationBuilder CreateBuilder(IHostBuilder hostBuilder);
    }
}
