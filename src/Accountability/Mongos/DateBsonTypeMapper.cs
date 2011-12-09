namespace Accountability.Mongos
{
	using System;
	using System.Reflection;
	using BclExtensionMethods.ValueTypes;
	using MongoDB.Bson;

	public class DateBsonTypeMapper : ICustomBsonTypeMapper
	{
		public bool TryMapToBsonValue(object value, out BsonValue bsonValue)
		{
			try
			{
				var date = (DateTime) typeof (Date).GetField("_Date", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(value);
				bsonValue = new BsonDateTime(date);
				return true;
			}
			catch (Exception exception)
			{
				bsonValue = null;
				return false;
			}
		}
	}
}