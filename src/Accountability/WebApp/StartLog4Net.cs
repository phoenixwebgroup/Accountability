namespace Accountability.WebApp
{
	using Castle.Windsor;
	using UISkeleton.Infrastructure;
	using log4net.Config;

	public class StartLog4Net : IConfigureOnStartup
	{
		public void Configure(IWindsorContainer container)
		{
			XmlConfigurator.Configure();
		}
	}
}