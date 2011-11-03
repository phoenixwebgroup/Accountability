namespace Accountability.Auth
{
	using System.Reflection;
	using HtmlTags.UI.Conventions;
	using WebApp;

	public class PageActionsSecurity : IPageActionsSecurity
	{
		public bool UserCanAccess(MethodInfo action)
		{
			return RequiresThatAttribute.UserCanAccess(action);
		}
	}
}