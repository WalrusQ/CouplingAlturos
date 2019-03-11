using System;
using System.Threading;
using CouplingAlturos.Abstractions;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public class ThreadManager : IThreadManager
	{
		private IVideoDetector VideoDetector { get; }

		private Thread _thread;

		public ThreadManager(IVideoDetector videoDetector)
		{
			VideoDetector = videoDetector;
		}

		public void Start(Action action)
		{
			if(_thread?.ThreadState == ThreadState.Background 
			   && _thread?.ThreadState == ThreadState.Running) throw new ThreadStateException();

			VideoDetector.IsCanceling = false;

			_thread = new Thread(() => action())
			{
				IsBackground = true,
				Name = action.Method.Name + "_Proccess"
			};
			_thread.Start();
		}

		public void Stop() => VideoDetector.IsCanceling = true;

		public void Abort() => _thread.Abort();
	}
}
