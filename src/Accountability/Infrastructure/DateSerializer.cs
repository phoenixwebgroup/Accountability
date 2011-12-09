namespace Accountability.Infrastructure
{
	using System;
	using System.Reflection;
	using BclExtensionMethods.ValueTypes;
	using MongoDB.Bson.IO;
	using MongoDB.Bson.Serialization;
	using MongoDB.Bson.Serialization.Serializers;

	public class DateSerializer : DateTimeSerializer
	{
		public override object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
		{
			var date = (DateTime)base.Deserialize(bsonReader, typeof(DateTime), typeof(DateTime), options);
			return new Date(date);
		}

		public override void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
		{
			var date = typeof (Date).GetField("_Date", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(value);
			base.Serialize(bsonWriter, typeof (DateTime), date, options);
		}
	}
}