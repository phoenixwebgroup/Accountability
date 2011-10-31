namespace Accountability.Users
{
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Helpers;

	public class SourceForm
	{
		private readonly Source _Source;

		public SourceForm(Source source)
		{
			_Source = source;
		}

		public HtmlTag GetForm()
		{
			var fieldset = Tags.FieldSet;
			fieldset.Nest(Tags.Legend.Text("Edit Source"));
			var form = Tags.Form;
			form.Action("Admin/SaveSource");
			var id = Tags.Hidden.Id("id").Value(_Source.Id);
			form.Nest(id,
			          _Source.EditTemplateFor(m => m.Name),
			          Tags.SubmitButton.Value("Save"));
			return fieldset.Nest(form);
		}
	}
}