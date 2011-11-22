namespace Accountability.Home
{
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Attributes;
	using Infrastructure;
	using Metrics;

	public class GiveFeedbackForm
	{
		[Multiline]
		public string Notes { get; set; }

		public Rating Rating { get; set; }

		public HtmlTag GetForm()
		{
			var wrapper = Tags.Div;
			var fieldset = new HtmlTag("fieldset")
				.Nest(
					new HtmlTag("legend").Text("Give Feedback"),
					Tags.Div
						.AddClass("inline")
						.DataBind("with: Model")
						.Nest(
							this.KnockoutEditTemplateFor(m => m.Rating),
							this.KnockoutEditTemplateFor(m => m.Notes)
						),
					Tags.SubmitButton.Value("Save Feedback").DataBind("click: SaveFeedback"),
					Tags.SubmitButton.Value("Done").DataBind("click: Done")
				);
			return wrapper.Nest(fieldset);
		}

		public string GetBlankFeedback()
		{
			var json = new
			           	{
			           		Notes = "",
			           		Rating = ""
			           	};
			return string.Format("function getBlankFeedback(){{ return ko.mapping.fromJS({0}); }}",
			                     Serialize.Javascript(json));
		}
	}
}