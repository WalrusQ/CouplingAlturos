using System;

namespace CouplingAlturos.Core.Models
{
	public class LogMessage
	{
		public DateTime Written { get; }

		public string Message { get; }

		public LogMessage(string message)
		{
			Message = message;
			Written = DateTime.Now;
		}
	}
}