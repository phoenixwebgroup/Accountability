namespace Accountability.Search
{
	using System.Linq;
	using System.Web.Mvc;
	using HtmlTags;
	using Users;

	public class SearchController : Controller
	{
		public ViewResult Home()
		{
			//new UserProvider().Save(new User("Loraine Snedeker"));
			//new UserProvider().Save(new User("Cody Hampshire"));
			//new UserProvider().Save(new User("Allie Amor"));
			//new UserProvider().Save(new User("Tyrone Dunnington"));
			//new UserProvider().Save(new User("Lilia Mynatt"));
			//new UserProvider().Save(new User("Jessie Buckle"));
			//new UserProvider().Save(new User("Ted Filippini"));
			//new UserProvider().Save(new User("Malinda Schmelzer"));
			//new UserProvider().Save(new User("Julio Drane"));
			//new UserProvider().Save(new User("Ericka Fetterman"));

			return View(new HomeView());
		}

		public JsonResult SearchFor(Search search)
		{
			var users = new UserProvider().QueryUsers()
				.Select(u => new UserResult(u));

			var resultSet = new ResultSet(users);

			return Json(resultSet.GetResults());
		}
	}
}