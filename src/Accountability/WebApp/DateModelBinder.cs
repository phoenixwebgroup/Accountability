namespace Accountability.WebApp
{
	using System.Web.Mvc;
	using BclExtensionMethods.ValueTypes;

	public class DateModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueProviderResult == null)
			{
				return base.BindModel(controllerContext, bindingContext);
			}
			var value = valueProviderResult.AttemptedValue;
			try
			{
				return new Date(value);
			}
			catch
			{
				return null;
			}
		}
	}
}