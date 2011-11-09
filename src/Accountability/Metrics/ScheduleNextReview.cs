namespace Accountability.Metrics
{
	using BclExtensionMethods.ValueTypes;

	public class ScheduleNextReview : AccountabilityEvent
	{
		public Date ReviewDate { get; set; }

		public override string GetSummary()
		{
			// todo time until
			return "Next review: " + ReviewDate.ToShortDateString();
		}
	}
}