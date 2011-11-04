namespace Accountability.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            get
            {
                var options = Mongo.Users
                    .FindAll()
                    .OrderBy(x => x.Name)
                    .ToOptions(x => x.Id.ToString(), x => x.Name);
                options.Insert(0, new OptionItem("", ""));
                return options;
            }
        }

        public Options Metrics
        {
            get
            {
                var options = Mongo.Metrics
                    .FindAll()
                    .OrderBy(x => x.Name)
                    .ToOptions(x => x.Id.ToString(), x => x.Name);
                options.Insert(0, new OptionItem("", ""));
                return options;
            }
        }

        public List<AccountabilityEvent> GetResults()
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
            return events.ToList();
        }

        public List<AccountabilityEvent> Match()
        {
            if (!Target.HasValue || !Source.HasValue || !Metric.HasValue)
            {
                throw new Exception("Not enough detail");
            }
            return Mongo.Events.AsQueryable()
                .Where(e => e.TargetId == Target.Value)
                .Where(e => e.SourceId == Source.Value)
                .Where(e => e.MetricId == Metric.Value)
                .ToList();
        }
    }
}