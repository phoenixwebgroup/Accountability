namespace Accountability.WebApp
{
	using Castle.Windsor;
	using FubuMVC.UI.Configuration;
	using HtmlTags;
	using HtmlTags.Constants;
	using HtmlTags.UI;
	using HtmlTags.UI.Attributes;
	using HtmlTags.UI.AutoComplete;
	using HtmlTags.UI.Builders;
	using HtmlTags.UI.Conventions;
	using HtmlTags.UI.Helpers;

	public class HtmlConventions : HtmlConventionsRegistryBase, IRunOnApplicationStart
	{
		public HtmlConventions()
		{
			RegisterBuilders();
			RegisterDefaults();
			RegisterModifiers();
		}

		private void RegisterBuilders()
		{
			Editors.IfPropertyTypeIsEnum().BuildBy(new EnumDropDownBuilder().Build);
			Editors.IfPropertyTypeIsNullableEnum().BuildBy(new EnumDropDownBuilder().Build);

			Editors.Builder<HiddenInputBuilder>();
			Editors.Builder<MultiSelectListBuilder>();
			Editors.Builder<AjaxSelectListBuilder>();
			Editors.Builder<SelectListBuilder>();
			Editors.Builder<CheckBoxListBuilder>();
			Editors.Builder<CheckBoxBuilder>();
			Editors.Builder<DateAndTimePickerBuilder>();
			Editors.Builder<DatePickerBuilder>();
			Editors.Builder<PasswordBuilder>();
			Editors.Builder<TextAreaBuilder>();
			Editors.Builder<AutoCompleteBuilder>();
			Editors.IfPropertyHasAttribute<ClickToEditAttribute>().Modify(ClickToEditModifier.ClickToEdit);

//			Displays.Builder<NumericDisplayBuilder>();
			Displays.Builder<DateOnlyBuilder>();
			Displays.Builder<YesOrNoBuilder>();
			Displays.Builder<FlagsEnumDisplayBuilder>();

			RegisterValidationModifiers();
		}

		private void RegisterModifiers()
		{
			Editors.Always.Modify(AddElementName);
		}

		private void RegisterDefaults()
		{
			Displays.Always.BuildBy(DefaultDisplay.Builder);
			Labels.Always.BuildBy(DefaultLabeler.Default);
			Editors.Always.BuildBy(DefaultEditor.Default);
		}

		public static void AddElementName(ElementRequest request, HtmlTag tag)
		{
			if (tag.IsInputElement())
			{
				tag.Attr(HtmlAttributeConstants.Name, request.ElementId);
			}
		}

		public void Start(IWindsorContainer container)
		{
			BaseElementBuilder.Security = new NoElementBuilderSecurity();
			SaveOrCancelConvention.Convention = new SaveOrCancelButtonsConvention();
			LabelingConvention.Convention = new SpaceBeforeCapitalsLabelingConvention();
			FiltersConventions.FilterButtonConvention = new FiltersFilterButtonConvention();
			FiltersConventions.ResetButtonConvention = new FiltersResetButtonConvention();
			PageActions.Convention = new PageActionConvention();
			PageActionConvention.Security = new NoPageActionsSecurity();
		}
	}
}