using System;
using System.Threading;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class VideoThreadManager : IVideoThreadManager
	{
		private IVideoDetector VideoDetector { get; }

		private Thread _thread;

		public VideoThreadManager(IVideoDetector videoDetector)
		{
			VideoDetector = videoDetector;
		}

		public void Start(string filename, IProgress<RecognitionResult> progress)
		{
			if(_thread?.ThreadState == ThreadState.Background 
			   && _thread?.ThreadState == ThreadState.Running) throw new ThreadStateException();

			VideoDetector.IsCanceling = false;

			_thread = new Thread(() => VideoDetector.Process(filename, progress))
			{
				IsBackground = true,
				Name = "VideoDecoderThread"
			};
			_thread.Start();
		}

		public void Stop() => VideoDetector.IsCanceling = true;

		public void Abort() => _thread.Abort();
	}
}
