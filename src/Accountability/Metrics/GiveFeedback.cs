namespace Accountability.Metrics
{
	public class GiveFeedback : AccountabilityEvent
	{
		public string Notes { get; set; }
		public Rating Rating { get; set; }
	}
}