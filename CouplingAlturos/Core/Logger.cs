using System.Collections.Generic;
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

		public void WriteLine(string message)
		{
            Messages.Add(new LogMessage(message));
		}

		public void Save(string path)
		{
			var strings = Messages.Select(x => x.ToString()); // todo: массив строк их и сохрани

            foreach(var msg in Messages)
            {
				//todo: save
				// — Сложно?
				//Я переопределил у LogMessage ф-ию ToString, теперь она норм текст отдаст
				var test = msg.ToString(); //


            }
		}
	}
}