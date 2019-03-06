using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Alturos.Yolo;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Converters;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class ImageDetector : IImageDetector
	{
		private readonly YoloWrapper _yolo;

		public YoloMetaInfo YoloMetaInfo { get; }

		public ImageDetector()
		{
			var stopwatch = new Stopwatch();

			stopwatch.Start();
			_yolo = new YoloWrapper(GetConfiguration(), true);
			stopwatch.Stop();

			YoloMetaInfo = new YoloMetaInfo()
			{
				DetectionSystem = _yolo.DetectionSystem.ToString(),
				GraphicDeviceName = _yolo.EnvironmentReport.GraphicDeviceName,
				ElapsedTime = stopwatch.Elapsed,
			};
		}

		private YoloConfiguration GetConfiguration()
		{
			var configurationDetector = new ConfigurationDetector();
			var config = configurationDetector.Detect(Constants.ResourceFolder);
			return config;
		}

		public RecognitionResult Process(Image image)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var items = _yolo.Detect(image.ToByteArray());
			stopwatch.Stop();

			return new RecognitionResult
			{
				ElapsedTime = stopwatch.Elapsed,
				Items = items,
				Image = image
			};
		}
	}
}