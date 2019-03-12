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