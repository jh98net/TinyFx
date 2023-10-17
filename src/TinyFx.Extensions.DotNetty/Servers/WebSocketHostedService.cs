using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyFx.Common;
using TinyFx.Logging;
using TinyFx.Net;

namespace TinyFx.Extensions.DotNetty
{
    public class WebSocketHostedService : BackgroundService
    {
        private WebSocketServer _server;
        private ServerOptions _options;
        protected MultiTimerWorks TimerWorks { get; } = new MultiTimerWorks();
        protected AppSessionContainer Sessions;

        public WebSocketHostedService(IHostApplicationLifetime appLifetime)
        {
            _server = new WebSocketServer();
            _options = DIUtil.GetRequiredService<ServerOptions>();
            Sessions = DotNettyUtil.Sessions;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                RegisterCheckInvalidSessionWork();
                var _ = TimerWorks.StartAsync(stoppingToken);
                await _server.StartAsync();
            }
            catch (CustomException ex)
            {
                LogUtil.Error(ex, "DotNetty.WebSocketServer不应该抛出CustomException异常！");
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, "未处理异常:DotNetty.WebSocketServer！");
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await TimerWorks.StopAsync(cancellationToken);
            await _server.StopAsync();
            await base.StopAsync(cancellationToken);
        }
        private void RegisterCheckInvalidSessionWork()
        {
            if (_options.CheckSessionInterval > 0 && _options.CheckSessionTimeout > 0)
            {
                var work = new TimerWork
                {
                    WorkId = "CheckInvalidSessionWork",
                    Interval = _options.CheckSessionInterval,
                    Action = CheckInvalidSessionWork,
                };
                TimerWorks.RegisterWork(work);
            }
        }
        private Task CheckInvalidSessionWork(CancellationToken stoppingToken)
        {
            return Task.Run(() => {
                foreach (var session in Sessions.Find())
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;
                    if (!session.IsLogin && _options.CheckSessionTimeout > 0 && (DateTime.UtcNow - session.CreateTime).TotalMilliseconds > _options.CheckSessionTimeout)
                    {
                        session.Channel.WriteAndFlushAsync(GetInvalidSessionPacket());
                        session.Channel.CloseAsync();
                    }
                }
            });
        }
        private Packet _invalidSessionPacket;
        private Packet GetInvalidSessionPacket()
        {
            if (_invalidSessionPacket == null)
            {
                _invalidSessionPacket = DotNettyUtil.CreateExceptionPacket(ResponseCode.G_ServiceDenyConnect
                    , $"疑似空连接，服务器关闭连接！（连接但在规定时间内未登录）");
            }
            return _invalidSessionPacket;
        }
    }
}
