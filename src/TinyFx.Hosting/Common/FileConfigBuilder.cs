using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Hosting.Common
{
    internal class FileConfigBuilder
    {
        public FileConfigBuilder()
        { }

        public IConfiguration Build(string envString)
        {
            IConfigurationBuilder ret = new ConfigurationBuilder();
            ret.SetBasePath(AppContext.BaseDirectory);
            var files = GetConfigFiles(envString);
            files.ForEach(file => ret.AddJsonFile(Path.GetFileName(file), true, true));
            ret.AddEnvironmentVariables();
            return ret.Build();
        }

        private List<string> GetConfigFiles(string envString)
        {
            var ret = new List<string>();
            if (TryGetFile("appsettings.json", out var file))
                ret.Add(file);
            if (!string.IsNullOrEmpty(envString))
            {
                if (TryGetFile($"appsettings.{envString}.json", out file))
                {
                    ret.Add(file);
                }
                else
                {
                    if (TryGetFile($"appsettings.{envString.ToLower()}.json", out file))
                        ret.Add(file);
                }
            }
            return ret;
        }
        private bool TryGetFile(string name, out string file)
        {
            file = Path.Combine(AppContext.BaseDirectory, name);
            if (File.Exists(file) && !string.IsNullOrEmpty(File.ReadAllText(file).Trim()))
                return true;

            file = Path.Combine(Directory.GetCurrentDirectory(), name);
            if (File.Exists(file) && !string.IsNullOrEmpty(File.ReadAllText(file).Trim()))
                return true;
            return false;
        }
    }
}
