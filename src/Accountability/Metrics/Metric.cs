namespace Accountability.Metrics
{
	using MongoDB.Bson;

	public class Metric
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}