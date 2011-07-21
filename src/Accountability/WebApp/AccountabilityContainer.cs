namespace Accountability.WebApp
{
	using Castle.Core;
	using Castle.MicroKernel.Registration;
	using Castle.MicroKernel.Releasers;
	using Castle.MicroKernel.Resolvers.SpecializedResolvers;
	using Castle.Windsor;
	using CommonServiceLocator.WindsorAdapter;
	using Microsoft.Practices.ServiceLocation;

	public class AccountabilityContainer : WindsorContainer
	{
		public static AccountabilityContainer Instance { get; set; }

		public AccountabilityContainer()
		{
			Instance = this;
			InjectGod();
			AddListResolver();
			SetTransientAsDefaultLifestyle();
			SetReleasePolicy();
			RegisterCSL();
		}

		private void SetReleasePolicy()
		{
			Kernel.ReleasePolicy = new NoTrackingReleasePolicy();
		}

		private void InjectGod()
		{
			Register(Component.For<IWindsorContainer>().Instance(this).LifeStyle.Singleton);
		}

		private void SetTransientAsDefaultLifestyle()
		{
			Kernel.ComponentModelCreated += Kernel_ComponentModelCreated;
		}

		private void RegisterCSL()
		{
			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(this));
			Register(Component.For<IServiceLocator>().Instance(ServiceLocator.Current).LifeStyle.Singleton);
		}

		private static void Kernel_ComponentModelCreated(ComponentModel model)
		{
			if (model.LifestyleType == LifestyleType.Undefined)
			{
				model.LifestyleType = LifestyleType.Transient;
			}
		}

		private void AddListResolver()
		{
			Kernel.Resolver.AddSubResolver(new ListResolver(Kernel));
		}

		public void Reset()
		{
			Instance = new AccountabilityContainer();
		}
	}
}