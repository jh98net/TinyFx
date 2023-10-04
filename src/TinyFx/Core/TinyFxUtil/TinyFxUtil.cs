using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TinyFx.Configuration;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx
{
    /// <summary>
    /// 提供TinyFx辅助方法
    /// </summary>
    public static partial class TinyFxUtil
    {

        #region RetryOnFault
        /// <summary>
        /// 尝试并等待检查函数返回true
        /// </summary>
        /// <param name="function"></param>
        /// <param name="maxTries"></param>
        /// <param name="interval"></param>
        /// <returns>true:检查函数返回true，false：在指定次数下没有返回true</returns>
        public static bool RetryAndWait(Func<bool> function, int maxTries, int interval)
        {
            for (int i = 0; i < maxTries; i++)
            {
                if (function())
                    return true;
                Thread.Sleep(interval);
            }
            return false;
        }

        /// <summary>
        /// 尝试执行指定次数，如仍然异常则抛出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function">执行的方法</param>
        /// <param name="maxTries">尝试次数</param>
        /// <param name="interval">尝试执行间隔，毫秒</param>
        /// <param name="faultCalcback">出现异常时回调方法</param>
        /// <returns></returns>
        public static T RetryOnFault<T>(Func<T> function, int maxTries, int interval, Action<int, Exception> faultCalcback)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    return function();
                }
                catch (Exception ex)
                {
                    faultCalcback?.Invoke(i, ex);
                    if (i == maxTries - 1)
                    {
                        ex.Data.Add("TinyFxUtil.RetryOnFault", $"调用方法{function.Method.Name}并重试{maxTries}次仍错误。");
                        throw;
                    }
                    Thread.Sleep(interval);
                }
            }
            return default;
        }
        /// <summary>
        /// 尝试执行指定次数，如仍然异常则抛出
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <param name="maxTries">尝试次数</param>
        /// <param name="interval">尝试执行间隔，毫秒</param>
        /// <param name="faultCalcback">出现异常时回调方法</param>
        /// <returns></returns>
        public static void RetryOnFault(Action action, int maxTries, int interval, Action<int, Exception> faultCalcback)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    faultCalcback?.Invoke(i, ex);
                    if (i == maxTries - 1)
                    {
                        ex.Data.Add("TinyFxUtil.RetryOnFault", $"调用方法{action.Method.Name}并重试{maxTries}次仍错误。");
                        throw;
                    }
                    Thread.Sleep(interval);
                }
            }
        }
        /// <summary>
        /// 尝试执行指定次数，如仍然异常则抛出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function">执行的方法</param>
        /// <param name="maxTries">尝试次数</param>
        /// <param name="interval">尝试执行间隔，毫秒</param>
        /// <param name="faultCalcback">出现异常时回调方法</param>
        /// <returns></returns>
        public static async Task<T> RetryOnFaultAsync<T>(Func<Task<T>> function, int maxTries, int interval, Action<int, Exception> faultCalcback)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    return await function().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    faultCalcback?.Invoke(i, ex);
                    if (i == maxTries - 1)
                    {
                        ex.Data.Add("TinyFxUtil.RetryOnFault", $"调用方法{function.Method.Name}并重试{maxTries}次仍错误。");
                        throw;
                    }
                    await Task.Delay(interval);
                }
            }
            return default;
        }

        /// <summary>
        /// 尝试执行指定次数，如仍然异常则抛出
        /// </summary>
        /// <param name="function">执行的方法</param>
        /// <param name="maxTries">尝试次数</param>
        /// <param name="interval">尝试执行间隔，毫秒</param>
        /// <param name="faultCalcback">出现异常时回调方法</param>
        public static async Task RetryOnFaultAsync(Func<Task> function, int maxTries, int interval, Action<int, Exception> faultCalcback)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    await function().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    faultCalcback?.Invoke(i, ex);
                    if (i == maxTries - 1)
                    {
                        ex.Data.Add("TinyFxUtil.RetryOnFault", $"调用方法{function.Method.Name}并重试{maxTries}次仍错误。");
                        throw;
                    }
                    await Task.Delay(interval);
                }
            }
        }
        #endregion

        #region Other
        private static ConcurrentDictionary<Type, bool> _nullableTypeCache = new ConcurrentDictionary<Type, bool>();
        /// <summary>
        /// 判断是否为可空类型
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            if (type == null)
                throw new ArgumentException("Type类型不能为NULL。");
            if (_nullableTypeCache.TryGetValue(type, out bool ret))
                return ret;
            ret = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
            _nullableTypeCache.TryAdd(type, ret);
            return ret;
        }


        /// <summary>
        /// 计算分页的页数
        /// </summary>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static long GetPageCount(long totalRecord, long pageSize)
            => (totalRecord % pageSize == 0) ? totalRecord / pageSize : totalRecord / pageSize + 1;
        #endregion

        /// <summary>
        /// 获取绝对路径。支持web相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path参数不能为空。");
            string ret = path;
            // 绝对路径
            if (Path.IsPathRooted(path))
                return path;
            if (path.StartsWith("~/"))
                return Path.Combine(AppContext.BaseDirectory, path.TrimStart("~/").Replace('/', '\\'));
            if (path.StartsWith("/"))
                return Path.Combine(AppContext.BaseDirectory, path.Replace('/', '\\'));
            return Path.Combine(AppContext.BaseDirectory, path.Replace('/', '\\'));
            //return Path.Combine(Environment.CurrentDirectory, path);
        }

        /// <summary>
        /// 获取应用程序Assembly所在目录,Web获取bin，其他获取执行程序当前目录
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyDirectory(bool isWebApp = false)
        {
            //string ret = Environment.CurrentDirectory;
            string ret = AppContext.BaseDirectory;
            if (isWebApp)
                ret = Path.Combine(ret, "bin");
            return ret;
        }

        /// <summary>
        /// 获取应用程序目录，web获取根目录，应用程序获取入口目录
        /// </summary>
        /// <returns></returns>
        public static string GetAppDirectory()
        {
            return AppContext.BaseDirectory;
        }

        /// <summary>
        /// 是否是Windows系统
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsOS
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsLinuxOS
            => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);


        public static T GetTaskResult<T>(this Task<T> task, bool isRunNewTask = false, bool continueOnCapturedContext = false)
        {
            return isRunNewTask
                ? Task.Run(() => task).ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult()
                : task.ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public static void GetTaskResult(this Task task, bool isRunNewTask = false, bool continueOnCapturedContext = false)
        {
            if (isRunNewTask)
                Task.Run(() => task).ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
            else
                task.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static void ClearCaching()
        {
            HttpClientExFactory.ClearClientCaching();
        }
    }
}
