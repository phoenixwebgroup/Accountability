namespace Accountability.WebApp
{
	using System.Web.Mvc;
	using GotFour.Windsor;

	public class WebRegistry : ExtendedRegistryBase
	{
		public WebRegistry()
		{
			RegisterControllerFactory();
			RegisterControllers();
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