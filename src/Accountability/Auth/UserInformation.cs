namespace Accountability.Authentication
{
	using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
	using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
	using DotNetOpenAuth.OpenId.RelyingParty;

	public class UserInformation
	{
		public UserInformation(IAuthenticationResponse response)
		{
			Response = response;
			ClaimedIdentifier = response.ClaimedIdentifier.ToString();
			if (response.Provider.Uri.Host == OpenIdHelper.Google)
			{
				SetUserInformationFromGoogle(response);
			}
			else
			{
				SetUserInformationGeneric(response);				
			}
		}

		private void SetUserInformationGeneric(IAuthenticationResponse response)
		{
			var userdata = response.GetExtension<ClaimsResponse>();
			var email = userdata.Email;
			FullName = userdata.FullName;
			Email = email;
		}

		private void SetUserInformationFromGoogle(IAuthenticationResponse response)
		{
			var userdata = response.GetExtension<FetchResponse>();
			var firstname = userdata.GetAttributeValue(WellKnownAttributes.Name.First);
			var lastname = userdata.GetAttributeValue(WellKnownAttributes.Name.Last);
			FullName = firstname + " " + lastname;
			Email = userdata.GetAttributeValue(WellKnownAttributes.Contact.Email);
		}

		public IAuthenticationResponse Response { get; set; }
		public string FullName { get; set; }
		public string ClaimedIdentifier { get; set; }
		public string Email { get; set; }
	}
}