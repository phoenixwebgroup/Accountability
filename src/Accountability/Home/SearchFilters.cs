namespace Accountability.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentMongo.Linq;
    using HtmlTags.UI;
    using HtmlTags.UI.Attributes;
    using Metrics;
    using MongoDB.Bson;
    using Mongos;

    public class SearchFilters
    {
        public SearchFilters()
        {
            // todo Source = current user
        }

        [OptionsFrom("Users")]
        public ObjectId? Target { get; set; }

        [OptionsFrom("Users"), WithBlankOption]
        public ObjectId? Source { get; set; }

        [OptionsFrom("Metrics"), WithBlankOption]
        public ObjectId? Metric { get; set; }

        public Options Users
        {
            get { return Mongo.Users.FindAll().ToOptions(x => x.Id.ToString(), x => x.Name); }
        }

        public Options Metrics
        {
            get { return Mongo.Metrics.FindAll().ToOptions(x => x.Id.ToString(), x => x.Name); }
        }

        public IEnumerable<AccountabilityEvent> GetResults()
        {
            var events = Mongo.Events.AsQueryable();
            if (Target.HasValue)
            {
                events = events.Where(e => e.TargetId == Target.Value);
            }
            if (Source.HasValue)
            {
                events = events.Where(e => e.SourceId == Source.Value);
            }
            if (Metric.HasValue)
            {
                events = events.Where(e => e.MetricId == Metric.Value);
            }
            return events.ToArray();
        }
    }
}