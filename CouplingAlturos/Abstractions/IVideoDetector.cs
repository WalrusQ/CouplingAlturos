using System;
using System.Threading.Tasks;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public interface IVideoDetector
	{
		void Process(string filename, IProgress<RecognitionResult> progress);
	}
}