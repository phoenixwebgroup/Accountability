namespace Accountability.Metrics
{
	using BclExtensionMethods.ValueTypes;

	public class Sample
	{
		/// <summary>
		/// Human, automated, billing system, other systems, exception logging
		/// </summary>
		public string Source { get; set; }
		public string Notes { get; set; }
		public Date Date { get; set; }
		public SampleRating Rating { get; set; }
	}
}