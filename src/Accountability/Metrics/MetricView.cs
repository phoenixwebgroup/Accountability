namespace Accountability.Metrics
{
    using System.Collections.Generic;
    using System.Linq;
    using Mongos;

    public class MetricView
    {
        private readonly List<AccountabilityEvent> _Events;

        public MetricView(IEnumerable<AccountabilityEvent> events)
        {
            _Events = events.ToList();
            ActionItems = _Events.OfType<AddActionItem>();
            Feedback = _Events.OfType<GiveFeedback>();
            ReviewDates = _Events.OfType<ScheduleNextReview>();
            Metric = _Events.Any()
                         ? Mongo.Metrics.FindOneById(_Events.First().MetricId).Name
                         : string.Empty;
        }

        public IEnumerable<string> Sources
        {
            get
            {
                return _Events
                    .Select(e => e.SourceId)
                    .Distinct()
                    .Select(id => Mongo.Users.FindOneById(id).Name);
            }
        }

        public IEnumerable<string> Targets
        {
            get
            {
                return _Events
                    .Select(e => e.TargetId)
                    .Distinct()
                    .Select(id => Mongo.Users.FindOneById(id).Name);
            }
        }

        public string Metric { get; set; }

        public IEnumerable<AddActionItem> ActionItems { get; set; }

        public IEnumerable<GiveFeedback> Feedback { get; set; }

        public IEnumerable<ScheduleNextReview> ReviewDates { get; set; }
    }
}