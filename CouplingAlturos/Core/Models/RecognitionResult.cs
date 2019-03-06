using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Alturos.Yolo.Model;

namespace CouplingAlturos.Core.Models
{
	public class RecognitionResult
	{
		/// <summary>Затраченное время</summary>
		public TimeSpan ElapsedTime { get; set; }

		public IEnumerable<YoloItem> Items { get; set; }

		/// <summary>Исходное изображение</summary>
		public Image Image { get; set; }
	}
}