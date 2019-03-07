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
		public bool IsCanceling { get; set; }
		public bool IsStarted { get; set; }

		private IImageDetector ImageDetector { get; }

		public VideoDetector(IImageDetector imageDetector)
		{
			ImageDetector = imageDetector;
		}

		public void Process(string filename, IProgress<RecognitionResult> progress)
		{
			IsStarted = true;
			using (var reader = new VideoFileReader())
			{
				reader.Open(filename);

				while (reader.IsOpen)
				{
					if(IsCanceling) break;

					var frame = reader.ReadVideoFrame();
					var result = ImageDetector.Process(frame);
	
					progress.Report(result); //todo: Вместо progress сделать event

					frame.Dispose();
				}

				reader.Close();
			}
			IsStarted = false;
		}
	}
}