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

		public void SaveMetric(Metric metric)
		{
			Mongo.Metrics.Save(metric);
		}

		public void SaveSource(Source source)
		{
			Mongo.Sources.Save(source);
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