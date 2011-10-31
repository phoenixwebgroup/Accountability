namespace Accountability.Mongos
{
	using MongoDB.Bson;
	using MongoDB.Driver;

	public static class MongoExtensions
	{
		public static void Remove<T>(this MongoCollection<T> collection, ObjectId id)
			where T : Aggregate
		{
			collection.Remove(new QueryDocument {{"_id", id}});
		}
	}
}