using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Accord.IO;
using CouplingAlturos.Core.Models;

namespace CouplingAlturos.Core.Extensions
{
	public static class RecognitionResultSaver
	{
		public static void SaveToJson(this IRecognitionResult result, string outputFile, string imageName)
		{
			foreach (var item in result.Items)
			{
				var xc = item.X;
				var yc = item.Y;
				var w = item.Width;
				var h = item.Height;

				var jsonObject = new RecognitionResultJsonObject
				{
					ImageName = imageName,
					Team      = Constants.TeamName,
					Region    = new Region
					{
						Main = new Main
						{
							TopLeft  = new int[] { xc - w / 2, yc + h / 2 }.ToList(),
							BotRight = new int[] { xc + w / 2, yc - h / 2 }.ToList(),
						},
						Alternative = new Alternative
						{ 
							Center = new int[] { xc, yc }.ToList(),
							Height = h,
							Width  =  w,
						},
						EachPoint = new EachPoint[]
						{
							new EachPoint
							{
								Comment = "top-left, (x; y)",
								Point = new int[] { xc - w / 2, yc + h / 2  }.ToList(),
							},
							new EachPoint
							{
								Comment = "top-right, (x; y)",
								Point   = new int[] { xc + w / 2, yc + h / 2  }.ToList(),
							},
							new EachPoint
							{
								Comment = "bottom-right, (x; y)",
								Point   = new int[] { xc + w / 2, yc - h / 2  }.ToList(),
							},
							new EachPoint
							{
								Comment = "bottom-left, (x; y)",
								Point   = new int[] { xc - w / 2, yc - h / 2  }.ToList(),
							},
						}.ToList()
					}
				};


			}
		}
	}
}