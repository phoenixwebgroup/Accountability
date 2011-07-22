namespace Accountability
{
	using MongoDB.Bson;

	public class Aggregate
	{
		public Aggregate()
		{
			Id = ObjectId.GenerateNewId();
		}

		public ObjectId Id { get; set; }
	}
}