using System;

using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Abstractions
{
	public interface IThreadManager
	{
		/// <summary>
		/// Запускает поток
		/// </summary>
		void Start(Action action);

		/// <summary>
		/// Останавливает поток
		/// </summary>
		void Stop();

		/// <summary>
		/// Принудительно останавливает поток
		/// </summary>
		void Abort();
	}
}
