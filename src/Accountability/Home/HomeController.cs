namespace Accountability.Home
{
    using System.Linq;
    using System.Web.Mvc;
	using Metrics;
	using Mongos;

    public class HomeController : Controller
	{
		public ViewResult Search()
		{
		    var model = new SearchFilters();
			return View(model);
		}

        public JsonResult SearchData(SearchFilters filters)
        {
            var results = filters
                .GetResults()
                .Select(r => new
                                 {
                                     key = new
                                               {
                                                   r.GetType().Name,
                                                   Target = r.TargetId.ToString(),
                                                   Metric = r.MetricId.ToString(),
                                                   Source = r.SourceId.ToString()
                                               },
                                     summary = r.GetSummary()
                                 });
            return Json(results);
        }

        public ViewResult Metric(SearchFilters filters)
        {
            return View(new MetricForm(filters));
        }

        public void GiveFeedback(GiveFeedback command)
        {
            Mongo.Events.Insert(command);
        }

        public void AddActionItem(AddActionItem command)
        {
            Mongo.Events.Insert(command);
        }

        public void ScheduleNextReview(ScheduleNextReview command)
        {
            Mongo.Events.Insert(command);
        }
	}
}