namespace Accountability.Users
{
	public class User : Aggregate
	{
		public User(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}