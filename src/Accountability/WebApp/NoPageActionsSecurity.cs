namespace Accountability.WebApp
{
	using System.Reflection;
	using HtmlTags.UI.Conventions;

	public class NoPageActionsSecurity: IPageActionsSecurity
	{
		public bool UserCanAccess(MethodInfo action)
		{
			return true;
		}
	}
}