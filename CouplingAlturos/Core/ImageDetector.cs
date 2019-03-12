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
            _yolo = new YoloWrapper(GetConfiguration(), Constants.GpuEnabled);
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

        public IRecognitionResult Process(Image image)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var bytes = image.ToByteArray();
            var items = _yolo.Detect(bytes);
            stopwatch.Stop();

            return new ImageRecognitionResult
            {
                ElapsedTime = stopwatch.Elapsed,
                Items = items,
                ImageBytes = bytes
            };
        }
    }
}