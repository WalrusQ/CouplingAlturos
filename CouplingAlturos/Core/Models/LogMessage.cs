using System;

namespace CouplingAlturos.Core.Models
{
	public class LogMessage
	{
		public DateTime MessageTime { get; }

		public string Message { get; }

		public LogMessage(string message)
		{
			//Посмотри сюды
			MessageTime = DateTime.Now;
            Message = MessageTime.ToString() + "\n" + message;
        }
	}
}