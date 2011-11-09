namespace Accountability.Home
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Authentication;
	using FluentMongo.Linq;
	using HtmlTags.UI;
	using HtmlTags.UI.Attributes;
	using Infrastructure;
	using Metrics;
	using Mongos;

	public class SearchFilters
	{
		public SearchFilters()
		{
			Target = UserPrincipal.Current.User.Id.ToString();
		}

		[OptionsFrom("Users")]
		public string Target { get; set; }

		[OptionsFrom("Users"), WithBlankOption]
		public string Source { get; set; }

		[OptionsFrom("Metrics"), WithBlankOption]
		public string Metric { get; set; }

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
			var targetId = Target.ParseObjectId();
			if (targetId.HasValue)
			{
				events = events.Where(e => e.TargetId == targetId.Value);
			}
			var sourceId = Source.ParseObjectId();
			if (sourceId.HasValue)
			{
				events = events.Where(e => e.SourceId == sourceId.Value);
			}
			var metricId = Metric.ParseObjectId();
			if (metricId.HasValue)
			{
				events = events.Where(e => e.MetricId == metricId.Value);
			}
			return events
				.OrderByDescending(o => o.Date)
				.ToList();
		}

		public List<AccountabilityEvent> Match()
		{
			var targetId = Target.ParseObjectId();
			var sourceId = Source.ParseObjectId();
			var metricId = Metric.ParseObjectId();
			if (!targetId.HasValue || !sourceId.HasValue || !metricId.HasValue)
			{
				throw new Exception("Not enough detail");
			}
			return Mongo.Events.AsQueryable()
				.Where(e => e.TargetId == targetId.Value)
				.Where(e => e.SourceId == sourceId.Value)
				.Where(e => e.MetricId == metricId.Value)
				.ToList();
		}
	}
}