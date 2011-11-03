namespace Accountability.Authentication
{
	using System.Linq;
	using FluentMongo.Linq;
	using Mongos;
	using Users;

	public class OpenIdUserService : IOpenIdUserService
	{
		public void CreatePendingUserIfDoesNotExist(UserInformation userInformation)
		{
			var existingUser = Mongo.Users.AsQueryable()
				.FirstOrDefault(u => u.ClaimedIdentifier == userInformation.ClaimedIdentifier);
			if (existingUser != null)
			{
				return;
			}
			var user = new User(userInformation);
			Mongo.Users.Insert(user);
		}
	}
}