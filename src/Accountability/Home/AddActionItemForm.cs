namespace Accountability.Home
{
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Attributes;
	using Infrastructure;

	public class AddActionItemForm
	{
		[Multiline]
		public string Description { get; set; }

		public HtmlTag GetForm()
		{
			var wrapper = Tags.Div;
			var fieldset = new HtmlTag("fieldset")
				.Nest(
					new HtmlTag("legend").Text("Add Action Item"),
					Tags.Div
						.AddClass("inline")
						.DataBind("with: Model")
						.Nest(
							this.KnockoutEditTemplateFor(m => m.Description)
						),
					Tags.SubmitButton.Value("Save Feedback").DataBind("click: SaveActionItem"),
					Tags.SubmitButton.Value("Done").DataBind("click: Done")
				);
			return wrapper.Nest(fieldset);
		}

		public string GetJavascript()
		{
			var json = new
			           	{
			           		Description = ""
			           	};
			return string.Format("function getBlankActionItem(){{ return ko.mapping.fromJS({0}); }}",
			                     Serialize.Javascript(json));
		}
	}
}