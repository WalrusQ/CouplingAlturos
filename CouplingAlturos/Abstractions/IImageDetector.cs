using System.Drawing;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Abstractions
{
	public interface IImageDetector
	{
		YoloMetaInfo YoloMetaInfo { get; }

		IRecognitionResult Process(Image image);
	}
}