namespace Accountability.Metrics
{
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI.Helpers;

	public class MetricForm
	{
		private Metric _Metric;

		public MetricForm(Metric metric)
		{
			_Metric = metric;
		}

		public HtmlTag GetForm()
		{
			var fieldset = Tags.FieldSet;
			fieldset.Nest(Tags.Legend.Text("Edit Metric"));
			var form = Tags.Form;
			form.Action("Admin/SaveMetric");
			var id = Tags.Hidden.Name("id").Value(_Metric.Id);
			form.Nest(id,
			          _Metric.EditTemplateFor(m => m.Name),
			          _Metric.EditTemplateFor(m => m.Description),
			          _Metric.EditTemplateFor(m => m.WhyItMatters),
			          Tags.SubmitButton.Value("Save"));
			return fieldset.Nest(form);
		}
	}
}