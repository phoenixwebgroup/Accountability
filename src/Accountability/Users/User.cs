namespace Accountability.Users
{
	using Authentication;

	public class User : Source
	{
		public User()
		{
		}

		public User(UserInformation userInformation)
		{
			Name = userInformation.FullName;
			ClaimedIdentifier = userInformation.ClaimedIdentifier;
			Email = userInformation.Email;
			Role = Roles.User;
		}

		public string ClaimedIdentifier { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public Roles Role { get; set; }

		public bool IsDisabled()
		{
			return !IsActive;
		}
	}
}