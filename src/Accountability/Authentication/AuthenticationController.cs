namespace Accountability.Authentication
{
	using System.Web.Mvc;
	using System.Web.Security;
	using DotNetOpenAuth.Messaging;
	using DotNetOpenAuth.OpenId.RelyingParty;

	public class AuthenticationController : Controller
	{
		private readonly IOpenIdUserService _Users;

		public AuthenticationController(IOpenIdUserService users)
		{
			_Users = users;
		}

		public object Login(string openid_identifier, string returnUrl)
		{
			if (Authentication.IsAuthenticated)
			{
				return RedirectUser(returnUrl);
			}

			return HandleOpenIdResponse(returnUrl)
			       ?? SendToOpenIdProvider(openid_identifier)
			       ?? View();
		}

		private static ActionResult SendToOpenIdProvider(string openid_identifier)
		{
			if (string.IsNullOrWhiteSpace(openid_identifier))
			{
				return null;
			}
			return OpenIdHelper.CreateOpenIdRequest(openid_identifier)
				.RedirectingResponse
				.AsActionResult();
		}

		private object HandleOpenIdResponse(string returnUrl)
		{
			var response = new OpenIdRelyingParty().GetResponse();
			if (response == null)
			{
				return null;
			}

			switch (response.Status)
			{
				case AuthenticationStatus.Authenticated:
					HandleAuthenticated(response);
					return RedirectUser(returnUrl);
				case AuthenticationStatus.Canceled:
					return "Login canceled.";
				case AuthenticationStatus.Failed:
					return "Login failed.";
			}
			return null;
		}

		private void HandleAuthenticated(IAuthenticationResponse response)
		{
			FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier.ToString(), false);
			_Users.CreatePendingUserIfDoesNotExist(new UserInformation(response));
		}

		private object RedirectUser(string returnUrl)
		{
			if (!string.IsNullOrEmpty(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return Redirect(FormsAuthentication.DefaultUrl);
		}

		public ViewResult Logout()
		{
			FormsAuthentication.SignOut();
			return View("Logout");
		}
	}
}