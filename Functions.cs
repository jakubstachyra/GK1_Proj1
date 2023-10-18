using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GK_Proj1
{
    static class Functions
    {
        public static void BresenhamLine(UserControl usercontrol  , Point P1, Point P2, Color color, Bitmap canvas)
        {
            usercontrol.BackgroundImage = null;
            int dx = P2.X - P1.X;
            int dy = P2.Y - P1.Y;
            Point d1 = new Point(Math.Sign(dx), Math.Sign(dy));

            Point d2;
            int longerDim = Math.Abs(dx);
            int shorterDim = Math.Abs(dy);
            if (longerDim < shorterDim)
            {
                (longerDim, shorterDim) = (shorterDim, longerDim);
                d2 = new Point(0, d1.Y);
            }
            else
                d2 = new Point(d1.X, 0);
            
            int numerator = longerDim >> 1;

            for (int i = 0; i <= longerDim; ++i)
            {
                if (P1.X > 0 && P1.Y > 0 && P1.X < canvas.Width && P1.Y < canvas.Height)
                    canvas.SetPixel(P1.X, P1.Y, color);

                numerator += shorterDim;
                if (numerator >= longerDim)
                {
                    numerator -= longerDim;
                    P1.Offset(d1);
                }
                else
                {
                    P1.Offset(d2);
                }
            }
            usercontrol.BackgroundImage = canvas;

        }
        public static void DrawPolygon(PaintEventArgs e, List<Polygon> polygons, int vertexSize, int offset)
        {
            
            foreach (var polygon in polygons)
            {
                foreach (var vertex in polygon.vertices)
                {
                    e.Graphics.FillEllipse(Brushes.Black,  vertex.point.X - offset, vertex.point.Y - offset, vertexSize ,vertexSize);
                    if (vertex.next != null)
                    {
                        e.Graphics.DrawLine(Pens.Black, vertex.point, vertex.next.point);
                    }
                }
            }

        }
        public static bool isPointOnLine(Point P1, Point P2, Point P, int tolerance)
        {
            double distanceP1ToP2 = Math.Sqrt(Math.Pow(P2.X - P1.X, 2) + Math.Pow(P2.Y - P1.Y, 2));

            // Oblicz długość odcinka między P1 a P.
            double distanceP1ToP = Math.Sqrt(Math.Pow(P.X - P1.X, 2) + Math.Pow(P.Y - P1.Y, 2));

            // Oblicz długość odcinka między P2 a P.
            double distanceP2ToP = Math.Sqrt(Math.Pow(P2.X - P.X, 2) + Math.Pow(P2.Y - P.Y, 2));

            // Oblicz odległość punktu P od prostej między P1 i P2.
            double d = Math.Abs((P2.X - P1.X) * (P1.Y - P.Y) - (P1.X - P.X) * (P2.Y - P1.Y)) /
                       distanceP1ToP2;

            if (d <= tolerance && distanceP1ToP + distanceP2ToP <= distanceP1ToP2 + tolerance)
            {
                return true;
            }

            return false;
        }
        public static Point FindCenterOfLine(Point P1, Point P2)
        {
            return new Point((P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2);
        }
        public static double CalculateDistance(Point point1, Point point2)
        {
            int dx = point2.X - point1.X;
            int dy = point2.Y - point1.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
        public static bool IsPointInsidePolygon(Polygon polygon, MouseEventArgs e)
        {
            Point point = e.Location;
            int intersectCount = 0;

            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                Point vertex1 = polygon.vertices[i].point;
                Point vertex2 = polygon.vertices[(i + 1) % polygon.vertices.Count].point;

                if (IsPointOnSegment(vertex1, vertex2, point))
                {
                    return true; // Jeśli punkt znajduje się na krawędzi wielokąta, uważamy go za wewnątrz
                }

                if ((vertex1.Y < point.Y && vertex2.Y >= point.Y || vertex2.Y < point.Y && vertex1.Y >= point.Y) &&
                    (vertex1.X + (point.Y - vertex1.Y) / (vertex2.Y - vertex1.Y) * (vertex2.X - vertex1.X) < point.X))
                {
                    intersectCount++;
                }
            }

            return intersectCount % 2 == 1;
        }
        private static bool IsPointOnSegment(Point p1, Point p2, Point point)
        {
            return (point.X <= Math.Max(p1.X, p2.X) && point.X >= Math.Min(p1.X, p2.X) &&
                    point.Y <= Math.Max(p1.Y, p2.Y) && point.Y >= Math.Min(p1.Y, p2.Y));
        }
        public static void MovePolygonByMouse(Polygon polygon, MouseEventArgs e, Point prevMousePosition)
        {
            foreach (var vertex in polygon.vertices)
            {
                MoveVertexByMouse(vertex, e, prevMousePosition);
            }
        }
        public static void MoveLineByMouse(Vertex? editingVertex, MouseEventArgs e, Point prevMousePosition)
        {
            if (editingVertex != null && editingVertex.next != null)
            {
                int dx = e.Location.X - prevMousePosition.X;
                int dy = e.Location.Y - prevMousePosition.Y;

                // Aktualizuj pozycje wierzchołków linii wraz z linią
                editingVertex.point = new Point(editingVertex.point.X + dx, editingVertex.point.Y + dy);
                editingVertex.next.point = new Point(editingVertex.next.point.X + dx, editingVertex.next.point.Y + dy);
            }
        }
        public static void MoveVertexByMouse(Vertex vertex, MouseEventArgs e, Point prevMousePosition)
        {
            int dx = e.Location.X - prevMousePosition.X;
            int dy = e.Location.Y - prevMousePosition.Y;

            // Aktualizuj pozycję wierzchołka
            vertex.point = new Point(vertex.point.X + dx, vertex.point.Y + dy);
        }

    }
}

