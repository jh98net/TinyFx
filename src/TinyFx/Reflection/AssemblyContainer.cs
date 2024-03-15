using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Logging;

namespace TinyFx.Reflection
{
    public interface IAssemblyContainer
    {
        Assembly GetAssembly(string assemblyName, string errorMsg = null);
        Type[] GetTypes(string assemblyName, string errorMsg = null);
    }

    public class AssemblyContainer : IAssemblyContainer
    {
        // key: xxx.dll
        private ConcurrentDictionary<string, Assembly> _assemblyCache = new();

        public Assembly GetAssembly(string assemblyName, string errorMsg = null)
        {
            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName), $"获取Assembly时assemblyName不能为空");

            return _assemblyCache.GetOrAdd(assemblyName, asmName =>
            {
                var ignore = !StringUtil.LetterChars.Contains(asmName[0]);//非字母开头，可忽略
                var file = ignore ? asmName.Substring(1) : asmName;
                if (!file.EndsWith(".dll"))
                    file += ".dll";

                if (!File.Exists(file))
                    file = Path.Combine(AppContext.BaseDirectory, file);
                if (File.Exists(file))
                    return Assembly.LoadFrom(file);
                var errMsg = $"未能获取Assembly。name:{asmName} file:{file} {errorMsg}";
                if (!ignore)
                    throw new Exception(errMsg);
                LogUtil.GetContextLogger()
                    .SetLevel(Microsoft.Extensions.Logging.LogLevel.Warning)
                    .AddField("AssemblyTypesContainer.Warning", errMsg)
                    .Save();
                return null;
            });
        }
        public Type[] GetTypes(string assemblyName, string errorMsg = null)
        {
            var assembly = GetAssembly(assemblyName, errorMsg);
            return assembly?.GetTypes() ?? new Type[0];
        }
    }
}
