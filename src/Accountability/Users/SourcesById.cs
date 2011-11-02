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

		private User Load(ObjectId sourceId)
		{
			// todo in the future we might have other sources too.
			return Mongo.Users.FindOneById(sourceId);
		}
	}
}