namespace Accountability.Metrics
{
	using BclExtensionMethods;

	public class AddActionItem : AccountabilityEvent
	{
		public string Description { get; set; }

		public override string GetSummary()
		{
			return string.Format("{0}: {1}", Date, Description.Truncate(100));
		}
	}
}