namespace Accountability.Metrics
{
	public class AccountabilityEventJson
	{
		public string Id { get; set; }
		public string MetricId { get; set; }
		public string SourceId { get; set; }
		public string TargetId { get; set; }
		public string Date { get; set; }

		public AccountabilityEventJson(AccountabilityEvent @event)
		{
			MetricId = @event.MetricId.ToString();
			SourceId = @event.SourceId.ToString();
			TargetId = @event.TargetId.ToString();
			Date = @event.Date.ToString();
			Id = @event.Id.ToString();
		}
	}
}