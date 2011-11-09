namespace Accountability.Metrics
{
	public class AddActionItemJson : AccountabilityEventJson
	{
		public string Description { get; set; }

		public AddActionItemJson(AddActionItem actionItem)
			: base(actionItem)
		{
			Description = actionItem.Description;
		}
	}
}