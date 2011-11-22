namespace Accountability.Home
{
	using System.Linq;
	using System.Web.Mvc;
	using Metrics;

	public class HomeController : Controller
	{
		private readonly AccountabilityEventsService _EventsService;

		public HomeController(AccountabilityEventsService eventsService)
		{
			_EventsService = eventsService;
		}

		public ViewResult Search()
		{
			var model = new AccountabilityEventSearchFilters();
			return View(model);
		}

		public JsonResult SearchData(AccountabilityEventSearchFilters filters)
		{
			var results = _EventsService
				.GetEvents(filters)
				.Select(r => new SearchJson(r));
			return Json(results);
		}
		
		public void GiveFeedback(GiveFeedback command)
		{
			_EventsService.GiveFeedback(command);
		}

		public void AddActionItem(AddActionItem command)
		{
			_EventsService.AddActionItem(command);
		}

		public void ScheduleNextReview(ScheduleNextReview command)
		{
			_EventsService.ScheduleNextReview(command);
		}
	}
}