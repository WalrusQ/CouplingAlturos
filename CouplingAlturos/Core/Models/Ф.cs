using System.Collections.Generic;
using System.Linq;

namespace CouplingAlturos.Core.Models
{
	public class Item
	{
		public Item(IRecognitionResult result)
		{
			IsDetected = result.Items.Any();
		}		

		public bool IsDetected { get; }
 	}

	public class FramesValidator
	{
		public FramesValidator(List<Item> frames)
		{
			Frames = frames;
		}

		private const double PercentValidity = .75;

		public List<Item> Frames { get; }

		public bool IsValid => (double) Frames.Count(x => x.IsDetected) / (double) Frames.Count > PercentValidity;
	}
}