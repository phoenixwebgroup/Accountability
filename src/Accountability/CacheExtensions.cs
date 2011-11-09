namespace Accountability
{
	using System;
	using Metrics;
	using MongoDB.Bson;
	using Users;

	public static class CacheExtensions
	{
		public static Lazy<MetricsById> MetricsById = new Lazy<MetricsById>(() => new MetricsById());
		public static Lazy<SourcesById> SourcesById = new Lazy<SourcesById>(() => new SourcesById());

		public static Metric GetMetric(this ObjectId metricId)
		{
			return MetricsById.Value[metricId];
		}

		public static string GetMetricName(this ObjectId metricId)
		{
			var metric = MetricsById.Value[metricId];
			if (metric == null)
			{
				return "Invalid metric id: " + metricId;
			}
			return metric.Name;
		}

		public static Source GetSource(this ObjectId sourceId)
		{
			return SourcesById.Value[sourceId];
		}

		public static string GetSourceName(this ObjectId sourceId)
		{
			var source = SourcesById.Value[sourceId];
			if (source == null)
			{
				return "Invalid source id: " + sourceId;
			}
			return source.Name;
		}

		public static void ClearAll()
		{
			MetricsById.Value.ClearAll();
			SourcesById.Value.ClearAll();
		}
	}
}