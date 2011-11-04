namespace Accountability.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public class NullableBooleanConverter : JavaScriptConverter
	{
		public override IEnumerable<Type> SupportedTypes
		{
			get { return new List<Type>() { typeof(bool) }; }
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			var result = new Dictionary<string, object>();
			if (obj == null) return result;
			result["__bool?__"] = ((bool)obj).ToString().ToLowerInvariant();
			return result;
		}

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary.ContainsKey("__bool?__"))
			{
				return (bool?) dictionary["__DateTime__"];
			}
			return null;
		}
	}
}