namespace Accountability.Metrics
{
	using System.Collections.Generic;

	public class MetricView
	{
		// todo denormalize off of ActionItems and Feedback
		public IEnumerable<string> Sources { get; set; }

		// todo denormalize off of ActionItems and Feedback
		public IEnumerable<string> Targets { get; set; }

		public string Metric { get; set; }

		public IEnumerable<AddActionItem> ActionItems { get; set; }

		public IEnumerable<GiveFeedback> Feedback { get; set; }
	}
}