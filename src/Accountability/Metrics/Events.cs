namespace Accountability.Metrics
{
	using BclExtensionMethods.ValueTypes;
	using MongoDB.Bson;

	public class GiveFeedback : AccountabilityEvent
	{
		public ObjectId MetricId { get; set; }
		public ObjectId TargetId { get; set; }
		public string Notes { get; set; }
	}

	public class ScheduleNextReview : AccountabilityEvent
	{
		public ObjectId MetricId { get; set; }
		public ObjectId TargetId { get; set; }
		public Date ReviewDate { get; set; }
	}

	public class AddActionItem : AccountabilityEvent
	{
		public ObjectId MetricId { get; set; }
		public ObjectId TargetId { get; set; }
		public string Description { get; set; }
	}
}