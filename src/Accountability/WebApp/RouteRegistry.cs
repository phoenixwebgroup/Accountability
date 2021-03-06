namespace Accountability.WebApp
{
	using System.Web.Mvc;
	using System.Web.Routing;
	using Castle.Windsor;
	using UISkeleton.Infrastructure;

	public class RouteRegistry : IConfigureOnStartup
	{
		protected static void IgnoreAxds(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
		} 

		protected static void IgnoreFavicons(RouteCollection routes)
		{
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
		}

		private static void RegisterRoutes(RouteCollection routes)
		{
			IgnoreAxds(routes);
			IgnoreFavicons(routes);
			routes.IgnoreRoute("Content/{*pathInfo}");
			routes.IgnoreRoute("Scripts/{*pathInfo}");

			routes.MapRoute(
				"IdLess",
				"{controller}/{action}",
				new { controller = "Home", action = "Search" }
				);

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Search", id = string.Empty }
				);
		}

		public void Configure(IWindsorContainer container)
		{
			var routes = RouteTable.Routes;
			RegisterRoutes(routes);
		}
	}
}