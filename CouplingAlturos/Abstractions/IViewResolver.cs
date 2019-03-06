using System.Windows.Forms;

namespace CouplingAlturos.Abstractions
{
	public interface IViewResolver
	{
		T Resolve<T>() where T : Control;

		T Resolve<T>(params object[] parameters) where T : Control;
	}
}