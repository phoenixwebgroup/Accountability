namespace Accountability.WebApp
{
	using System.Web;
	using System.Web.Mvc;
	using BclExtensionMethods;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using HtmlTags.UI.Helpers;
	using Spark.Web.Mvc;

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			Container = new AccountabilityContainer();
			SetupComponents(Container);
			SetControllerFactory(Container);
			ViewEngines.Engines.Add(new SparkViewFactory());
			HtmlContentExtensions.DefaultScriptLocation = "~/Scripts/";
			HtmlContentExtensions.DefaultStyleSheetLocation = "~/Content/";
		}

		protected AccountabilityContainer Container
		{
			get { return (Application.Get("ServiceLocator") as AccountabilityContainer); }
			set { Application.Set("ServiceLocator", value); }
		}

		protected virtual void SetupComponents(IWindsorContainer container)
		{
			LoadRegistrations(container);
			ScanIn<IRunOnApplicationStart>(container);
			Init(container);
			AccountabilityContainer.Instance.ExpectAllRegistrationsAreValid();
		}

		private void SetControllerFactory(IWindsorContainer container)
		{
			var factory = container.Resolve<IControllerFactory>();
			ControllerBuilder.Current.SetControllerFactory(factory);
		}

		private static void LoadRegistries(IWindsorContainer container)
		{
			container
				.ResolveAll<IWindsorInstaller>()
				.ForEach(r => container.Install(r));
		}

		public void ScanIn<T>(IWindsorContainer container)
		{
			container.Register(AllTypes.FromThisAssembly().BasedOn<T>());
		}

		public void Init(IWindsorContainer container)
		{
			container.ResolveAll<IRunOnApplicationStart>()
				.ForEach(i => i.Start(container));

			new HtmlConventions().Start(container);
		}

		public void LoadRegistrations(IWindsorContainer container)
		{
			ScanIn<IWindsorInstaller>(container);
			LoadRegistries(container);
		}
	}
}