namespace Accountability.Users
{
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Helpers;

	public class UserForm
	{
		private readonly User _User;

		public UserForm(User user)
		{
			_User = user;
		}

		public HtmlTag GetForm()
		{
			var fieldset = Tags.FieldSet;
			fieldset.Nest(Tags.Legend.Text("Edit User"));
			var form = Tags.Form;
			form.Action("Admin/SaveUser");
			var id = Tags.Hidden.Name("id").Value(_User.Id);
			form.Nest(id,
			          _User.EditTemplateFor(m => m.Name),
			          _User.EditTemplateFor(m => m.Email),
			          _User.EditTemplateFor(m => m.ClaimedIdentifier),
			          Tags.SubmitButton.Value("Save"),
			          Tags.SubmitButton.Value("Delete").Name("Delete")
				);
			return fieldset.Nest(form);
		}
	}
}