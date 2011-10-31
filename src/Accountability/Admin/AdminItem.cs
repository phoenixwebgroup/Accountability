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
			Summary = metric.Name + " - " + metric.WhyItMatters;
			AdminType = AdminFilters.AdminType.Metrics;
			Id = metric.Id;
		}

		public AdminItem(Source source)
		{
			Summary = source.Name;
			AdminType = AdminFilters.AdminType.Metrics;
			Id = source.Id;
		}
	}
}