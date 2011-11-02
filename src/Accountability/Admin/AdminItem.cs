namespace Accountability.Admin
{
	using Metrics;
	using MongoDB.Bson;
	using Users;

	public class AdminItem
	{
		public ObjectId Id { get; set; }
		public string Summary { get; set; }
		public AdminFilters.AdminType AdminType { get; set; }

		public AdminItem(Metric metric)
		{
			Summary = metric.Name;
			AdminType = AdminFilters.AdminType.Metric;
			Id = metric.Id;
		}

		public AdminItem(User user)
		{
			Summary = user.Name + " - " + user.Role + (user.IsActive ? string.Empty : " - Inactive");
			AdminType = AdminFilters.AdminType.User;
			Id = user.Id;
		}
	}
}