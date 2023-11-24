using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Enums;
using Com.Ctrip.Framework.Apollo.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.Microsoft.Extensions.Configuration;
using Nacos.V2.DependencyInjection;
using Nacos.V2.Naming.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TinyFx.Logging;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 配置文件Util类
    /// </summary>
    public static class ConfigUtil
    {
        #region Properties

        private static readonly object _sync = new object();
        private static bool _isInited = false;
        public static ConfigFromMode FromMode { get; private set; } = ConfigFromMode.None;

        public static event EventHandler ConfigChange;
        private static void OnConfigChange()
        {
            Sections.Clear();
            _project = null;
            _env = null;
            ConfigChange?.Invoke(null, null);
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

        private static EnvironmentNames? _env;
        /// <summary>
        /// 当前程序运行环境
        /// </summary>
        public static EnvironmentNames Environment
        {
            get
            {
                if (_env != null) return _env.Value;

                var ret = EnvironmentNames.Unknown;
                if (!string.IsNullOrEmpty(Project.Environment))
                    ret = ParseEnvironmentName(Project.Environment);
                if (ret == EnvironmentNames.Unknown)
                    ret = ParseEnvironmentName(EnvironmentString);
                if (ret == EnvironmentNames.Unknown)
                    ret = EnvironmentNames.Production;
                _env = ret;
                return ret;
            }
        }
        /// <summary>
        /// 当前项目是否处于测试环境(Development,Testing)
        /// </summary>
        public static bool IsDebugEnvironment
            => Environment != EnvironmentNames.Unknown 
            && Environment != EnvironmentNames.Production 
            && Project.IsDebugEnvironment;
        public static bool IsStagingEnvironment
            => Environment == EnvironmentNames.Staging;
        #endregion

        #region Init
        /// <summary>
        /// 按照指定的环境初始化Config
        /// </summary>
        /// <param name="envString"></param>
        public static void Init(IHostBuilder hostBuilder, string envString = null)
        {
            EnvironmentString = envString;
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = System.Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(EnvironmentString))
                EnvironmentString = "Production";
            Sections.Clear();

            // configBuilder
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(AppContext.BaseDirectory);
            var files = GetConfigFiles(EnvironmentString);
            FromMode = files.Count == 0 ? ConfigFromMode.None : ConfigFromMode.ConfigFile;
            files.ForEach(file => configBuilder.AddJsonFile(Path.GetFileName(file), true, true));
            configBuilder.AddEnvironmentVariables();

            // hostBuilder
            if (hostBuilder != null)
            {
                var tmpConfig = configBuilder.Build();
                // apollo
                var hasApollo = tmpConfig.GetValue("Apollo:Enabled", false);
                if (hasApollo)
                {
                    var apollo = tmpConfig.GetSection("Apollo").Get<ApolloSection>();
                    if (string.IsNullOrEmpty(apollo.AppId))
                        apollo.AppId = tmpConfig.GetValue<string>("Project:ProjectId");
                    if (string.IsNullOrEmpty(apollo.AppId))
                        throw new Exception("Apollo配置必须配置AppId");
                    if (string.IsNullOrEmpty(apollo.MetaServer))
                        throw new Exception("Apollo配置必须配置MetaServer");
                    if (apollo.Namespaces == null || apollo.Namespaces.Count == 0)
                        throw new Exception("Apollo配置必须配置Namespaces");
                    FromMode = ConfigFromMode.Apollo;
                    LogManager.UseConsoleLogging(apollo.LogLevel);
                    configBuilder = new ConfigurationBuilder();
                    configBuilder.AddApollo(new ApolloOptions
                    {
                        AppId = apollo.AppId,
                        MetaServer = apollo.MetaServer,
                        ConfigServer = apollo.ConfigServer,
                        Namespaces = apollo.Namespaces,
                        CacheFileProvider = new ApolloCacheMyProvider()
                    });
                    configBuilder.AddEnvironmentVariables();
                }
                // Nacos
                var hasNacos = tmpConfig.GetValue("Nacos:Enabled", false);
                if (hasNacos)
                {
                    var failoverDir = tmpConfig.GetValue("Nacos:FailoverDir", "");
                    if (!string.IsNullOrEmpty(failoverDir))
                    {
                        var ns = tmpConfig.GetValue("Nacos:Namespace", "");
                        var file = Path.Combine(failoverDir, "nacos", "naming", ns, "failover", UtilAndComs.FAILOVER_SWITCH);
                        var path = Path.GetDirectoryName(file);
                        try
                        {
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            if (!File.Exists(file))
                                File.WriteAllText(file, "0");
                            System.Environment.SetEnvironmentVariable("JM.SNAPSHOT.PATH", failoverDir);
                        }
                        catch { }
                    }
                    // 是否启用config
                    var listeners = tmpConfig.GetSection("Nacos:Listeners").Get<List<ConfigListener>>();
                    if (listeners != null && listeners.Count > 0)
                    {
                        FromMode = ConfigFromMode.Nacos;
                        configBuilder = new ConfigurationBuilder();
                        configBuilder.AddConfiguration(tmpConfig, false);
                        configBuilder.AddNacosV2Configuration(tmpConfig.GetSection("Nacos"));
                        configBuilder.AddEnvironmentVariables();
                    }
                    // 是否启用naming
                    var clients = tmpConfig.GetSection("Nacos:Clients").Get<Dictionary<string, NacosClientElement>>();
                    if (clients != null && clients.Count > 0)
                    {
                        hostBuilder.ConfigureServices((context, services) =>
                        {
                            services.AddNacosV2Naming(context.Configuration, sectionName: "Nacos");
                        });
                    }
                }
                Configuration = configBuilder.Build();
                hostBuilder.ConfigureAppConfiguration((context, builder) =>
                {
                    context.Configuration = Configuration;
                    builder = configBuilder;
                });
            }
            else
            {
                Configuration = configBuilder.Build();
            }
            Configuration.GetReloadToken().RegisterChangeCallback((_) =>
            {
                LogUtil.Warning("配置更新: {changeTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                OnConfigChange();
            }, null);
            _isInited = true;
        }
        #endregion

        #region Sections
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
                    var proj = GetSection<ProjectSection>() ?? new ProjectSection();
                    if (string.IsNullOrEmpty(proj.ProjectId))
                    {
                        var nacos = GetSection<NacosSection>();
                        if (nacos != null && !string.IsNullOrEmpty(nacos.ServiceName))
                            proj.ProjectId = nacos.ServiceName;
                        else
                            proj.ProjectId = Assembly.GetEntryAssembly().GetName().Name;
                    }
                    _project = proj;
                }
                return _project;
            }
        }

        /// <summary>
        /// app自定义配置key/value数据
        /// </summary>
        public static AppSettingsSection AppSettings
            => GetSection<AppSettingsSection>() ?? new AppSettingsSection();
        /// <summary>
        /// app自定义配置类数据
        /// </summary>
        public static AppConfigsSection AppConfigs
            => GetSection<AppConfigsSection>() ?? new AppConfigsSection();

        /// <summary>
        /// 数据库配置信息
        /// </summary>
        public static DataSection Data => GetSection<DataSection>();
        #endregion

        #region Utils
        private static void CheckInit()
        {
            if (!_isInited)
            {
                lock (_sync)
                {
                    if (!_isInited)
                    {
                        Init(null, null);
                    }
                }
            }
        }
        private static Dictionary<string, EnvironmentNames> _envMapDic = new() {
            { "local", EnvironmentNames.Local},
            // dev
            { "dev", EnvironmentNames.Development},
            { "development",EnvironmentNames.Development },
            // sit
            { "testing",EnvironmentNames.Testing },
            { "sit",EnvironmentNames.Testing },
            { "test",EnvironmentNames.Testing },
            // fat
            { "fat",EnvironmentNames.QA },
            { "qa",EnvironmentNames.QA },
            // uat
            { "uat",EnvironmentNames.Staging },
            { "staging",EnvironmentNames.Staging },
            { "sim",EnvironmentNames.Staging },
            // pro
            { "pro",EnvironmentNames.Production },
            { "prod",EnvironmentNames.Production },
            { "production",EnvironmentNames.Production },
        };
        private static EnvironmentNames ParseEnvironmentName(string envString)
        {
            return _envMapDic.TryGetValue(envString.ToLower(), out var v)
                ? v : EnvironmentNames.Unknown;
        }
        private static Env MapApolloEnv(EnvironmentNames env)
        {
            switch (env)
            {
                case EnvironmentNames.Development:
                    return Env.Local;
                case EnvironmentNames.Testing:
                    return Env.Dev;
                case EnvironmentNames.QA:
                    return Env.Fat;
                case EnvironmentNames.Staging:
                    return Env.Uat;
                case EnvironmentNames.Production:
                    return Env.Pro;
                default:
                    return Env.Unknown;
            }
        }
        private static List<string> GetConfigFiles(string envString)
        {
            var ret = new List<string>();
            if (TryGetFile("appsettings.json", out var file))
                ret.Add(file);
            if (TryGetFile($"appsettings.{envString}.json", out file))
                ret.Add(file);
            else
            {
                if (TryGetFile($"appsettings.{envString.ToLower()}.json", out file))
                    ret.Add(file);
            }
            return ret;
            bool TryGetFile(string name, out string file)
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
        #endregion
    }
    public enum ConfigFromMode
    {
        None,
        ConfigFile,
        Apollo,
        Nacos
    }
}
