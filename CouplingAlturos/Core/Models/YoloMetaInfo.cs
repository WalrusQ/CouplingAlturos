using System;

namespace CouplingAlturos.Core.Models
{
	public class YoloMetaInfo
	{
		/// <summary>Затраченное время на инициализацию</summary>
		public TimeSpan ElapsedTime { get; set; }

		/// <summary>Название GPU устройства</summary>
		public string GraphicDeviceName { get; set; }

		/// <summary>Система распознание</summary>
		public string DetectionSystem { get; set; }
	}
}