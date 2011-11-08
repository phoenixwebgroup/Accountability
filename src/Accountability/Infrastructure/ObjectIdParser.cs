namespace Accountability.Infrastructure
{
	using MongoDB.Bson;

	public static class ObjectIdParser
	{
		public static ObjectId? ParseObjectId(this object source)
		{
			if (source == null)
			{
				return null;
			}
			ObjectId objectId;
			if (ObjectId.TryParse(source.ToString(), out objectId))
			{
				return objectId;
			}
			return null;
		}
	}
}