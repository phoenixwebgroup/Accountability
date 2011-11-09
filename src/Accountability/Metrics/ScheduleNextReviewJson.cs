namespace Accountability.Metrics
{
	public class ScheduleNextReviewJson : AccountabilityEventJson
	{
		public string ReviewDate { get; set; }

		public ScheduleNextReviewJson(ScheduleNextReview review)
			: base(review)
		{
			ReviewDate = review.ReviewDate.ToShortDateString();
		}
	}
}