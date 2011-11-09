namespace Accountability.Metrics
{
	using BclExtensionMethods;
	using BclExtensionMethods.Friendly;

	public class AddActionItem : AccountabilityEvent
	{
		public string Description { get; set; }

		public override string GetSummary()
		{
			return string.Format("{0}: {1}", Date.TimeSince(), Description.Truncate(100));
		}
	}
}