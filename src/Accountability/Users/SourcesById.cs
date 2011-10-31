namespace Accountability.Users
{
	using System;
	using BclExtensionMethods.Caches;
	using MongoDB.Bson;
	using Mongos;

	public class SourcesById : ExpiringCache<ObjectId, Source>
	{
		public SourcesById()
		{
			ItemLifeSpan = TimeSpan.FromHours(1);
			OnMissingOrExpired = Load;
		}

		private Source Load(ObjectId sourceId)
		{
			return Mongo.Sources.FindOneById(sourceId);
		}
	}
}