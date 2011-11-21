namespace Accountability.WebApp
{
	using Authentication;
	using HtmlTags;
	using HtmlTags.Constants;
	using HtmlTags.Extensions;
	using Users;

	public class Navigation
	{
		public static HtmlTag GetHeader()
		{
			var header = Tags.Div.AddClass("NavigationHeader");
			AddTitle(header);
			AddLogout(header);
			AddAdminLink(header);
			return header;
		}

		private static void AddLogout(HtmlTag header)
		{
			var logout = Tags.Link.Attr(HtmlAttributeConstants.Href, "authentication/logout")
				.Text("(logout)");

			header.Nest(logout);
		}

		private static void AddTitle(HtmlTag header)
		{
			var currentUser = UserPrincipal.Current.User;
			var userName = currentUser != null ? currentUser.Name : string.Empty;

			var title = Tags.SpanText("Feedback - " + userName);
			header
				.Nest(title);
		}

		private static void AddAdminLink(HtmlTag header)
		{
			var currentUser = UserPrincipal.Current.User;
			var isAdmin = currentUser != null && currentUser.Role.HasFlag(Roles.Admin);
			if (isAdmin)
			{
				var admin = Tags.Link.Attr(HtmlAttributeConstants.Href, "admin/index")
					.Text("(admin)");
				header.Nest(admin);
			}
		}
	}
}