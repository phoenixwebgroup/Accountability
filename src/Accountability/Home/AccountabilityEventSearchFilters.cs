namespace Accountability.Home
{
	using System.Collections.Generic;
	using System.Linq;
	using Authentication;
	using FluentMongo.Linq;
	using HtmlTags.UI;
	using HtmlTags.UI.Attributes;
	using Infrastructure;
	using Metrics;
	using Mongos;
	using Users;

	public class AccountabilityEventSearchFilters
	{
		public AccountabilityEventSearchFilters()
		{
			Type = SearchType.ForMe;
		}

		public SearchType Type { get; set; }

		[OptionsFrom("Sources"), WithBlankOption]
		public string UserId { get; set; }

		[OptionsFrom("Metrics"), WithBlankOption]
		public string MetricId { get; set; }

		public Options Sources
		{
			get
			{
				var options = new SourcesService()
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
			var me = UserPrincipal.Current.User.Id;
			events = ForMe 
					? events.Where(e => e.TargetId == me)
					: events.Where(e => e.SourceId == me);
			var userId = UserId.ParseObjectId();
			if (userId.HasValue)
			{
				events = ForMe 
					? events.Where(e => e.SourceId == userId.Value)
					: events.Where(e => e.TargetId == userId.Value);
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

		private bool ForMe
		{
			get { return Type == SearchType.ForMe; }
		}
	}
}