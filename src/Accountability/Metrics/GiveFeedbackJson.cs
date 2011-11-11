namespace Accountability.Metrics
{
    using BclExtensionMethods;

    public class GiveFeedbackJson : AccountabilityEventJson
	{
		public string Source { get; set; }
		public string Notes { get; set; }
		public string Rating { get; set; }

		public GiveFeedbackJson(GiveFeedback feedback)
			: base(feedback)
		{
		    Source = feedback.SourceId.GetSourceName();
			Notes = feedback.Notes;
			Rating = feedback.Rating.ToDescription();
		}
	}
}