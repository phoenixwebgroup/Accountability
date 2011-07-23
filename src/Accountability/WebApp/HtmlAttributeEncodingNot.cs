namespace Accountability.WebApp
{
	using System.IO;
	using System.Web.Util;

	public class HtmlAttributeEncodingNot : HttpEncoder
	{
		protected override void HtmlAttributeEncode(string value, TextWriter output)
		{
			output.Write(value.Replace("\"", "&quot;"));
		}
	}
}