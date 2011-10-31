namespace Accountability.Admin
{
	using System.Web.Mvc;
	using HtmlTags;

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
	}
}