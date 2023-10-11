using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Proj1
{
    internal class Line
    {
        public Point start { get; set; }
        public Point end { get; set; }

        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

    }
    internal class Polygon
    {
        public List<Tuple<Point, Rectangle>> vertices = new List<Tuple<Point, Rectangle>>();
        public List<Line> lines = new List<Line>();
        public Polygon(List<Tuple<Point, Rectangle>> points, List<Line> lines)
        {
            this.vertices = points;
            this.lines = lines;
        }
    }
}
