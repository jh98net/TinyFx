// <copyright file="ElasticSearchReporterProvider.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using App.Metrics.Abstractions.Filtering;
using App.Metrics.Abstractions.Reporting;
using App.Metrics.Internal;
using App.Metrics.Reporting;
using Microsoft.Extensions.Logging;
using TinyFx.Timer.Formatting.ElasticSearch.Client;
using TinyFx.Timer.Reporting.ElasticSearch;
using BulkPayload = TinyFx.Timer.Reporting.ElasticSearch.BulkPayload;
using BulkPayloadBuilder = TinyFx.Timer.Reporting.ElasticSearch.BulkPayloadBuilder;

namespace TinyFx.Timer.Formatting.ElasticSearch
{
    public class ElasticSearchReporterProvider : IReporterProvider
    {
        private readonly ElasticSearchReporterSettings _settings;

        public ElasticSearchReporterProvider(ElasticSearchReporterSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Filter = new NoOpMetricsFilter();
        }

        public ElasticSearchReporterProvider(ElasticSearchReporterSettings settings, IFilterMetrics fitler)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Filter = fitler ?? new NoOpMetricsFilter();
        }

        public IFilterMetrics Filter { get; }

        public IMetricReporter CreateMetricReporter(string name, ILoggerFactory loggerFactory)
        {
            var elasticSearchBulkClient = new ElasticSearchBulkClient(
                loggerFactory,
                _settings.ElasticSearchSettings,
                _settings.HttpPolicy);

            var payloadBuilder = new BulkPayloadBuilder(
                _settings.ElasticSearchSettings.Index,
                _settings.MetricNameFormatter,
                _settings.MetricTagValueFormatter,
                _settings.DataKeys);

            return new ReportRunner<BulkPayload>(
                async p =>
                {
                    var result = await elasticSearchBulkClient.WriteAsync(p.Payload());
                    return result;
                },
                payloadBuilder,
                _settings.ReportInterval,
                name,
                loggerFactory);
        }
    }
}