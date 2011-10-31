namespace Accountability.Metrics
{
	using BclExtensionMethods.ValueTypes;

	public class ScheduleNextReview : AccountabilityEvent
	{
		public Date ReviewDate { get; set; }

		public override string GetSummary()
		{
			return "Next review: " + ReviewDate.ToShortDateString();
		}
	}
}