namespace Accountability.Home
{
	using System.Web.Mvc;

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
	}
}