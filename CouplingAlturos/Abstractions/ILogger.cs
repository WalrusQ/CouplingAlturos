namespace CouplingAlturos.Abstractions
{
	public interface ILogger
	{
		void WriteLine(string message);
		void Save(string path);
	}
}