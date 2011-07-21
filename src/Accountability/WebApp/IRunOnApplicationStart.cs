namespace Accountability.WebApp
{
	using Castle.Windsor;

	public interface IRunOnApplicationStart
	{
		void Start(IWindsorContainer container);
	}
}