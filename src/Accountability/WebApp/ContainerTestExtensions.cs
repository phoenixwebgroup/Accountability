namespace Accountability.WebApp
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using Castle.Core;
	using Castle.MicroKernel;
	using Castle.MicroKernel.SubSystems.Naming;
	using Castle.Windsor;

	public static class ContainerTestExtensions
	{
		public static void ExpectAllRegistrationsAreValid(this IWindsorContainer container)
		{
			// adapted from a future release of windsor's PotentiallyMisconfiguredComponents

			var items = GetPotentiallyMisconfiguredComponents(container);
			if (items.Any())
			{
				var messages = items.Select(i => GetMissingDependenciesMessage(i, container.Kernel)).ToArray();
				var missing = string.Join(Environment.NewLine, messages);
				throw new Exception("Registrations pending: " + Environment.NewLine + missing);
			}
		}

		private static string GetMissingDependenciesMessage(HandlerByKeyDebuggerView item, IKernel kernel)
		{
			var missing = string.Empty;
			var dependencies = item.Service.ComponentModel
				.Constructors
				.SelectMany(c => c.Dependencies)
				.Where(d => DependencyPending(d, kernel));


			foreach (var dependency in dependencies)
			{
				missing += string.Format("\tdependency with key {0} : {1}", dependency.DependencyKey, dependency.TargetType) +
				           Environment.NewLine;
			}
			return string.Format("For key {0}: {1}", item.Key, item.ServiceString) + Environment.NewLine + missing;
		}

		private static bool DependencyPending(DependencyModel dependency, IKernel kernel)
		{
			var handler = kernel.GetHandler(dependency.TargetType);
			return handler == null || handler.CurrentState == HandlerState.WaitingDependency;
		}

		private static HandlerByKeyDebuggerView[] GetPotentiallyMisconfiguredComponents(IWindsorContainer container)
		{
			var naming = container.Kernel.GetSubSystem(SubSystemConstants.NamingKey) as INamingSubSystem;

			var waitingComponents = naming.GetKey2Handler()
				.Where(h => h.Value.CurrentState == HandlerState.WaitingDependency)
				.ToArray();
			if (waitingComponents.Length == 0)
			{
				return new HandlerByKeyDebuggerView[0];
			}
			return new HandlersByKeyDictionaryDebuggerView(waitingComponents).Items;
		}
	}

	public class HandlersByKeyDictionaryDebuggerView
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly HandlerByKeyDebuggerView[] items;

		public HandlersByKeyDictionaryDebuggerView(IEnumerable<KeyValuePair<string, IHandler>> key2Handler)
		{
			items = key2Handler.Select(h => new HandlerByKeyDebuggerView(h.Key, h.Value)).ToArray();
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Count
		{
			get { return items.Length; }
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public HandlerByKeyDebuggerView[] Items
		{
			get { return items; }
		}
	}

	public class HandlerByKeyDebuggerView
	{
		public HandlerByKeyDebuggerView(string key, IHandler service)
		{
			Key = key;
			Service = service;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string Key { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public IHandler Service { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string ServiceString
		{
			get
			{
				var value = Service.Service.Name;
				var impl = Service.ComponentModel.Implementation;
				if (impl == Service.Service)
				{
					return value;
				}
				value += " / ";
				if (impl == null)
				{
					value += "no type";
				}
				else
				{
					value += impl.Name;
				}
				return value;
			}
		}

		public IEnumerable<DependencyModel> Dependencies()
		{
			return Service.ComponentModel.Dependencies.OfType<DependencyModel>();
		}
	}
}