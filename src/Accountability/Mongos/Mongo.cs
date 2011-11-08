namespace Accountability.Mongos
{
	using Metrics;
	using MongoDB.Bson.Serialization;
	using MongoDB.Driver;
	using Properties;
	using Users;

	public class Mongo
	{
		protected static MongoServer Server { get; set; }
		public static MongoDatabase Database { get; set; }

		static Mongo()
		{
			Init();
			Server = MongoServer.Create(Settings.Default.MongoServerLocation);
			Database = Server.GetDatabase(Settings.Default.MongoDatabaseName);
			BsonClassMap.RegisterClassMap<GiveFeedback>();
			BsonClassMap.RegisterClassMap<AddActionItem>();
			BsonClassMap.RegisterClassMap<ScheduleNextReview>();
		}

		public static void Init()
		{
		}

		public static MongoCollection<T> GetCollection<T>()
		{
			return Database.GetCollection<T>(typeof (T).Name);
		}

		public static MongoCollection<Metric> Metrics
		{
			get { return GetCollection<Metric>(); }
		}

		public static MongoCollection<User> Users
		{
			get { return GetCollection<User>(); }
		}

        public static MongoCollection<AccountabilityEvent> Events
        {
            get { return GetCollection<AccountabilityEvent>(); }
        }
	}
}