﻿namespace Accountability.Mongos
{
	using Metrics;
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
	}
}