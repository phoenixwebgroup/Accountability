namespace Accountability.Metrics
{
	using System;
	using BclExtensionMethods.Caches;
	using MongoDB.Bson;
	using Mongos;

	public class MetricsById : ExpiringCache<ObjectId, Metric>
	{
		public MetricsById()
		{
			ItemLifeSpan = TimeSpan.FromHours(1);
			OnMissingOrExpired = Load;
		}

		private Metric Load(ObjectId metricId)
		{
			return Mongo.Metrics.FindOneById(metricId);
		}
	}
}