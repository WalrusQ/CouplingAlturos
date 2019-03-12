using System.Collections.Generic;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Abstractions
{
	public interface ILogger
	{
		List<LogMessage> Messages { get; }
		void WriteLine(string message);
		void Save(string path);
        void Clear();
	}
}