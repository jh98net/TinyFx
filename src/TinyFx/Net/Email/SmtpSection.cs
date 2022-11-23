using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;
using TinyFx.Net;

namespace TinyFx.Configuration
{
    public class SmtpSection : ConfigSection
    {
        public override string SectionName => "Smtp";
        public string DefaultClientName { get; set; }
        public Dictionary<string, SmtpClientElement> Clients = new Dictionary<string, SmtpClientElement>();

        public Dictionary<string, SendToElement> SendTos = new Dictionary<string, SendToElement>();
        public override void Bind(IConfiguration configuration)
        {
            base.Bind(configuration);
            Clients = BindDictionary<SmtpClientElement>(configuration, "Clients");
            SendTos = BindDictionary<SendToElement>(configuration, "SendTos");
        }
    }
}

namespace TinyFx.Net 
{
    public class SmtpClientElement : IOnlyKeyConfigElement
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }

        public string GetConfigElementKey()
        {
            return Name;
        }
    }
    public class SendToElement : IOnlyKeyConfigElement
    {
        public string Name { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }

        public string GetConfigElementKey()
        {
            return Name;
        }
    }
}

