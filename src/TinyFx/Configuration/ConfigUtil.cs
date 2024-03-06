using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using TinyFx.Configuration.Common;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.Configuration
{
    /// <summary>
    /// 配置文件Util类
    /// </summary>
    public static class ConfigUtil
    {
        #region Properties
        /// <summary>
        /// TinyFx配置IConfiguration
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        public static event EventHandler ConfigChanged;
        /// <summary>
        /// TinyFx配置节集合
        /// </summary>
        public static readonly ConcurrentDictionary<string, object> Sections = new ConcurrentDictionary<string, object>();

        #region Environment
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
                if (_env != null && _env.HasValue)
                    return _env.Value;

                var ret = EnvironmentNames.Unknown;
                var envParser = new EnvironmentNameParser();
                if (!string.IsNullOrEmpty(Project.Environment))
                    ret = envParser.Parse(Project.Environment);
                if (ret == EnvironmentNames.Unknown)
                    ret = envParser.Parse(EnvironmentString);
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

        /// <summary>
        /// 是否仿真环境
        /// </summary>
        public static bool IsStagingEnvironment
            => Environment == EnvironmentNames.Staging;
        #endregion

        /// <summary>
        /// Host服务信息
        /// </summary>
        public static readonly ServiceInfo ServiceInfo = new();
        #endregion

        #region Init
        public static void InitConfiguration(IConfiguration configuration, string envStr = null)
        {
            EnvironmentString = envStr;

            configuration.GetReloadToken().RegisterChangeCallback((_) =>
            {
                LogUtil.Warning("配置更新: {changeTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ClearCacheData();
                ConfigChanged?.Invoke(null, null);
            }, null);
            Configuration = configuration;
            ClearCacheData();

            ServiceInfo.ServiceId = $"{Project.ProjectId}:{ServiceInfo.ServiceGuid}";
            ServiceInfo.ServiceUrl ??= Project.ServiceUrl;
        }
        private static void ClearCacheData()
        {
            _env = null;
            _project = null;
            _host = null;
            _appSettings = null;
            _appConfigs = null;
            Sections.Clear();
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
                        proj.ProjectId = Assembly.GetEntryAssembly().GetName().Name;
                    }
                    if (string.IsNullOrEmpty(proj.ApplicationName))
                        proj.ApplicationName = proj.ProjectId;
                    _project = proj;
                }
                return _project;
            }
        }
        private static HostSection _host;
        public static HostSection Host
        {
            get
            {
                if (_host == null)
                {
                    _host = GetSection<HostSection>() ?? new HostSection();
                }
                return _host;
            }
        }

        private static AppSettingsSection _appSettings;
        /// <summary>
        /// app自定义配置key/value数据
        /// </summary>
        public static AppSettingsSection AppSettings
            => _appSettings ??= GetSection<AppSettingsSection>() ?? new AppSettingsSection();

        private static AppConfigsSection _appConfigs;
        /// <summary>
        /// app自定义配置类数据
        /// </summary>
        public static AppConfigsSection AppConfigs
            => _appConfigs ??= GetSection<AppConfigsSection>() ?? new AppConfigsSection();

        private static void CheckInit()
        {
            if (Configuration == null)
                throw new Exception("TinyFx应用程序配置没有初始化!");
        }
        #endregion
    }
}
