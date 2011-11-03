namespace Accountability.Authentication
{
	using System;
	using System.Web;
	using Microsoft.Practices.ServiceLocation;

	public class AuthenticationModule : IHttpModule
	{
		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += context_AuthenticateRequest;
		}

		private void context_AuthenticateRequest(object sender, EventArgs e)
		{
			var userPrincipal = ServiceLocator.Current.GetInstance<UserPrincipal>();
			userPrincipal.InitializeForRequest();
		}
	}
}