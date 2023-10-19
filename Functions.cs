using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        public static void DrawPolygon(PaintEventArgs e, List<Polygon> polygons, int vertexSize, int mouseOffset, bool isOffset)
        {
            
            foreach (var polygon in polygons)
            {
                foreach (var vertex in polygon.vertices)
                {
                    e.Graphics.FillEllipse(Brushes.Black,  vertex.point.X - mouseOffset, vertex.point.Y - mouseOffset, vertexSize ,vertexSize);
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
        public static void MovePolygonByMouse(Polygon? polygon, MouseEventArgs e, Point prevMousePosition)
        {
            if(polygon != null)
            {
                foreach (var vertex in polygon.vertices)
                {
                    MoveVertexByMouse(vertex, e, prevMousePosition);
                }
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
        public static Polygon OffsetPolygon(Polygon originalPolygon, float offsetDistance)
        {
            Polygon offsetPolygon = new Polygon(new List<Vertex>());

            for (int i = 0; i < originalPolygon.vertices.Count; i++)
            {
                Vertex currentVertex = originalPolygon.vertices[i];
                Vertex prevVertex = originalPolygon.vertices[(i - 1 + originalPolygon.vertices.Count) % originalPolygon.vertices.Count];
                Vertex nextVertex = originalPolygon.vertices[(i + 1) % originalPolygon.vertices.Count];

                Vector2 edge = new Vector2(nextVertex.point.X - currentVertex.point.X, nextVertex.point.Y - currentVertex.point.Y);
                Vector2 prevEdge = new Vector2(currentVertex.point.X - prevVertex.point.X, currentVertex.point.Y - prevVertex.point.Y);

                Vector2 edgeNormal = new Vector2(-edge.Y, edge.X);
                Vector2 prevEdgeNormal = new Vector2(-prevEdge.Y, prevEdge.X);
                edgeNormal = Vector2.Normalize(edgeNormal);
                prevEdgeNormal = Vector2.Normalize(prevEdgeNormal);

                Vector2 offsetVector = edgeNormal + prevEdgeNormal;
                offsetVector = Vector2.Normalize(offsetVector);

                Point offsetPoint = new Point(
                    (int)(currentVertex.point.X + offsetVector.X * offsetDistance),
                    (int)(currentVertex.point.Y + offsetVector.Y * offsetDistance));

                // Ensure the offset polygon remains outside the original polygon
                if (IsInsideOriginalPolygon(offsetPoint, originalPolygon))
                {
                    // If offsetPoint is inside the original polygon, invert the offset direction
                    offsetPoint = new Point(
                        (int)(currentVertex.point.X - offsetVector.X * offsetDistance),
                        (int)(currentVertex.point.Y - offsetVector.Y * offsetDistance));
                }

                offsetPolygon.vertices.Add(new Vertex(offsetPoint, null, null));
            }

            for (int i = 0; i < offsetPolygon.vertices.Count; i++)
            {
                int prevIndex = (i - 1 + offsetPolygon.vertices.Count) % offsetPolygon.vertices.Count;
                int nextIndex = (i + 1) % offsetPolygon.vertices.Count;

                offsetPolygon.vertices[i].prev = offsetPolygon.vertices[prevIndex];
                offsetPolygon.vertices[i].next = offsetPolygon.vertices[nextIndex];
            }

            // Check and resolve self-intersections
            offsetPolygon = ResolveSelfIntersections(offsetPolygon);

            return offsetPolygon;
        }

        public static bool IsInsideOriginalPolygon(Point point, Polygon originalPolygon)
        {
            int numIntersections = 0;

            for (int i = 0; i < originalPolygon.vertices.Count; i++)
            {
                Point vertex1 = originalPolygon.vertices[i].point;
                Point vertex2 = originalPolygon.vertices[(i + 1) % originalPolygon.vertices.Count].point;

                // Check if the point is on the edge of the polygon
                if (vertex1 == point)
                    return true;

                // Ensure vertex2 has a greater Y-coordinate
                if (vertex1.Y > vertex2.Y)
                {
                    Point temp = vertex1;
                    vertex1 = vertex2;
                    vertex2 = temp;
                }

                // Check if the point is within the Y-range of this edge
                if (point.Y > vertex1.Y && point.Y <= vertex2.Y && point.X <= Math.Max(vertex1.X, vertex2.X))
                {
                    // Calculate the intersection point's X-coordinate
                    double xIntersection = (point.Y - vertex1.Y) * (vertex2.X - vertex1.X) / (vertex2.Y - vertex1.Y) + vertex1.X;

                    // Check if the intersection point is to the left of the point in question
                    if (point.X <= xIntersection)
                    {
                        numIntersections++;
                    }
                }
            }

            return numIntersections % 2 == 1;
        }


        public static Polygon ResolveSelfIntersections(Polygon offsetPolygon)
        {
            bool intersectionsFound = true;

            while (intersectionsFound)
            {
                intersectionsFound = false; // Reset the flag
                List<Vertex> newVertices = new List<Vertex>();

                for (int i = 0; i < offsetPolygon.vertices.Count; i++)
                {
                    Vertex vertexA = offsetPolygon.vertices[i];
                    Vertex vertexB = offsetPolygon.vertices[(i + 1) % offsetPolygon.vertices.Count];
                    bool intersectionOccurred = false;

                    for (int j = i + 2; j < offsetPolygon.vertices.Count; j++)
                    {
                        Vertex vertexC = offsetPolygon.vertices[j];
                        Vertex vertexD = offsetPolygon.vertices[(j + 1) % offsetPolygon.vertices.Count];

                        // Check if the line segment (vertexA, vertexB) intersects with (vertexC, vertexD)
                        if (DoLineSegmentsIntersect(vertexA.point, vertexB.point, vertexC.point, vertexD.point))
                        {
                            // Calculate the intersection point and replace the vertices with it
                            Point intersectionPoint = CalculateIntersectionPoint(vertexA.point, vertexB.point, vertexC.point, vertexD.point);

                            // Create a new polygon with the vertices from the original to the intersection point
                            Polygon newPolygon = new Polygon(newVertices);
                            newPolygon.vertices.Add(new Vertex(intersectionPoint, null, null));

                            // Replace the vertices in the original polygon with the new vertices
                            offsetPolygon.vertices.RemoveRange(i + 1, j - i);
                            offsetPolygon.vertices.InsertRange(i + 1, newPolygon.vertices);

                            intersectionsFound = true; // Set the flag to continue checking
                            intersectionOccurred = true;
                            break; // Exit the inner loop to start checking again
                        }
                    }

                    if (!intersectionOccurred)
                    {
                        newVertices.Add(vertexA);
                    }
                }

                if (intersectionsFound)
                {
                    // Calculate the index for the vertices to be replaced
                    int startIndex = offsetPolygon.vertices.IndexOf(newVertices.First());
                    int count = newVertices.Count;

                    // Ensure that startIndex is within the valid range
                    if (startIndex >= 0)
                    {
                        // Remove the old vertices and insert the new ones
                        offsetPolygon.vertices.RemoveRange(startIndex, count);
                        offsetPolygon.vertices.InsertRange(startIndex, newVertices);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return offsetPolygon;
        }



        public static bool DoLineSegmentsIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            int x3 = p3.X;
            int y3 = p3.Y;
            int x4 = p4.X;
            int y4 = p4.Y;

            // Calculate the cross product
            int crossProduct1 = (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1);
            int crossProduct2 = (x2 - x1) * (y4 - y1) - (y2 - y1) * (x4 - x1);

            // Check if the line segments intersect (cross products have opposite signs)
            if ((crossProduct1 > 0 && crossProduct2 < 0) || (crossProduct1 < 0 && crossProduct2 > 0))
            {
                // Calculate the cross product for the other pair of line segments
                int crossProduct3 = (x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3);
                int crossProduct4 = (x4 - x3) * (y2 - y3) - (y4 - y3) * (x2 - x3);

                // Check if the line segments intersect (cross products have opposite signs)
                if ((crossProduct3 > 0 && crossProduct4 < 0) || (crossProduct3 < 0 && crossProduct4 > 0))
                {
                    return true; // Segments intersect
                }
            }

            return false; // Segments do not intersect
        }



        public static Point CalculateIntersectionPoint(Point p1, Point p2, Point p3, Point p4)
        {
            double m1 = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
            double m2 = (double)(p4.Y - p3.Y) / (p4.X - p3.X);

            double x = (m1 * p1.X - m2 * p3.X + p3.Y - p1.Y) / (m1 - m2);
            double y = m1 * (x - p1.X) + p1.Y;

            return new Point((int)Math.Round(x), (int)Math.Round(y)); // Round to the nearest integer.
        }

        public static bool IsConvexHullVertex(Vertex vertex, List<Vertex> convexHull)
        {
            // Sprawdzamy, czy wierzchołek jest częścią otoczki
            foreach (var hullVertex in convexHull)
            {
                if (vertex.point == hullVertex.point)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

