using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Enrichers;
using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.Configuration;

namespace Serilog
{
    public static class SerilogExtensions
    {
        #region Enrichers

#if THREAD_NAME
        /// <summary>
        /// Enrich log events with a ThreadName property containing the <see cref="Thread.CurrentThread"/> <see cref="Thread.Name"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="enrichmentConfiguration"/> is null.</exception>
        public static LoggerConfiguration WithThreadName(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ThreadNameEnricher>();
        }
#endif
        /// <summary>
        /// 将 logEvent.MessageTemplate 的 MurmurHash（高效hash）值添加到事件属性
        /// </summary>
        /// <param name="enrich"></param>
        /// <returns></returns>
        public static LoggerConfiguration WithTemplateHash(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
                throw new ArgumentNullException(nameof(enrich));
            return enrich.With<TemplateHashEnricher>();
        }
        #endregion 
    }
}
