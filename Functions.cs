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
        //public static Point GetCloserPoint(Point P1, Point P2, Point P)
        //{
        //    double distanceTo1 = CalculateDistance(P1, P);
        //    double distanceTo2 = CalculateDistance(P, P2);

        //    if (distanceTo1 < distanceTo2)
        //    {
        //        return P1;
        //    }
        //    else
        //    {
        //        return P2;
        //    }
        //}
       

    }
}
