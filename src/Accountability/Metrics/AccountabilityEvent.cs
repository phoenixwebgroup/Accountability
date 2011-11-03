namespace Accountability.Metrics
{
	using System;
	using MongoDB.Bson;

	public abstract class AccountabilityEvent
	{
		public AccountabilityEvent()
		{
			Date = DateTime.Now;
			Id = ObjectId.GenerateNewId();
		}

		public ObjectId Id { get; set; }
		public DateTime Date { get; set; }
		public ObjectId SourceId { get; set; }
		public ObjectId MetricId { get; set; }
		public ObjectId TargetId { get; set; }

		public abstract string GetSummary();
	}
}