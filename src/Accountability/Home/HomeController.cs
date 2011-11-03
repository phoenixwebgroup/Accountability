namespace Accountability.Home
{
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

        public ViewResult SearchData(SearchFilters filters)
        {
            return View(new SearchResultsView(filters));
        }

        public ViewResult View(SearchFilters filters)
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