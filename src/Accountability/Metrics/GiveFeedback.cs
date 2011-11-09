namespace Accountability.Metrics
{
	using BclExtensionMethods;
	using BclExtensionMethods.Friendly;

	public class GiveFeedback : AccountabilityEvent
	{
		public string Notes { get; set; }
		public Rating Rating { get; set; }

		public override string GetSummary()
		{
			return string.Format("{1} ({0}): {2}", Date.TimeSince(), Rating, Notes.Truncate(100));
		}
	}
}