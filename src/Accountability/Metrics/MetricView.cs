namespace Accountability.Metrics
{
    using System.Collections.Generic;
    using System.Linq;
    using Home;
    using Infrastructure;

    public class MetricView
    {
        public MetricView(AccountabilityEventSearchFilters filters)
        {
            var events = filters.Match();
            SourceId = filters.SourceId;
            TargetId = filters.TargetId;
            MetricId = filters.MetricId;
            ActionItems = events.OfType<AddActionItem>().Select(a => new AddActionItemJson(a));
            Feedback = events.OfType<GiveFeedback>().Select(a => new GiveFeedbackJson(a));
            ReviewDates = events.OfType<ScheduleNextReview>().Select(a => new ScheduleNextReviewJson(a));
        }

        public string SourceId { get; set; }

        public string Source
        {
            get { return SourceId.ParseObjectId().Value.GetSourceName(); }
        }

        public string TargetId { get; set; }

        public string Target
        {
            get { return TargetId.ParseObjectId().Value.GetSourceName(); }
        }

        public string MetricId { get; set; }

        public string Metric
        {
            get { return MetricId.ParseObjectId().Value.GetMetricName(); }
        }

        public IEnumerable<AddActionItemJson> ActionItems { get; set; }

        public IEnumerable<GiveFeedbackJson> Feedback { get; set; }

        public IEnumerable<ScheduleNextReviewJson> ReviewDates { get; set; }
    }
}