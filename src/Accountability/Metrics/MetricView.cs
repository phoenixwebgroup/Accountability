namespace Accountability.Metrics
{
	using System.Collections.Generic;
	using System.Linq;
	using Mongos;

	public class MetricView
	{
		private readonly List<AccountabilityEvent> _Events;

		public MetricView(IEnumerable<AccountabilityEvent> events)
		{
			_Events = events.ToList();
			ActionItems = _Events.OfType<AddActionItem>()
				.Select(a => new AddActionItemJson(a));
			Feedback = _Events.OfType<GiveFeedback>()
				.Select(a => new GiveFeedbackJson(a));
			ReviewDates = _Events.OfType<ScheduleNextReview>()
				.Select(a => new ScheduleNextReviewJson(a));

			Metric = _Events.Any()
			         	? Mongo.Metrics.FindOneById(_Events.First().MetricId).Name
			         	: string.Empty;
		}

		public IEnumerable<string> Sources
		{
			get
			{
				return _Events
					.Select(e => e.SourceId.GetSourceName())
					.Distinct();
			}
		}

		public IEnumerable<string> Targets
		{
			get
			{
				return _Events
					.Select(e => e.TargetId.GetSourceName())
					.Distinct();
			}
		}

		public string Metric { get; set; }

		public IEnumerable<AddActionItemJson> ActionItems { get; set; }

		public IEnumerable<GiveFeedbackJson> Feedback { get; set; }

		public IEnumerable<ScheduleNextReviewJson> ReviewDates { get; set; }
	}
}