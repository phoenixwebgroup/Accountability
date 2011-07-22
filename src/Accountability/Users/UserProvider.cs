namespace Accountability.Users
{
	using System.Collections.Generic;
	using MongoDB.Driver;
	using Mongos;

	public class UserProvider
	{
		public void Save(User user)
		{
			GetUsers().Save(user);
		}

		private static MongoCollection<User> GetUsers()
		{
			return Mongo.GetCollection<User>();
		}

		public IEnumerable<User> Users()
		{
			return GetUsers().FindAll();
		}
	}
}