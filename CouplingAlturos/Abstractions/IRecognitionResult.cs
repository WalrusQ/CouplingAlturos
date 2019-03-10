using System;
using System.Collections.Generic;
using System.Drawing;
using Alturos.Yolo.Model;

namespace CouplingAlturos.Core.Models
{
	public interface IRecognitionResult
	{
		/// <summary>Затраченное время</summary>
		TimeSpan ElapsedTime { get; set; }

		IEnumerable<YoloItem> Items { get; set; }

		/// <summary>Исходное изображение</summary>
		byte[] ImageBytes { get; set; }
	}
}