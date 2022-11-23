using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TinyFx.Configuration
{
    /// <summary>
    /// tinyfx配置节接口
    /// </summary>
    public interface IConfigSection
    {
        /// <summary>
        /// 配置节名称
        /// </summary>
        string SectionName { get; }

        /// <summary>
        /// 绑定配置数据
        /// </summary>
        /// <param name="configuration"></param>
        void Bind(IConfiguration configuration);
        /// <summary>
        /// 配置文件修改时回调方法
        /// </summary>
        /// <param name="state"></param>
        void ChangeCallback(object state);
    }
    /// <summary>
    /// tinyfx配置节基类
    /// </summary>
    public abstract class ConfigSection : IConfigSection
    {
        /// <summary>
        /// 配置节名称
        /// </summary>
        public abstract string SectionName { get; }
        
        /// <summary>
        /// 配置信息
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// 绑定配置数据
        /// </summary>
        /// <param name="configuration"></param>

        public virtual void Bind(IConfiguration configuration)
        {
            Configuration = configuration;
            configuration?.Bind(this);
        }
        
        /// <summary>
        /// 配置文件修改时回调方法
        /// </summary>
        /// <param name="state"></param>
        public virtual void ChangeCallback(object state)
        { }

        protected Dictionary<string, T> BindDictionary<T>(IConfiguration configuration, string sectionName)
            where T : IOnlyKeyConfigElement
        {
            var ret = new Dictionary<string, T>();
            var items = configuration.GetSection(sectionName).Get<T[]>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    var key = item.GetConfigElementKey();
                    if (ret.ContainsKey(key))
                        throw new Exception($"配置文件中{SectionName}:{sectionName}重复。key: {key}");
                    ret.Add(key, item);
                }
            }
            return ret;
        }
    }
    public interface IOnlyKeyConfigElement
    {
        string GetConfigElementKey();
    }
    public class OnlyKeyConfigElement: IOnlyKeyConfigElement
    {
        public string Name { get; set; }
        public string GetConfigElementKey()
        {
            return Name;
        }
    }
}
