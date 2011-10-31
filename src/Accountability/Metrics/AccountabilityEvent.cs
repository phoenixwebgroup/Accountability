namespace Accountability.Metrics
{
	using BclExtensionMethods.ValueTypes;
	using MongoDB.Bson;

	public class AccountabilityEvent
	{
		public Date Date { get; set; }
		public ObjectId SourceId { get; set; }
		public ObjectId MetricId { get; set; }
		public ObjectId TargetId { get; set; }
	}
}