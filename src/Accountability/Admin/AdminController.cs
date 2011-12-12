namespace Accountability.Admin
{
	using System.Web.Mvc;
	using HtmlTags;
	using Metrics;
	using MongoDB.Bson;
	using Mongos;
	using Projects;
	using Users;
	using WebApp;

	[RequiresThat(Roles.Admin)]
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

		public HtmlTag AddUser()
		{
			return new UserForm(new User())
				.GetForm();
		}

		public HtmlTag AddProject()
		{
			return new ProjectForm(new Project())
				.GetForm();
		}

		public string SaveMetric(Metric metric, string delete = null)
		{
			if (IsDelete(delete))
			{
				Mongo.Metrics.Remove(metric.Id);
				return "Deleted";
			}
			Mongo.Metrics.Save(metric);
			return "Saved";
		}

		private static bool IsDelete(string delete)
		{
			return delete == "Delete";
		}

		public string SaveUser(User user, string delete = null)
		{
			if (IsDelete(delete))
			{
				Mongo.Users.Remove(user.Id);
				return "Deleted";
			}
			Mongo.Users.Save(user);
			return "Saved";
		}

		public string SaveProject(Project project, string delete = null)
		{
			if (IsDelete(delete))
			{
				Mongo.Projects.Remove(project.Id);
				return "Deleted";
			}
			Mongo.Projects.Save(project);
			return "Saved";
		}

		public HtmlTag Edit(AdminFilters.AdminType adminType, ObjectId id)
		{
			if (adminType == AdminFilters.AdminType.Metric)
			{
				return new MetricForm(Mongo.Metrics.FindOneById(id))
					.GetForm();
			}
			if (adminType == AdminFilters.AdminType.User)
			{
				return new UserForm(Mongo.Users.FindOneById(id))
					.GetForm();
			}
			return new ProjectForm(Mongo.Projects.FindOneById(id))
				.GetForm();
		}
	}
}