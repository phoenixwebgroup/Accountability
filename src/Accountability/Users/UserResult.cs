namespace Accountability.Users
{
	using BclExtensionMethods;
	using Search;

	public class UserResult : SimpleResult
	{
		private readonly User _User;

		public UserResult(User user)
		{
			_User = user;
		}

		public override string title
		{
			get { return _User.Name.Truncate(25); }
		}

		public override string body
		{
			get { return "Name: " + _User.Name; }
		}
	}
}