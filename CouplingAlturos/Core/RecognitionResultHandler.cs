using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Accord.IO;
using CouplingAlturos.Core.Converters;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core
{
	public static class RecognitionResultHandler
	{
		public static void SaveToXml(this IRecognitionResult result, string outputFile, string imageName)
		{
			var settings = new XmlWriterSettings
			{
				Indent = true,
				NewLineOnAttributes = true,
			};
			var writer = XmlWriter.Create(outputFile, settings);

			foreach (var item in result.Items)
			{
				var xc = item.X;
				var yc = item.Y;
				var w = item.Width;
				var h = item.Height;

				writer.WriteStartElement("Object");
				writer.WriteStartElement("Team");
				writer.WriteAttributeString("Value", "team6");
				writer.WriteEndElement();
				writer.WriteAttributeString("ImageName", imageName);
				writer.WriteEndElement();
				writer.WriteStartElement("Region");
				writer.WriteStartElement("Main");
				writer.WriteStartElement("TopLeft");
				writer.WriteAttributeString("X", (xc - w / 2) + "");
				writer.WriteAttributeString("Y", (yc + h / 2) + "");
				writer.WriteEndElement();
				writer.WriteStartElement("BotRight");
				writer.WriteAttributeString("X", (xc + w / 2) + "");
				writer.WriteAttributeString("Y", (yc - h / 2) + "");
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteStartElement("Alternative");
				writer.WriteStartElement("Center");
				writer.WriteAttributeString("X", xc + "");
				writer.WriteAttributeString("Y", yc + "");
				writer.WriteEndElement();
				writer.WriteStartElement("Width");
				writer.WriteAttributeString("Value", w + "");
				writer.WriteEndElement();
				writer.WriteStartElement("Height");
				writer.WriteAttributeString("Value", h + "");
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteStartElement("EachPoint");
				writer.WriteStartElement("Point");
				writer.WriteAttributeString("X", (xc - w / 2) + "");
				writer.WriteAttributeString("Y", (yc + h / 2) + "");
				writer.WriteComment("top-left");
				writer.WriteEndElement();
				writer.WriteStartElement("Point");
				writer.WriteAttributeString("X", (xc + w / 2) + "");
				writer.WriteAttributeString("Y", (yc + h / 2) + "");
				writer.WriteComment("top-right");
				writer.WriteEndElement();
				writer.WriteStartElement("Point");
				writer.WriteAttributeString("X", (xc + w / 2) + "");
				writer.WriteAttributeString("Y", (yc - h / 2) + "");
				writer.WriteComment("bottom-right");
				writer.WriteEndElement();
				writer.WriteStartElement("Point");
				writer.WriteAttributeString("X", (xc - w / 2) + "");
				writer.WriteAttributeString("Y", (yc - h / 2) + "");
				writer.WriteComment("bottom-left");
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndElement();

				writer.Save();
			}
		}
        public static Image DrawBorder2Image(this IRecognitionResult result)
        {
            var image = result.ImageBytes.ToImage();
            using (var canvas = Graphics.FromImage(image))
            {
                foreach (var item in result.Items)
                {
                    using (var pen = new Pen(Brushes.GreenYellow, image.Width / 100))
                    {
                        canvas.DrawRectangle(pen, item.X, item.Y, item.Width, item.Height);
                        canvas.Flush();
                    }
                }
            }
            return image;
        }
    }

}