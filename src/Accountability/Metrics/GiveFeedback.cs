namespace Accountability.Metrics
{
	using BclExtensionMethods;

	public class GiveFeedback : AccountabilityEvent
	{
		public string Notes { get; set; }
		public Rating Rating { get; set; }

		public override string GetSummary()
		{
			return string.Format("Rating ({0}): {1} {2}", Date, Rating, Notes.Truncate(100));
		}
	}
}