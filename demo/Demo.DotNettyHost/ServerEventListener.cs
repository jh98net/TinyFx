using DotNetty.Codecs.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TinyFx.Extensions.DotNetty;

namespace Demo.DotNettyHost
{
    internal class ServerEventListener : IServerEventListener
    {
        #region IServerEventListener
        public void OnChannelActive(object sender, ChannelActiveArgs args)
        {
        }

        public void OnChannelClosed(object sender, ChannelClosedArgs args)
        {
        }

        public void OnChannelException(object sender, ChannelExceptionArgs args)
        {
        }

        public void OnChannelHeartbeat(object sender, ChannelHeartbeatArgs args)
        {
        }

        public void OnChannelInactive(object sender, ChannelInactiveArgs args)
        {
        }

        public void OnChannelReceive(object sender, ChannelReceiveArgs args)
        {
        }

        public void OnChannelSend(object sender, ChannelSendArgs args)
        {
        }

        public void OnServerException(object sender, ServerExceptionArgs args)
        {
        }

        public void OnServerStart(object sender, ServerStartArgs args)
        {
        }

        public void OnServerStop(object sender, ServerStopArgs args)
        {
        }
        #endregion

        public bool VerifyBeforeHandshake(IFullHttpRequest req)
        {
            /*
            // ConnectTicket
            var idx = req.Uri.LastIndexOf('?');
            if (idx < 0)
                return false;
            var values = HttpUtility.ParseQueryString(req.Uri.Substring(idx));
            if (!values.AllKeys.Contains("ticket"))
                return false;
            var ticket = values["ticket"];
            //LoginCacheUtil.CheckConnectTicket(ticket, Options.AppId);
            */
            return true;
        }
    }
}
