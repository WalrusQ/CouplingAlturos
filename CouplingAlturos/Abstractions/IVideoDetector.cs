using System;
using System.Threading.Tasks;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public interface IVideoDetector
	{
		bool IsCanceling { get; set; }
		bool IsStarted { get; set; }

		void Process(string filename, IProgress<RecognitionResult> progress);
	}
}