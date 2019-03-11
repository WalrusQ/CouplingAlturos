using System;

namespace CouplingAlturos.Core.Models
{
	public class LogMessage
	{
		public DateTime Timestamp { get; }

		public string Message { get; }

		public LogMessage(string message)
		{
			//Посмотри сюды
			Timestamp = DateTime.Now;
            Message = message;
        }

		public override string ToString()
		{
			return $"{Timestamp:s} — {Message}";
		}
	}
}