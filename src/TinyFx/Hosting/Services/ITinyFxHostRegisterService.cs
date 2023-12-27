using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Caching;

namespace TinyFx.Hosting.Services
{
    public interface ITinyFxHostRegisterService
    {
        Task Register();
        Task Unregister();
        Task Heartbeat();
        Task Health();

        /// <summary>
        /// 设置本机host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetData<T>(string field, T value);
        /// <summary>
        /// 获取本机host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        Task<CacheValue<T>> GetData<T>(string field);

        /// <summary>
        /// 获取所有host的serviceId
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        Task<List<string>> GetHosts(string connectionStringName = null);
        /// <summary>
        /// 设置指定serviceId的host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        Task SetHostData<T>(string serviceId, string field, T value, string connectionStringName = null);
        /// <summary>
        /// 获取指定serviceId的host数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceId"></param>
        /// <param name="field"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        Task<CacheValue<T>> GetHostData<T>(string serviceId, string field, string connectionStringName = null);
    }
}
