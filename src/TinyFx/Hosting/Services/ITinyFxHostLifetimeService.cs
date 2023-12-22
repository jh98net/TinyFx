using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFx.Hosting.Services
{
    public interface ITinyFxHostLifetimeService
    {
        List<Func<Task>> StartedEvents { get; }
        List<Func<Task>> StoppingEvents { get; }
        List<Func<Task>> StoppedEvents { get; }
        void RegisterStarted(Func<Task> func);
        void RegisterStopping(Func<Task> func);
        void RegisterStopped(Func<Task> func);
    }
    public class DefaultTinyFxHostLifetimeService : ITinyFxHostLifetimeService
    {
        public List<Func<Task>> StartedEvents { get; } = new();
        public List<Func<Task>> StoppingEvents { get; } = new();
        public List<Func<Task>> StoppedEvents { get; } = new();

        public void RegisterStarted(Func<Task> func)
        {
            StartedEvents.Add(func);
        }
        public void RegisterStopping(Func<Task> func)
        {
            StoppingEvents.Add(func);
        }
        public void RegisterStopped(Func<Task> func)
        {
            StoppedEvents.Add(func);
        }
    }
}
