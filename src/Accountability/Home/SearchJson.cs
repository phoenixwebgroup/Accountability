namespace Accountability.Home
{
	using Metrics;

	public class SearchJson
	{
		public SearchJson(AccountabilityEvent @event)
		{
			Key = new
			      	{
			      		@event.GetType().Name,
			      		TargetId = @event.TargetId.ToString(),
                        MetricId = @event.MetricId.ToString(),
                        SourceId = @event.SourceId.ToString()
			      	};
			Metric = @event.MetricId.GetMetricName();
			Summary = @event.GetSummary();
		}

		public string Metric { get; set; }
		public string Summary { get; set; }

		public object Key { get; set; }
	}
}