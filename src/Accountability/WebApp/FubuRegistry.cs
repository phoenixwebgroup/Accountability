namespace Accountability.WebApp
{
	using Castle.Windsor;
	using FubuCore;
	using FubuMVC.UI;
	using FubuMVC.UI.Configuration;
	using FubuMVC.UI.Tags;
	using GotFour.Windsor;
	using HtmlTags.UI.Conventions;

	public class FubuRegistry : ExtendedRegistryBase, IRunOnApplicationStart
	{
		public FubuRegistry()
		{
			AddComponent<HtmlConventionRegistry, HtmlConventions>();
			AddComponent<IElementNamingConvention, DottedElementNamingConvention>();
			For<Stringifier>();
			For<TagProfileLibrary>().LifeStyle.Singleton();
			For(typeof (ITagGenerator<>)).ImplementedBy(typeof (TagGenerator<>));
			AddComponent<ITypeResolver, TypeResolver>();
		}

		public void Start(IWindsorContainer container)
		{
			var library = container.Resolve<TagProfileLibrary>();
			var conventions = container.Resolve<HtmlConventionRegistry>();
			library.ImportRegistry(conventions);
		}
	}
}