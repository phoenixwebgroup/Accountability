namespace Accountability.Metrics
{
	public class GiveFeedbackJson : AccountabilityEventJson
	{
		public string Notes { get; set; }
		public Rating Rating { get; set; }

		public GiveFeedbackJson(GiveFeedback feedback)
			: base(feedback)
		{
			Notes = feedback.Notes;
			Rating = feedback.Rating;
		}
	}
}