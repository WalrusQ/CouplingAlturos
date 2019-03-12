using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouplingAlturos.Core.Models
{
    public class Main
    {
        public List<int> TopLeft { get; set; }
        public List<int> BotRight { get; set; }
    }

    public class Alternative
    {
        public List<int> Center { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class EachPoint
    {
        public EachPoint(string _Comment, List<int> _Point)
        {
            Comment = _Comment;
            Point = _Point;
        }
        public string Comment { get; set; }
        public List<int> Point { get; set; }
    }

    public class Region
    {
        public Main Main { get; set; }
        public Alternative Alternative { get; set; }
        public List<EachPoint> EachPoint { get; set; }
    }

    public class RootObject
    {
        public string Team { get; set; }
        public string ImageName { get; set; }
        public Region Region { get; set; }
    }
    
    public class ImageLogger
    {
        public RootObject obj { get; set; }
        public ImageLogger(IRecognitionResult result, string imageName)
        {
            obj = new RootObject();
            obj.ImageName = imageName;
            obj.Team = "team6";
            foreach (var item in result.Items)
            {
                var xc = item.X;
                var yc = item.Y;
                var w = item.Width;
                var h = item.Height;
                obj.Region.Main.TopLeft = new List<int> { (xc - w / 2), (yc + h / 2) };
                obj.Region.Main.BotRight = new List<int> { (xc + w / 2), (yc - h / 2) };
                obj.Region.Alternative.Center = new List<int> { xc, yc };
                obj.Region.Alternative.Width = w;
                obj.Region.Alternative.Height = h;
                obj.Region.EachPoint = new List<EachPoint> { new EachPoint("top - left, (x; y)", new List<int> { (xc - w / 2), (yc + h / 2) }), new EachPoint("top-right, (x; y)", new List<int> { (xc - w / 2), (yc + h / 2) }) };
            }
            
        }
    }
}

