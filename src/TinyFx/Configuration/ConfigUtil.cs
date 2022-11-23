using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Concurrent;
using TinyFx.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TinyFx.Configuration
{
    public delegate void ConfigChangeEvent();
    /// <summary>
    /// 配置文件Util类
    /// </summary>
    public static class ConfigUtil
    {
        #region Properties

        private static readonly object _locker = new object();
        private static bool _isInited = false;
        public static bool HasConfigFile { get; private set; }
        public static ConfigChangeEvent Change;
        private static void OnChange()
        {
            if (Change != null)
                Change();
        }

        /// <summary>
        /// TinyFx配置IConfiguration
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        /// TinyFx配置节集合
        /// </summary>
        public static readonly ConcurrentDictionary<string, object> Sections = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 配置的程序运行环境：Development/Testing/....
        /// </summary>
        public static string EnvironmentString { get; private set; }
        /// <summary>
        /// 当前程序运行环境
        /// </summary>
        public static EnvironmentNames Environment { get; private set; } = EnvironmentNames.Unknown;
        #endregion

        #region Init
        /// <summary>
        /// 按照指定的环境初始化Config
        /// </summary>
        /// <param name="envString"></param>
        public static void Init(string envString = null)
        {
            _isInited = true;
            EnvironmentString = envString;
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = System.Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = "Development";
            Environment = EnvironmentString.ToEnum(EnvironmentNames.Unknown);

            var files = new List<string>();
            files.AddRange(GetConfigFiles("tinyfx", EnvironmentString, out bool _));
            if (files.Count == 0)
                files.AddRange(GetConfigFiles("appsettings", EnvironmentString, out _));
            if (files.Count == 0)
            {
                HasConfigFile = false;
                LogUtil.Warning($"警告：应用程序没有配置文件!tinyfx.json 或 appsettings.json");
                Configuration = new ConfigurationBuilder().Build();
            }
            else
            {
                HasConfigFile = true;
                var configuration = GetConfiguration(files);
                Init(configuration);
            }
        }
        private static Dictionary<string, string> _envMapDic = new Dictionary<string, string> {
            { "dev","Development" },
            { "test","Testing" },
            { "prod","Production" }
        };
        private static List<string> GetConfigFiles(string name, string envString, out bool existEnvFile)
        {
            var ret = new List<string>();
            existEnvFile = false;
            var file = GetFile($"{name}.json");
            if (!string.IsNullOrEmpty(file))
                ret.Add(file);
            //
            if(_envMapDic.ContainsKey(envString))
                envString = _envMapDic[envString];
            file = GetFile($"{name}.{envString}.json");
            if (!string.IsNullOrEmpty(file))
            {
                ret.Add(file);
                existEnvFile = true;
            }
            else
            {
                file = GetFile($"{name}.{envString.ToLower()}.json");
                if (!string.IsNullOrEmpty(file))
                {
                    ret.Add(file);
                    existEnvFile = true;
                }
            }
            return ret;
        }
        private static string GetFile(string file)
        {
            var ret = Path.Combine(Directory.GetCurrentDirectory(), file);
            if (File.Exists(ret)) return ret;

            ret = Path.Combine(AppContext.BaseDirectory, file);
            if (File.Exists(ret)) return ret;
            return null;
        }
        private static IConfigurationRoot GetConfiguration(List<string> files)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppContext.BaseDirectory);
            foreach (var file in files)
                builder.AddJsonFile(Path.GetFileName(file), true, true);
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
        /*
        /// <summary>
        /// 使用自定义配置文件，初始化配置信息,默认当前目录中存在tinyfx.json配置文件
        /// </summary>
        /// <param name="configFile">tinyfx配置文件</param>
        /// <param name="environment">软件运行环境</param>
        public static void InitByConfigFile(string configFile, EnvironmentNames environment = EnvironmentNames.Development)
        {
            if (string.IsNullOrEmpty(configFile))
                throw new ArgumentNullException("configFile", "配置文件名不能为空");
            if (!File.Exists(configFile))
                throw new Exception($"tinyfx.json配置文件不存在： {configFile}");
            ConfigFile = configFile;
            var configuration = GetConfiguration(new List<string> { configFile } );
            Init(configuration, environment);
        }
        /// <summary>
        /// 使用系统配置文件的配置节，初始化配置信息
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        /// <param name="environment"></param>
        public static void InitByConfigSection(IConfiguration configuration, string sectionName = "TinyFx", EnvironmentNames environment = EnvironmentNames.Development)
        {
            var config = configuration.GetSection(sectionName);
            SectionName = sectionName;
            Init(config, environment);
        }
        */
        /// <summary>
        /// 初始化配置信息，使用以获取的IConfiguration对象
        /// </summary>
        /// <param name="configuration"></param>
        private static void Init(IConfiguration configuration)
        {
            lock (_locker)
            {
                Sections.Clear();
                Configuration = configuration;
                Configuration.GetReloadToken().RegisterChangeCallback((sections) =>
                {
                    var sets = (ConcurrentDictionary<string, IConfigSection>)sections;
                    OnChange();
                    sets.Clear();
                    _isInited = false;
                }, Sections);
            }
            _isInited = true;
        }
        #endregion

        #region Sections
        private static void CheckInit()
        {
            if (!_isInited) Init();
            //if (Configuration == null)
            //    throw new Exception("TinyFx配置没有初始化，请调用ConfigUtil.InitXXX();方法进行初始化。");
        }
        /// <summary>
        /// 获取配置节信息（T类型继承IConfigSection）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSection<T>() where T : class, IConfigSection, new()
        {
            CheckInit();
            var key = typeof(T).FullName;
            if (Sections.TryGetValue(key, out object value))
                return (T)value;
            T ret = null;
            if (Configuration != null)
            {
                var newItem = new T();
                var section = Configuration.GetSection(newItem.SectionName);
                if (section.Exists())
                {
                    newItem.Bind(section);
                    //newItem.ChangeCallback(null);
                    ret = newItem;
                }
            }
            Sections.TryAdd(key, ret);
            return ret;
        }

        /// <summary>
        /// 获取配置节信息（T可以是任意类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static T GetSection<T>(string sectionName)
        {
            CheckInit();
            var key = typeof(T).FullName;
            if (Sections.TryGetValue(key, out object value))
                return (T)value;
            var ret = default(T);
            if (Configuration != null)
            {
                var section = Configuration.GetSection(sectionName);
                if (section.Exists())
                {
                    ret = section.Get<T>();
                }
            }
            Sections.TryAdd(key, ret);
            return ret;
        }

        private static ProjectSection _project;
        /// <summary>
        /// 项目配置信息
        /// </summary>
        public static ProjectSection Project
        {
            get
            {
                if (_project == null)
                {
                    _project = GetSection<ProjectSection>() ?? new ProjectSection
                    {
                        ProjectId = Assembly.GetEntryAssembly().GetName().Name,
                        Description = "未定义项目"
                    };
                }
                return _project;
            }
        }

        private static AppSettingsSection _appSettingsSection;
        /// <summary>
        /// 项目自定义配置信息
        /// </summary>
        public static AppSettingsSection AppSettings
        {
            get
            {
                if (_appSettingsSection == null)
                    _appSettingsSection = GetSection<AppSettingsSection>() ?? new AppSettingsSection();
                return _appSettingsSection;
            }
        }

        /// <summary>
        /// 数据库配置信息
        /// </summary>
        public static DataSection Data => GetSection<DataSection>();
        #endregion
    }
}
