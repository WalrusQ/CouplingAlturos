using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CouplingAlturos.Core.Converters
{
	public static class ImageConverter
	{
		public static byte[] ToByteArray(this Image image)
		{
			using (var ms = new MemoryStream())
			{
				image.Save(ms, ImageFormat.Bmp);
				return ms.ToArray();
			}
		}

		public static Image ToImage(this byte[] bytes)
		{
			using (var ms = new MemoryStream(bytes))
			{
				return Image.FromStream(ms);
			}
		}
	}
}