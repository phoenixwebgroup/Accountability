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
                .Select(r => new SearchJson(r));
            return Json(results);
        }

        public JsonResult Metric(SearchFilters filters)
        {
            return Json(new MetricView(filters));
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