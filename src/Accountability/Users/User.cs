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
		}

		public string ClaimedIdentifier { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }

		public bool IsDisabled()
		{
			return !IsActive;
		}
	}
}