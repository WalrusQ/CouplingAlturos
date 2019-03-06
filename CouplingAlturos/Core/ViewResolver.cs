using System;
using System.Linq;
using System.Windows.Forms;

using Autofac;
using CouplingAlturos.Abstractions;

namespace CouplingAlturos.Core
{
	public sealed class ViewResolver : IViewResolver
	{
		#region Data

		private readonly IComponentContext _componentContext;

		#endregion

		#region .ctor

		public ViewResolver(IComponentContext componentContext)
		{
			_componentContext = componentContext;
		}

		#endregion

		#region Methods

		private void RegisterView<T>()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<T>().AsSelf().ExternallyOwned();
			builder.Update(_componentContext.ComponentRegistry);
		}

		#endregion

		#region IViewResolver Members

		public T Resolve<T>() where T : Control
		{
			if (!_componentContext.TryResolve<T>(out T view))
			{
				RegisterView<T>();
				view = _componentContext.Resolve<T>();
			}
			return view;
		}

		public T Resolve<T>(params object[] parameters) where T : Control
		{
			if (parameters == null || parameters.Length == 0)
			{
				return Resolve<T>();
			}
			if (!_componentContext.IsRegistered<T>())
			{
				RegisterView<T>();
			}
			return _componentContext.Resolve<T>(parameters.Select(p => new TypedParameter(p.GetType(), p)));
		}

		#endregion
	}
}
