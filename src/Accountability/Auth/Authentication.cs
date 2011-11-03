namespace Accountability.Authentication
{
	using System.Web;
	using System.Web.Security;

	public class Authentication
	{
		public static bool IsAuthenticated
		{
			get
			{
				return
					GetFormsIdentity() != null
					&& GetFormsIdentity().IsAuthenticated;
			}
		}

		private static FormsIdentity GetFormsIdentity()
		{
			if (HttpContext.Current.User == null)
			{
				return null;
			}
			return HttpContext.Current.User.Identity as FormsIdentity;
		}
	}
}