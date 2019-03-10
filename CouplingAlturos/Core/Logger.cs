using System.Collections.Generic;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class Logger : ILogger
	{
		public List<LogMessage> Messages { get; }

		public void WriteLine(string message)
		{

		}

		public void Save(string path)
		{

		}
	}
}