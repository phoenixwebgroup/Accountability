namespace Accountability.Mongos
{
	using BclExtensionMethods.ValueTypes;
	using Infrastructure;
	using Metrics;
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization;
	using MongoDB.Driver;
	using Projects;
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

			BsonTypeMapper.RegisterCustomTypeMapper(typeof(Date), new DateBsonTypeMapper());
			BsonSerializer.RegisterSerializer(typeof(Date), new DateSerializer());
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

		public static MongoCollection<Project> Projects
		{
			get { return GetCollection<Project>(); }
		}

        public static MongoCollection<AccountabilityEvent> Events
        {
            get { return GetCollection<AccountabilityEvent>(); }
        }
	}
}