namespace Accountability.Home
{
    using System.Linq;
    using System.Web.Mvc;
    using Authentication;
    using Metrics;
	using Mongos;

    public class HomeController : Controller
	{
        private readonly UserPrincipal _Principal;

        public HomeController(UserPrincipal principal)
        {
            _Principal = principal;
        }

        public ViewResult Search()
		{
		    var model = new SearchFilters();
			return View(model);
		}

        public JsonResult SearchData(SearchFilters filters)
        {
        	var results = filters
        		.GetResults()
        		.Select(r => new SearchJson(r));
            return Json(results);
        }

        [ChildActionOnly]
        public ViewResult MetricPartial()
        {
            var model = new MetricPartialView(_Principal);
            return View(model);
        }

        public JsonResult Metric(SearchFilters filters)
        {
            var events = filters.Match();
            return Json(new MetricView(events));
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