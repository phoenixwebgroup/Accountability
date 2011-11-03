namespace Accountability.Auth
{
	using System.Net;
	using System.Web.Mvc;
	using UISkeleton.Infrastructure;
	using WebApp;

	public class AuthorizeAccessFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			var method = filterContext.ActionDescriptor.TryGetMethodInfo();
			if (method == null)
			{
				return;
			}
			var canAccess = RequiresThatAttribute.UserCanAccess(method);
			if (canAccess)
			{
				return;
			}
			filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
			filterContext.HttpContext.Response.Write("User does not have access");
			filterContext.HttpContext.Response.End();
		}
	}
}