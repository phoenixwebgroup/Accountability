namespace Accountability.Admin
{
	using System.Web.Mvc;
	using HtmlTags;
	using Metrics;
	using Mongos;
	using Users;

	public class AdminController : Controller
	{
		public ViewResult Index()
		{
			return View(new AdminFilters());
		}

		public HtmlTag IndexData(AdminFilters filters)
		{
			return new AdminListView(filters)
				.GetView();
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
	}
}