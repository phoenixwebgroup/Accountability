namespace Accountability.Infrastructure
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using BclExtensionMethods;
	using FubuCore.Reflection;
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.UI;
	using HtmlTags.UI.Conventions;
	using HtmlTags.UI.Helpers;

	public static class KnockoutHelpers
	{
		public static HtmlTag KnockoutInputFor<T>(this T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var propertyName = selector.ToAccessor().Name;
			var input = model
				.InputFor(selector);
			var binding = "value";
			if (input is CheckboxTag)
			{
				binding = "checked";
			}
			return input.Attr("data-bind", string.Format("{0}: {1}", binding, propertyName));
		}

		public static HtmlTag KnockoutTextboxFor<T>(this T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var propertyName = selector.ToAccessor().Name;
			return Tags.Input
				.Attr("type", "text")
				.Attr("data-bind", string.Format("value: {0}", propertyName));
		}

		public static HtmlTag KnockoutTextboxFor(string propertyName)
		{
			return Tags.Input
				.Attr("type", "text")
				.Attr("data-bind", string.Format("value: {0}", propertyName));
		}

		public static HtmlTag KnockoutDisplayFor<T>(this T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var propertyName = selector.ToAccessor().Name;
			return Tags.Span.Attr("data-bind", string.Format("text: {0}", propertyName));
		}

		public static HtmlTag KnockoutDisplayFor(string propertyName)
		{
			return Tags.Span.Attr("data-bind", string.Format("text: {0}", propertyName));
		}

		public static HtmlTag KnockoutEditTemplateFor<T>(this T model, params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			if (!fieldSelectors.Any())
			{
				throw new ArgumentException("Must be at least one field selector");
			}

			var label = model.LabelFor(fieldSelectors[0]).AddClass(TemplateHelpers.LabelClass);

			var fields = fieldSelectors
				.Select(f => Tags.Div
								.AddClass(TemplateHelpers.FieldDivClass)
								.Nest(KnockoutInputFor(model, f)));

			return Tags.Div
				.AddClass(TemplateHelpers.FieldsDivClass)
				.Nest(label)
				.Nest(fields.ToArray());
		}

		public static HtmlTag KnockoutEditTemplateFor<T>(this HtmlHelper<T> helper,
												 params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			return KnockoutEditTemplateFor(helper.ViewData.Model, fieldSelectors);
		}

		public static HtmlTag KnockoutDisplayTemplateFor<T>(this T model,
													params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			if (!fieldSelectors.Any())
			{
				throw new ArgumentException("Must be at least one field selector");
			}

			var label = model.LabelFor(fieldSelectors[0]).AddClass(TemplateHelpers.LabelClass);

			var fields = fieldSelectors
				.Select(f => Tags.Div
								.AddClass(TemplateHelpers.FieldDivClass)
								.Nest(KnockoutDisplayFor(model, f)));

			return Tags.Div
				.AddClass(TemplateHelpers.FieldsDivClass)
				.Nest(label)
				.Nest(fields.ToArray());
		}

		public static HtmlTag KnockoutDisplayTemplateFor<T>(this HtmlHelper<T> helper,
													params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			return KnockoutDisplayTemplateFor(helper.ViewData.Model, fieldSelectors);
		}

		// Horizontal layout

		/// <summary>
		/// Just an empty textbox with the data-bind attribute.
		/// </summary>
		public static HtmlTag KnockoutTextboxFor<T>(this TableRowTag row, T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var input = KnockoutTextboxFor(model, selector);
			row.Cell().Child(input);
			return input;
		}

		public static HtmlTag KnockoutDisplayFor<T>(this TableRowTag row, T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var input = KnockoutDisplayFor(model, selector);
			row.Cell().Child(input);
			return input;
		}

		/// <summary>
		/// Calls the input builder conventions.
		/// </summary>
		public static HtmlTag KnockoutInputFor<T>(this TableRowTag row, T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var input = KnockoutInputFor(model, selector);
			row.Cell().Child(input);
			// HACK Jquery UI datepicker can't handle multiple identical ids
			if (selector.ToAccessor().PropertyType.In(typeof(DateTime), typeof(DateTime?)))
			{
				input.RemoveAttr("id");
			}
			return input;
		}

		// Vertical layout

		/// <summary>
		/// Just an empty textbox with the data-bind attribute.
		/// </summary>
		public static HtmlTag KnockoutTextboxFor<T>(this TableTag table, T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var input = KnockoutTextboxFor(model, selector);
			table.AddBodyRow(row =>
			{
				row.Cell(LabelingConvention.Convention.GetLabelText(selector.ToAccessor()));
				row.Cell().Child(input);
			});
			return input;
		}

		/// <summary>
		/// For client-side bound fields.
		/// </summary>
		public static HtmlTag KnockoutTextboxFor(this TableTag table, string propertyName)
		{
			var input = KnockoutTextboxFor(propertyName);
			table.AddBodyRow(row =>
			{
				row.Cell(LabelingConvention.Convention.GetLabelText(propertyName));
				row.Cell().Child(input);
			});
			return input;
		}

		/// <summary>
		/// Calls the input builder conventions.
		/// </summary>
		public static HtmlTag KnockoutInputFor<T>(this TableTag table, T model, Expression<Func<T, object>> selector)
			where T : class
		{
			var input = KnockoutInputFor(model, selector);
			table.AddBodyRow(row =>
			{
				row.Cell(LabelingConvention.Convention.GetLabelText(selector.ToAccessor()));
				row.Cell().Child(input);
			});
			return input;
		}


		public static HtmlTag KnockoutTextboxFor(this TableRowTag row, string propertyName)
		{
			var input = KnockoutTextboxFor(propertyName);
			row.Cell().Child(input);
			return input;
		}

		public static HtmlTag KnockoutDisplayFor(this TableRowTag row, string propertyName)
		{
			var input = KnockoutDisplayFor(propertyName);
			row.Cell().Child(input);
			return input;
		}
	}
}