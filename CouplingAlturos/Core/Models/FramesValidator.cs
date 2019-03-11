using System.Collections.Generic;
using System.Linq;

namespace CouplingAlturos.Core.Models
{
	public class FramesValidator
	{
		public FramesValidator(List<IRecognitionResult> frames)
		{
			Frames = frames;
		}

		private const double PercentValidity = .75;

		public List<IRecognitionResult> Frames { get; }

		public bool IsValid => (double) Frames.Count(x => x.Items.Any()) / (double) Frames.Count > PercentValidity;
	}
}