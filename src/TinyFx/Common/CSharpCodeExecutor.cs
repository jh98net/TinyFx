using System;
using Microsoft.CSharp;
using System.Collections.Specialized;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using TinyFx.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Diagnostics;

namespace TinyFx.Common
{
    /// <summary>
    /// C#代码动态编译执行
    /// </summary>
    public class CSharpCodeExecutor
    {
        private string _code;
        private bool _force;
        private List<MetadataReference> _references = new List<MetadataReference>();
        private Assembly _assembly;
        public CSharpCodeExecutor(string code)
        {
            _code = code;
            _force = true;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    _references.Add(MetadataReference.CreateFromFile(asm.Location));
                }
                catch { }
            }
        }
        public Assembly Assembly
        {
            get
            {
                Build();
                return _assembly;
            }
        }

        public void AddReferences(params MetadataReference[] references)
        {
            _references.AddRange(references);
        }

        /// <summary>
        /// 执行动态代码中的方法
        /// </summary>
        /// <param name="typeName">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">执行参数</param>
        /// <returns></returns>
        public object ExecuteStaticMethod(string typeName, string methodName, params object[] parameters)
        {
            var method = Assembly.CreateInstance(typeName).GetType().GetMethod(methodName);
            return method.Invoke(null, parameters);
        }
        public object ExecuteMethod(string typeName, string methodName, params object[] parameters)
        {
            var t = Assembly.GetType(typeName);
            var obj = Activator.CreateInstance(t);
            return t.InvokeMember(methodName, BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, parameters);
        }

        private void Build()
        {
            if (!_force && _assembly != null)
                return;
            // 随机程序集名称
            string assemblyName = Path.GetRandomFileName();
            // 丛代码中转换表达式树
            var syntaxTree = CSharpSyntaxTree.ParseText(_code);
            var compiler = CSharpCompilation.Create(assemblyName,
                new[] { syntaxTree },
                _references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using (var ms = new MemoryStream())
            {
                var result = compiler.Emit(ms);
                if (!result.Success || result.Diagnostics.FirstOrDefault(x => x.Severity > 0) != null)
                {
                    var msg = string.Join(Environment.NewLine, result.Diagnostics.Select(item => item.GetMessage()));
                    throw new Exception(msg);
                }
                ms.Seek(0, SeekOrigin.Begin);
                _force = false;
                _assembly = Assembly.Load(ms.ToArray());
            }
        }
    }
}
