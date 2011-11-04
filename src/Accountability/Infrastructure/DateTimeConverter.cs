namespace Accountability.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public class DateTimeConverter : JavaScriptConverter
	{
		public override IEnumerable<Type> SupportedTypes
		{
			get { return new List<Type>() { typeof(DateTime), typeof(DateTime?) }; }
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			var result = new Dictionary<string, object>();
			if (obj == null) return result;
			result["__DateTime__"] = ((DateTime)obj).ToString("MM/dd/yy");
			return result;
		}

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary.ContainsKey("__DateTime__"))
				return new DateTime(long.Parse(dictionary["__DateTime__"].ToString()), DateTimeKind.Unspecified);
			return null;
		}
	}
}