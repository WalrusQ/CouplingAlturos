using Autofac;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core;

namespace CouplingAlturos
{
	public class RegistrationService
	{
		public static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();

			builder
				.RegisterType<ImageDetector>()
				.As<IImageDetector>()
				.SingleInstance();

			builder
				.RegisterType<VideoDetector>()
				.As<IVideoDetector>();

			builder
				.RegisterType<ThreadManager>()
				.As<IThreadManager>();

			builder
				.RegisterType<Logger>()
				.As<ILogger>()
				.SingleInstance();

			builder
				.RegisterType<ViewResolver>()
				.As<IViewResolver>();

			builder
				.RegisterType<MainForm>()
				.AsSelf();

			var container = builder.Build();

			return container;
		}
	}
}