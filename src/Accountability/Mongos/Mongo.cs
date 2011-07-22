namespace Accountability.Mongos
{
	using MongoDB.Driver;
	using Properties;

	public class Mongo
	{
		protected static MongoServer Server { get; set; }
		protected static MongoDatabase Database { get; set; }

		static Mongo()
		{
			Init();
			Server = MongoServer.Create(Settings.Default.MongoServerLocation);
			Database = Server.GetDatabase(Settings.Default.MongoDatabaseName);
		}

		public static void Init()
		{
		}

		public static MongoCollection<T>  GetCollection<T>()
		{
			return Database.GetCollection<T>(typeof (T).Name);
		}
	}
}