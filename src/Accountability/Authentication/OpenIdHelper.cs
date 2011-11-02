namespace Accountability.Authentication
{
	using DotNetOpenAuth.OpenId;
	using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
	using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
	using DotNetOpenAuth.OpenId.RelyingParty;

	public class OpenIdHelper
	{
		public const string Google = "www.google.com";

		public static IAuthenticationRequest CreateOpenIdRequest(string providerUrl)
		{
			var relyingParty = new OpenIdRelyingParty();
			var request = relyingParty.CreateRequest(Identifier.Parse(providerUrl));
			if (request.Provider.Uri.Host == Google)
			{
				var fetchRequest = new FetchRequest();
				fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
				fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
				fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);
				request.AddExtension(fetchRequest);
			}
			else
			{
				var claimsRequest = new ClaimsRequest
				                    	{
				                    		Email = DemandLevel.Require,
				                    		FullName = DemandLevel.Require
				                    	};
				request.AddExtension(claimsRequest);
			}
			return request;
		}
	}
}