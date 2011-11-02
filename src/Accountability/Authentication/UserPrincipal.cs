namespace Accountability.Authentication
{
	using System;
	using System.Linq;
	using System.Web;
	using System.Web.Security;
	using FluentMongo.Linq;
	using Microsoft.Practices.ServiceLocation;
	using Mongos;
	using Users;

	public class UserPrincipal
	{
		public User User { get; set; }

		public void InitializeForRequest()
		{
			if (!Authentication.IsAuthenticated)
			{
				return;
			}
			var name = HttpContext.Current.User.Identity.Name;
			User = Mongo.Users.AsQueryable().FirstOrDefault(u => u.ClaimedIdentifier == name);
			if (User == null)
			{
				FormsAuthentication.SignOut();
				throw new ApplicationException("User does not exist. Access denied.");
			}
			if (User.IsDisabled())
			{
				FormsAuthentication.SignOut();
				throw new ApplicationException(
					"Your account has been created but is not yet approved, please contact an administrator to complete the setup process.");
			}
		}

		public static UserPrincipal Current
		{
			get { return ServiceLocator.Current.GetInstance<UserPrincipal>(); }
		}
	}
}