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
			      		Target = @event.TargetId.ToString(),
			      		Metric = @event.MetricId.ToString(),
			      		Source = @event.SourceId.ToString()
			      	};
			Metric = @event.MetricId.GetMetricName();
			Summary = @event.GetSummary();
		}

		public string Metric { get; set; }
		public string Summary { get; set; }

		public object Key { get; set; }
	}
}