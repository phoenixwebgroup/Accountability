namespace Accountability.WebApp
{
	using System;
	using System.Web.Mvc;
	using MongoDB.Bson;

	public class ObjectIdModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueProviderResult == null)
			{
				return base.BindModel(controllerContext, bindingContext);
			}
			var value = valueProviderResult.AttemptedValue;

			return String.IsNullOrEmpty(value)
					? ObjectId.Empty
					: new ObjectId(value);
		}
	}
}