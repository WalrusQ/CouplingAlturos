using System;
using System.Threading.Tasks;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public interface IVideoReader
	{
		bool IsCanceling { get; set; }
		bool IsStarted { get; set; }

		void Read(string filename, IProgress<VideoRecognitionResult> progress);
	}
}