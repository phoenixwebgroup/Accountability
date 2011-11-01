namespace Accountability.Metrics
{
	using HtmlTags.UI.Attributes;

	public class Metric : Aggregate
	{
		public string Name { get; set; }

		[Multiline]
		public string Description { get; set; }

		[Multiline]
		public string WhyItMatters { get; set; }
	}
}