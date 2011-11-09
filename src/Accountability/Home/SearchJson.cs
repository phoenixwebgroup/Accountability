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
			Summary = @event.GetSummary();
		}

		public string Summary { get; set; }

		public object Key { get; set; }
	}
}