using System.Reflection;
using TinyFx.Reflection;
using System.Collections;
using System.Collections.Concurrent;
using TinyFx.Collections;

namespace TinyFx.Demos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var demoId = string.Empty;
            demoId = "BuilderDemo";

            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsSubclassOf(typeof(DemoBase))
                        select t;
            var demos = new Dictionary<string, DemoBase>();
            types.ForEach(t =>
            {
                var demo = (DemoBase)ReflectionUtil.CreateInstance(t);
                demos.Add(demo.DemoId, demo);
            });
            while (true)
            {
                string input = null;
                if (!string.IsNullOrEmpty(demoId))
                {
                    input = demoId;
                    demoId = null;
                }
                else
                {
                    ConsoleEx.Write($"请输入DemoId(默认类名，q退出):", ConsoleColor.Yellow);
                    input = Console.ReadLine()?.Trim();
                }
                if (input?.ToLower() == "q")
                    break;
                if (!demos.ContainsKey(input))
                {
                    ConsoleEx.WriteLine("未找到此DemoId，请重新输入！", ConsoleColor.Red);
                    continue;
                }
                demos[input].Execute();
            }
        }
    }
    internal abstract class DemoBase
    {
        public string DemoId { get; }
        public abstract void Execute();
        public DemoBase()
        {
            DemoId = GetType().Name;
        }
    }
}