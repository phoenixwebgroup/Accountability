namespace Accountability.WebApp
{
	using System.Web.Mvc;
	using Authentication;
	using GotFour.Windsor;
	using UISkeleton.Infrastructure;

	public class WebRegistry : ExtendedRegistryBase
	{
		public WebRegistry()
		{
			RegisterControllerFactory();
			RegisterControllers();
			For<UserPrincipal>().LifeStyle.PerWebRequest();
			ScanMyAssembly(Conventions.FirstInterfaceIsIName);
		}

		private void RegisterControllerFactory()
		{
			AddComponent<IControllerFactory, WindsorControllerFactory>();
		}

		private void RegisterControllers()
		{
			ScanMyAssemblyFor<Controller>();
		}
	}
}