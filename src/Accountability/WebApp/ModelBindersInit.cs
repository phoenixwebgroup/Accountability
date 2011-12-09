namespace Accountability.WebApp
{
	using System.Web.Mvc;
	using BclExtensionMethods.ValueTypes;
	using Castle.Windsor;
	using HtmlTags.UI.ModelBinders;
	using MongoDB.Bson;
	using UISkeleton.Infrastructure;

	public class ModelBindersInit : ModelBinderRegistryBase, IConfigureOnStartup
	{
		public void Configure(IWindsorContainer container)
		{
			ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
			DecimalModelBinder();
			ModelBinders.Binders.Add(typeof(ObjectId), new ObjectIdModelBinder());

			ModelBinders.Binders.Add(typeof(Date), new DateModelBinder());
			ModelBinders.Binders.Add(typeof(Date?), new DateModelBinder());
		}
	}
}