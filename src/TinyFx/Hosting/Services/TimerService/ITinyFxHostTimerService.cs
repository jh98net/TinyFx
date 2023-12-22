using System.Threading;
using System.Threading.Tasks;

namespace TinyFx.Hosting.Services
{
    public interface ITinyFxHostTimerService
    {
        bool Register(TinyFxHostTimerItem item, bool tryUpdate = false);
        bool Unregister(string id);
        Task StartAsync(CancellationToken stoppingToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}