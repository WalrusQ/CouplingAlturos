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
        public string Comment { get; set; }
        public List<int> Point { get; set; }
    }

    public class Region
    {
        public Main Main { get; set; }
        public Alternative Alternative { get; set; }
        public List<EachPoint> EachPoint { get; set; }
    }

    public class RecognitionResultJsonObject
    {
        public string Team { get; set; }
        public string ImageName { get; set; }
        public Region Region { get; set; }
    }
}

