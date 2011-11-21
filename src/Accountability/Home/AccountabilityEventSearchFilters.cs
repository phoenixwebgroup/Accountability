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

	public class AccountabilityEventSearchFilters
	{
		public AccountabilityEventSearchFilters()
		{
			TargetId = UserPrincipal.Current.User.Id.ToString();
		}

		[OptionsFrom("Users")]
		public string TargetId { get; set; }

		[OptionsFrom("Users"), WithBlankOption]
		public string SourceId { get; set; }

		[OptionsFrom("Metrics"), WithBlankOption]
		public string MetricId { get; set; }

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
			var targetId = TargetId.ParseObjectId();
			if (targetId.HasValue)
			{
				events = events.Where(e => e.TargetId == targetId.Value);
			}
			var sourceId = SourceId.ParseObjectId();
			if (sourceId.HasValue)
			{
				events = events.Where(e => e.SourceId == sourceId.Value);
			}
			var metricId = MetricId.ParseObjectId();
			if (metricId.HasValue)
			{
				events = events.Where(e => e.MetricId == metricId.Value);
			}
			return events
				.RestrictEventsForCurrentUser()
				.OrderByDescending(o => o.Date)
				.ToList();
		}

		public List<AccountabilityEvent> Match()
		{
			var targetId = TargetId.ParseObjectId();
			var sourceId = SourceId.ParseObjectId();
			var metricId = MetricId.ParseObjectId();
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