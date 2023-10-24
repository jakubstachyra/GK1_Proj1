using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Proj1
{
    internal class Vertex
    {
        public Point point { get; set; }
        public Vertex? next { get; set; }
        public Vertex? prev { get; set; }
        public Relation relation { get; set; }
        public Vertex(Point point, Vertex? prev, Vertex? next)
        {
            this.point = point;
            this.next = next;
            this.prev = prev;
            this.relation = Relation.None;
        }
        public Vertex(Point point, Vertex? prev, Vertex? next, Relation relation)
        {
            this.point = point;
            this.next = next;
            this.prev = prev;
            this.relation = relation;
        }
    }
    internal class Polygon
    {
        public List<Vertex> vertices;
        public Polygon(List<Vertex> points)
        {
            this.vertices = new List<Vertex>(points);
        }
    }
    enum Relation {
        Horizontal,
        Vertical,
        None,
    }

}
