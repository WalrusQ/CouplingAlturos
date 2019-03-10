using System;

using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Abstractions
{
	public interface IVideoThreadManager
	{
		/// <summary>
		/// Запускает поток распознавания видеофайла
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <param name="progress">Callback</param>
		void Start(string filename, IProgress<VideoRecognitionResult> progress);

		/// <summary>
		/// Останавливает поток распознавания видеофайла
		/// </summary>
		void Stop();

		/// <summary>
		/// Принудительно останавливает поток распознавания видеофайла
		/// </summary>
		void Abort();
	}
}
