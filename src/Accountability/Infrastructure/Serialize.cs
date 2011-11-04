namespace Accountability.Infrastructure
{
    using System.Text.RegularExpressions;
    using System.Web.Script.Serialization;

    public static class Serialize
	{
		public static JavaScriptConverter[] Converters = new JavaScriptConverter[]
		                                                 	{
		                                                 		new DateTimeConverter(),
																new NullableBooleanConverter()
		                                                 	};

		public static Regex RemoveDateTimeWrapper = new Regex(@"{""__DateTime__"":(""[0-9/-]+"")}", RegexOptions.Compiled);
		public static Regex RemoveBooleanWrapper = new Regex(@"{""__bool\?__"":(""[a-z]+"")}", RegexOptions.Compiled);

		public static string Javascript(object obj)
		{
			var serializer = new JavaScriptSerializer();
			serializer.RegisterConverters(Converters);
			var s = serializer.Serialize(obj);
			// HACK This is really gross but MSFTs serializer doesn't support the behavior we need
			s = RemoveDateTimeWrapper.Replace(s, "$1");
			return RemoveBooleanWrapper.Replace(s, "$1");
		}
	}
}