namespace Accountability.Admin
{
	using System.Web.Mvc;
	using HtmlTags;
	using Metrics;
	using MongoDB.Bson;
	using Mongos;
	using Users;

	public class AdminController : Controller
	{
		public ViewResult Index()
		{
			return View(new AdminFilters());
		}

		public ViewResult IndexData(AdminFilters filters)
		{
			return View(new AdminListView(filters));
		}

		public HtmlTag AddMetric()
		{
			return new MetricForm(new Metric())
				.GetForm();
		}

		public HtmlTag AddSource()
		{
			return new SourceForm(new Source())
				.GetForm();
		}

		public string SaveMetric(Metric metric, string delete = null)
		{
			if (IsDelete(delete))
			{
				Mongo.Metrics.Remove(metric.Id);
				return "Deleted";
			}
			Mongo.Metrics.Save(metric);
			return "Saved";
		}

		private static bool IsDelete(string delete)
		{
			return delete == "Delete";
		}

		public string SaveSource(Source source, string delete = null)
		{
			if (IsDelete(delete))
			{
				Mongo.Sources.Remove(source.Id);
				return "Deleted";
			}
			Mongo.Sources.Save(source);
			return "Saved";
		}

		public HtmlTag Edit(AdminFilters.AdminType adminType, ObjectId id)
		{
			if (adminType == AdminFilters.AdminType.Metrics)
			{
				return new MetricForm(Mongo.Metrics.FindOneById(id))
					.GetForm();
			}
			return new SourceForm(Mongo.Sources.FindOneById(id))
				.GetForm();
		}
	}
}