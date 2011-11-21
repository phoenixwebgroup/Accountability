namespace Accountability.Home
{
	using System.Collections.Generic;
	using Authentication;
	using GotFour.Windsor;
	using Metrics;
	using Mongos;

	[SelfService]
	public class AccountabilityEventsService
	{
		public IEnumerable<AccountabilityEvent> GetEvents(AccountabilityEventSearchFilters filters)
		{
			return filters
				.GetResults();
		}

		public void GiveFeedback(GiveFeedback command)
		{
			SetSource(command);
			Mongo.Events.Insert(command);
		}

		private static void SetSource(AccountabilityEvent command)
		{
			command.SourceId = UserPrincipal.Current.User.Id;
		}

		public void AddActionItem(AddActionItem command)
		{
			SetSource(command);
			Mongo.Events.Insert(command);
		}

		public void ScheduleNextReview(ScheduleNextReview command)
		{
			SetSource(command);
			Mongo.Events.Insert(command);
		}
	}
}