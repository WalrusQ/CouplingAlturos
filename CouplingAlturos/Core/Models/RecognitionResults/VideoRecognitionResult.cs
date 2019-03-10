using System;
using System.Collections.Generic;
using System.Drawing;
using Alturos.Yolo.Model;

namespace CouplingAlturos.Core.Models
{
	public class VideoRecognitionResult : IRecognitionResult
	{
		public TimeSpan ElapsedTime { get; set; }

		public IEnumerable<YoloItem> Items { get; set; }

		public Image Image { get; set; }

		public long IndexFrame { get; set; }

		public long FrameRate { get; set; }
	}
}