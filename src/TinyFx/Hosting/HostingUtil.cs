using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Caching;
using TinyFx.Hosting.Services;

namespace TinyFx.Hosting
{
    public static class HostingUtil
    {
        #region ITinyFxHostLifetimeService
        /// <summary>
        /// 注册Host启动中事件
        /// </summary>
        /// <param name="func"></param>
        public static void RegisterStarting(Func<Task> func)
            => GetLifetimeService().RegisterStarting(func);
        /// <summary>
        /// 注册Host启动完毕事件
        /// </summary>
        /// <param name="func"></param>
        public static void RegisterStarted(Func<Task> func)
            => GetLifetimeService().RegisterStarted(func);
        /// <summary>
        /// 注册Host准备停止事件
        /// </summary>
        /// <param name="func"></param>
        public static void RegisterStopping(Func<Task> func)
            => GetLifetimeService().RegisterStopping(func);
        /// <summary>
        /// 注册Host已经停止事件
        /// </summary>
        /// <param name="func"></param>
        public static void RegisterStopped(Func<Task> func)
            => GetLifetimeService().RegisterStopped(func);

        public static readonly DefaultTinyFxHostLifetimeService LifetimeService = new();
        /// <summary>
        /// 获取Host生命周期事件注册服务
        /// </summary>
        /// <returns></returns>
        private static ITinyFxHostLifetimeService GetLifetimeService()
        {
            return LifetimeService;
            //var ret = DIUtil.GetService<ITinyFxHostLifetimeService>();
            //if (ret == null)
            //    throw new Exception("ITinyFxHostLifetimeService没有注入服务，请在配置服务ConfigureServices()里调用");
            //return ret;
        }
        #endregion

        #region ITinyFxHostTimerService
        /// <summary>
        /// 注册主机定时任务
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tryUpdate"></param>
        /// <returns></returns>
        public static bool RegisterTimer(TinyFxHostTimerItem item, bool tryUpdate = false)
            => GetTimerService().Register(item, tryUpdate);

        /// <summary>
        /// 注销主机定时任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool UnregisterTimer(string id)
            => GetTimerService().Unregister(id);

        /// <summary>
        /// 注册延迟任务
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="callback"></param>
        /// <param name="title"></param>
        public static void RegisterDelayTimer(TimeSpan delay, Func<CancellationToken, Task> callback, string title = null)
        {
            RegisterTimer(new TinyFxHostTimerItem
            {
                Title = title,
                ExecuteCount = 1,
                TryCount = 0,
                Interval = (int)delay.TotalMilliseconds,
                Callback = callback
            });
        }

        /// <summary>
        /// 获取Host定时任务服务
        /// </summary>
        /// <returns></returns>
        private static ITinyFxHostTimerService GetTimerService()
        {
            var ret = DIUtil.GetService<ITinyFxHostTimerService>();
            if (ret == null)
                throw new Exception("ITinyFxHostTimerService没有注入服务");
            return ret;
        }
        #endregion

        #region ITinyFxHostMicroService
        public static async Task<List<string>> GetAllServiceNames()
            => await DIUtil.GetRequiredService<ITinyFxHostMicroService>().GetAllServiceNames();

        /// <summary>
        /// 获取指定服务地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="isWebsocket"></param>
        /// <returns></returns>
        public static Task<string> SelectOneServiceUrl(string serviceName, bool isWebsocket = false)
            => DIUtil.GetRequiredService<ITinyFxHostMicroService>().SelectOneServiceUrl(serviceName, isWebsocket);
        #endregion

        #region ITinyFxHostRegDataService
        /// <summary>
        /// 设置主机数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Task SetHostData<T>(string key, T value)
            => GetRegDataService().SetHostData<T>(key, value);

        /// <summary>
        /// 获取主机数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Task<CacheValue<T>> GetHostData<T>(string key)
            => GetRegDataService().GetHostData<T>(key);
        public static ITinyFxHostRegDataService GetRegDataService()
        {
            var ret = DIUtil.GetService<ITinyFxHostRegDataService>();
            if (ret == null)
                throw new Exception("ITinyFxHostRegDataService没有注入服务");
            return ret;
        }
        #endregion
    }
}
