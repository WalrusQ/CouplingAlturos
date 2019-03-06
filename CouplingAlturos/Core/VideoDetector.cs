using System;
using System.IO;
using System.Threading.Tasks;
using Accord.Video.FFMPEG;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class VideoDetector : IVideoDetector
	{
		private IImageDetector ImageDetector { get; }

		public VideoDetector(IImageDetector imageDetector)
		{
			ImageDetector = imageDetector;
		}

		public Task Process(string filename, IProgress<RecognitionResult> progress)
		{
			using (var reader = new VideoFileReader())
			{
				reader.Open("test.avi");

				while (reader.IsOpen)
				{
					var frame = reader.ReadVideoFrame();
					var result = ImageDetector.Process(frame);
					progress.Report(result);
				}

				reader.Close();
			}

			return Task.CompletedTask;
		}
	}
}