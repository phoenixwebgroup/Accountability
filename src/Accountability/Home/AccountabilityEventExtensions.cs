namespace Accountability.Home
{
	using System.Linq;
	using Authentication;
	using Metrics;

	public static class AccountabilityEventExtensions
	{
		public static IQueryable<AccountabilityEvent> RestrictEventsForCurrentUser(this IQueryable<AccountabilityEvent> source)
		{
			var currentUserId = UserPrincipal.Current.User.Id;
			return source
				.Where(e => e.TargetId == currentUserId || e.SourceId == currentUserId);
		}
	}
}