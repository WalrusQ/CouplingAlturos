using System;
using System.Threading;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class VideoReaderThreadManager : IVideoReaderThreadManager
	{
		private IVideoReader VideoReader { get; }

		private Thread _thread;

		public VideoReaderThreadManager(IVideoReader videoReader)
		{
			VideoReader = videoReader;
		}

		public void Start(string filename, IProgress<VideoRecognitionResult> progress)
		{
			if(_thread?.ThreadState == ThreadState.Background 
			   && _thread?.ThreadState == ThreadState.Running) throw new ThreadStateException();

			VideoReader.IsCanceling = false;

			_thread = new Thread(() => VideoReader.Read(filename, progress))
			{
				IsBackground = true,
				Name = "VideoDecoderThread"
			};
			_thread.Start();
		}

		public void Stop() => VideoReader.IsCanceling = true;

		public void Abort() => _thread.Abort();
	}
}
