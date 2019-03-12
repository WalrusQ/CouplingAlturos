using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void Clear()
        {
            Messages.Clear();
        }

		public void WriteLine(string message)
		{
            Messages.Add(new LogMessage(message));
		}

		public void Save(string path)
		{
			var strings = Messages.Select(x => x.ToString()); // todo: массив строк их и сохрани
            using (var writer = new StreamWriter(path))
            foreach(var msg in Messages)
            {
                    writer.WriteLine(msg.ToString());
            }
		}
	}
}