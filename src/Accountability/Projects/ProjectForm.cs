namespace Accountability.Projects
{
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Helpers;

	public class ProjectForm
	{
		private readonly Project _Project;

		public ProjectForm(Project project)
		{
			_Project = project;
		}

		public HtmlTag GetForm()
		{
			var fieldset = Tags.FieldSet;
			fieldset.Nest(Tags.Legend.Text("Edit Project"));
			var form = Tags.Form;
			form.Action("Admin/SaveProject");
			var id = Tags.Hidden.Name("id").Value(_Project.Id);
			form.Nest(id,
			          _Project.EditTemplateFor(m => m.Name),
			          Tags.SubmitButton.Value("Save"),
			          Tags.SubmitButton.Value("Delete").Name("Delete")
				);
			return fieldset.Nest(form);
		}
	}
}