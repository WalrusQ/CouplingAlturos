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
				.As<IVideoDetector>()
				.SingleInstance();

			builder
				.RegisterType<MainForm>()
				.AsSelf();

			var container = builder.Build();

			return container;
		}
	}
}