namespace Accountability.WebApp
{
	using System.Web.Mvc;
	using Castle.Windsor;
	using HtmlTags.UI.ModelBinders;
	using MongoDB.Bson;

	public class ModelBindersInit : ModelBinderRegistryBase, IRunOnApplicationStart
	{
		public void Start(IWindsorContainer container)
		{
			ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
			DecimalModelBinder();
			ModelBinders.Binders.Add(typeof (ObjectId), new ObjectIdModelBinder());
		}
	}
}