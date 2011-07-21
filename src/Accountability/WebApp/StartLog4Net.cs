namespace Accountability.WebApp
{
	using Castle.Windsor;
	using log4net.Config;

	public class StartLog4Net : IRunOnApplicationStart
	{
		public void Start(IWindsorContainer container)
		{
			XmlConfigurator.Configure();
		}
	}
}