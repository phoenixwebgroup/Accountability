namespace Accountability.Home
{
	using System.Collections.Generic;
	using System.Linq;
	using Authentication;
	using Metrics;
	using Mongos;

	public static class AccountabilityEventExtensions
	{
		public static IEnumerable<AccountabilityEvent> RestrictEventsForCurrentUser(this IQueryable<AccountabilityEvent> source)
		{
			var currentUserId = UserPrincipal.Current.User.Id;
			var projectIds = Mongo.Projects.FindAll().Select(x => x.Id).ToArray();
			return source
				.ToList()
				.Where(e => e.TargetId == currentUserId || e.SourceId == currentUserId || projectIds.Contains(e.TargetId));
		}
	}
}