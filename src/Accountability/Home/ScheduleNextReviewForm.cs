namespace Accountability.Home
{
	using System;
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Attributes;
	using Infrastructure;

	public class ScheduleNextReviewForm
	{
		[DateOnly]
		public DateTime ReviewDate { get; set; }

		public HtmlTag GetForm()
		{
			var wrapper = Tags.Div;
			var fieldset = new HtmlTag("fieldset")
				.Nest(
					new HtmlTag("legend").Text("Schedule Review"),
					Tags.Div
						.AddClass("inline")
						.DataBind("with: Model")
						.Nest(
							this.KnockoutEditTemplateFor(m => m.ReviewDate)
						),
					Tags.SubmitButton.Value("Schedule").DataBind("click: SaveNextReview"),
					Tags.SubmitButton.Value("Done").DataBind("click: Done")
				);
			return wrapper.Nest(fieldset);
		}

		public string GetJavascript()
		{
			var json = new
			           	{
			           		ReviewDate = DateTime.Today.ToString("M/d/yyyy")
			           	};
			return string.Format("function getBlankNextReview(){{ return ko.mapping.fromJS({0}); }}",
			                     Serialize.Javascript(json));
		}
	}
}