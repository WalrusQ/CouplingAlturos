using System.Collections.Generic;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class Logger : ILogger
	{   

		public List<LogMessage> Messages { get; }

        public Logger()
        {
            Messages = new List<LogMessage>();
        }

		public void WriteLine(string message)
		{
            Messages.Add(new LogMessage(message));
		}

		public void Save(string path)
		{
            foreach(var msg in Messages)
            {
                //todo: save
            }
		}
	}
}