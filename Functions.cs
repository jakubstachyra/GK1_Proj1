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
       static int eps = 4;
        public static void BresenhamLine( Point P1, Point P2, Color color, Bitmap canvas)
        {
            
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
            
        }
        public static void DrawPolygon(PaintEventArgs e, List<Polygon> polygons, int vertexSize, int mouseOffset, bool isBersenham, Bitmap canvas)
        {

            foreach (var polygon in polygons)
            {
                foreach (var vertex in polygon.vertices)
                {
                    e.Graphics.FillEllipse(Brushes.Black, vertex.point.X - mouseOffset, vertex.point.Y - mouseOffset, vertexSize, vertexSize);
                    if (vertex.next != null)
                    {
                        if (isBersenham)
                        {
                            BresenhamLine(vertex.point, vertex.next.point, Color.Green, canvas );
                        }
                        else
                        {
                            e.Graphics.DrawLine(Pens.Black, vertex.point, vertex.next.point);
                        }
                        if (vertex.nextRelation == vertex.next.prevRelation && vertex.nextRelation != Relation.None)
                        {
                            Pen boldPen = new Pen(Color.Green, 3);
                            var center = Functions.FindCenterOfLine(vertex.point, vertex.next.point);
                            center.Y = center.Y + eps;
                           
                            if (vertex.nextRelation == Relation.Horizontal)
                            {
                                var center2 = center;
                                center.X = center.X - eps;
                                center2.X = center2.X + eps;
                                e.Graphics.DrawLine(boldPen, center, center2);
                                center.Y = center.Y - 2 * eps;
                                center2.Y = center2.Y - 2 * eps;
                                e.Graphics.DrawLine(boldPen, center, center2);
                            }
                            if(vertex.nextRelation == Relation.Vertical)
                            {
                                var center2 = center;
                                center2.Y = center2.Y - 2 * eps;
                                e.Graphics.DrawLine(boldPen, center, center2);
                                center.X = center2.X - eps;
                                center2.Y = center.Y;
                                center2.X = center2.X + eps;
                                e.Graphics.DrawLine(boldPen, center, center2);
                            }

                        }
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
            if (polygon != null)
            {
                foreach (var vertex in polygon.vertices)
                {
                    MoveVertexByMouse(vertex, e, prevMousePosition,true);
                }
            }
        }
        public static void MoveLineByMouse(Vertex? editingVertex, MouseEventArgs e, Point prevMousePosition)
        {
            if (editingVertex != null && editingVertex.next != null && editingVertex.prev != null)
            {
                int dx = e.Location.X - prevMousePosition.X;
                int dy = e.Location.Y - prevMousePosition.Y;

                // Aktualizuj pozycje wierzchołków linii wraz z linią
                editingVertex.point = new Point(editingVertex.point.X + dx, editingVertex.point.Y + dy);      
                editingVertex.next.point = new Point(editingVertex.next.point.X + dx, editingVertex.next.point.Y + dy);
                
                
                if (editingVertex.next != null && editingVertex.nextRelation == Relation.Horizontal && editingVertex.next.prevRelation == Relation.Horizontal)
                {
                    editingVertex.next.point = new Point(editingVertex.next.point.X, editingVertex.point.Y);
                    var centre = FindCenterOfLine(editingVertex.point, editingVertex.next.point);
                    //Graphics.DrawImage(imageHorizontal, centre);
                }
                if (editingVertex.next.next != null && editingVertex.next.nextRelation == Relation.Horizontal && editingVertex.next.next.prevRelation == Relation.Horizontal)
                {
                    editingVertex.next.next.point = new Point(editingVertex.next.next.point.X, editingVertex.next.point.Y);
                }
                if (editingVertex.prev != null && editingVertex.prevRelation == Relation.Horizontal && editingVertex.prev.nextRelation == Relation.Horizontal)
                {
                    editingVertex.prev.point = new Point(editingVertex.prev.point.X, editingVertex.point.Y);
                }
                if (editingVertex.prev.prev != null && editingVertex.prev.prev.prevRelation == Relation.Horizontal && editingVertex.prev.prev.nextRelation == Relation.Horizontal)
                {
                    editingVertex.prev.prev.point = new Point(editingVertex.prev.prev.point.X, editingVertex.prev.point.Y);
                }

                
                if (editingVertex.next != null && editingVertex.nextRelation == Relation.Vertical && editingVertex.next.prevRelation == Relation.Vertical)
                {
                    editingVertex.next.point = new Point(editingVertex.point.X, editingVertex.next.point.Y);
                }
                if (editingVertex.prev != null && editingVertex.prevRelation == Relation.Vertical && editingVertex.prev.nextRelation == Relation.Vertical)
                {
                    editingVertex.prev.point = new Point(editingVertex.point.X, editingVertex.prev.point.Y);
                }
                if (editingVertex.next.next != null && editingVertex.next.nextRelation == Relation.Vertical && editingVertex.next.next.prevRelation == Relation.Vertical)
                {
                    editingVertex.next.next.point = new Point(editingVertex.next.point.X, editingVertex.next.next.point.Y);
                }

                if (editingVertex.prev.prev != null && editingVertex.prev.prev.prevRelation == Relation.Vertical && editingVertex.prev.prev.nextRelation == Relation.Vertical)
                {
                    editingVertex.prev.prev.point = new Point(editingVertex.prev.point.X, editingVertex.prev.prev.point.Y);
                }
            }
        }
        public static void MoveVertexByMouse(Vertex vertex, MouseEventArgs e, Point prevMousePosition, bool movePolygon = false)
        {
            int dx = e.Location.X - prevMousePosition.X;
            int dy = e.Location.Y - prevMousePosition.Y;

            // Aktualizacja pozycji bieżącego wierzchołka
            vertex.point = new Point(vertex.point.X + dx, vertex.point.Y + dy);
            
            if(!movePolygon)
            {
                if (vertex.next != null && vertex.nextRelation == Relation.Horizontal && vertex.next.prevRelation == Relation.Horizontal)
                {
                    vertex.next.point = new Point(vertex.next.point.X, vertex.point.Y);
                }
                if (vertex.prev != null && vertex.prevRelation == Relation.Horizontal && vertex.prev.nextRelation == Relation.Horizontal)
                {
                    vertex.prev.point = new Point(vertex.prev.point.X, vertex.point.Y);
                }
                if (vertex.next != null && vertex.nextRelation == Relation.Vertical && vertex.next.prevRelation == Relation.Vertical)
                {
                    vertex.next.point = new Point(vertex.point.X, vertex.next.point.Y);
                }
                if (vertex.prev != null && vertex.prevRelation == Relation.Vertical && vertex.prev.nextRelation == Relation.Vertical)
                {
                    vertex.prev.point = new Point(vertex.point.X, vertex.prev.point.Y);
                }
            }
        }

        public static Polygon OffsetPolygon(Polygon originalPolygon, float offsetDistance)
        {
            Polygon offsetPolygon = new Polygon(new List<Vertex>());

            for (int i = 0; i < originalPolygon.vertices.Count ; i++)
            {
                Vertex currentVertex = originalPolygon.vertices[i];
                Vertex prevVertex = originalPolygon.vertices[(i - 1 + originalPolygon.vertices.Count) % originalPolygon.vertices.Count];
                Vertex nextVertex = originalPolygon.vertices[(i + 1) % originalPolygon.vertices.Count];

                Vector2 edge = new Vector2(nextVertex.point.X - currentVertex.point.X, nextVertex.point.Y - currentVertex.point.Y);
                Vector2 prevEdge = new Vector2(currentVertex.point.X - prevVertex.point.X, currentVertex.point.Y - prevVertex.point.Y);

                Vector2 edgeNormal = new Vector2(-edge.Y, edge.X);
                Vector2 prevEdgeNormal = new Vector2(-prevEdge.Y, prevEdge.X);


                if (IsClockwiseOffset(originalPolygon))
                {
                    edgeNormal = Vector2.Normalize(edgeNormal);
                    prevEdgeNormal = Vector2.Normalize(prevEdgeNormal);
                }
                else
                {
                    edgeNormal = Vector2.Normalize(-edgeNormal);
                    prevEdgeNormal = Vector2.Normalize(-prevEdgeNormal);
                }


                Vector2 offsetVector = edgeNormal + prevEdgeNormal;
                offsetVector = Vector2.Normalize(offsetVector);

                Point offsetPoint = new Point(
                    (int)(currentVertex.point.X + offsetVector.X * offsetDistance),
                    (int)(currentVertex.point.Y + offsetVector.Y * offsetDistance));

                // Sprawdzamy, czy przesunięty wierzchołek jest częścią otoczki
                bool isConvexHullVertex = IsConvexHullVertex(currentVertex, originalPolygon.vertices);

                if (isConvexHullVertex)
                {
                    offsetPolygon.vertices.Add(new Vertex(offsetPoint, null, null));
                }
                else
                {
                    offsetPolygon.vertices.Add(currentVertex);
                }
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
        public static Polygon ResolveSelfIntersections(Polygon offsetPolygon)
        {
            bool intersectionsFound = true;

            while (intersectionsFound)
            {
                intersectionsFound = false;

                for (int i = 0; i < offsetPolygon.vertices.Count; i++)
                {
                    Vertex vertexA = offsetPolygon.vertices[i];
                    Vertex vertexB = offsetPolygon.vertices[(i + 1) % offsetPolygon.vertices.Count];

                    for (int j = i + 2; j < offsetPolygon.vertices.Count; j++)
                    {
                        Vertex vertexC = offsetPolygon.vertices[j];
                        Vertex vertexD = offsetPolygon.vertices[(j + 1) % offsetPolygon.vertices.Count];

                        if (DoLineSegmentsIntersect(vertexA.point, vertexB.point, vertexC.point, vertexD.point))
                        {
                            Point intersectionPoint = CalculateIntersectionPoint(vertexA.point, vertexB.point, vertexC.point, vertexD.point);
                            Vertex newVertex1 = new Vertex(intersectionPoint, vertexA, vertexC);
                            Vertex newVertex2 = new Vertex(intersectionPoint, vertexB, vertexD);

                            // Update next and prev references of vertices correctly
                            newVertex1.next = newVertex2;
                            newVertex2.prev = newVertex1;

                            if (i == 0)
                            {
                                vertexC.next = newVertex1;
                                vertexC.prev = vertexC;
                                newVertex1.next = vertexB;
                                vertexB.prev = newVertex1;
                                offsetPolygon.vertices.Insert(0, newVertex1);
                                offsetPolygon.vertices.Remove(vertexA);
                                offsetPolygon.vertices.Remove(vertexD);
                            }
                            else
                            {
                                vertexA.next = newVertex1;
                                vertexC.prev = newVertex1;
                                vertexB.next = newVertex2;
                                vertexD.prev = newVertex2;

                                offsetPolygon.vertices.RemoveRange(i + 1, j - i);
                                offsetPolygon.vertices.Insert(i, newVertex1);
                                offsetPolygon.vertices.Insert(i + 1, newVertex2);
                            }



                            intersectionsFound = true;
                        }
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
            // Calculate the slopes of the two line segments
            float m1 = (float)(p2.Y - p1.Y) / (p2.X - p1.X);
            float m2 = (float)(p4.Y - p3.Y) / (p4.X - p3.X);

            // Calculate the intersection point
            float x = (m1 * p1.X - m2 * p3.X + p3.Y - p1.Y) / (m1 - m2);
            float y = m1 * (x - p1.X) + p1.Y;

            return new Point((int)x, (int)y);
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
        public static bool IsClockwiseOffset(Polygon polygon)
        {
            int sum = 0;

            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                Vertex currentVertex = polygon.vertices[i];
                Vertex nextVertex = polygon.vertices[(i + 1) % polygon.vertices.Count];

                sum += (nextVertex.point.X - currentVertex.point.X) * (nextVertex.point.Y + currentVertex.point.Y);
            }

            return sum > 0;
        }
        public static List<Vertex> StartVertices1()
        {
            
            Vertex v1 = new Vertex(new Point(346, 135), null, null);
            Vertex v2 = new Vertex(new Point(428, 138), v1, null);
            v1.next = v2;
            Vertex v3 = new Vertex(new Point(488, 138), v2, null);
            v2.next = v3;
            v2.nextRelation = Relation.Horizontal;
            Vertex v4 = new Vertex(new Point(578, 190), v3, null);
            v3.next = v4;
            v3.prevRelation = Relation.Horizontal;
            Vertex v5 = new Vertex(new Point(578, 288), v4, null);
            v4.next = v5;
            v4.nextRelation = Relation.Vertical;
            Vertex v6 = new Vertex(new Point(550, 360), v5, null);
            v5.next = v6;
            v5.prevRelation = Relation.Vertical;
            Vertex v7 = new Vertex(new Point(513, 387), v6, null);
            v6.next = v7;
            Vertex v8 = new Vertex(new Point(463, 381), v7, null);
            v7.next = v8;
            Vertex v9 = new Vertex(new Point(465, 328), v8, null);
            v8.next = v9;
            Vertex v10 = new Vertex(new Point(468, 279), v9, null);
            v9.next = v10;
            Vertex v11 = new Vertex(new Point(455, 270), v10, null);
            v10.next = v11;
            Vertex v12 = new Vertex(new Point(440, 269), v11, null);
            v11.next = v12;
            Vertex v13 = new Vertex(new Point(426, 311), v12, null);
            v12.next = v13;
            Vertex v14 = new Vertex(new Point(419, 355), v13, null);
            v13.next = v14;
            Vertex v15 = new Vertex(new Point(357, 372), v14, null);
            v14.next = v15;
            Vertex v16 = new Vertex(new Point(333, 352), v15, null);
            v15.next = v16;
            Vertex v17 = new Vertex(new Point(330, 352), v16, null);
            v16.next = v17;
            Vertex v18 = new Vertex(new Point(349, 267), v17, null);
            v17.next = v18;
            Vertex v19 = new Vertex(new Point(318, 264), v18, null);
            v18.next = v19;
            Vertex v20 = new Vertex(new Point(306, 278), v19, null);
            v19.next = v20;
            Vertex v21 = new Vertex(new Point(287, 322), v20, null);
            v20.next = v21;
            Vertex v22 = new Vertex(new Point(267, 336), v21, null);
            v21.next = v22;
            Vertex v23 = new Vertex(new Point(148, 300), v22, null);
            v22.next = v23;
            Vertex v24 = new Vertex(new Point(217, 240), v23, null);
            v23.next = v24;
            Vertex v25 = new Vertex(new Point(177, 213), v24, null);
            v24.next = v25;
            v1.prev = v25;
            v25.next = v1;
            List<Vertex> list = new List<Vertex>
            {
                v1, v2, v3, v4, v5, v6, v7, v8,
                v9, v10, v11, v12, v13, v14, v15,
                v16, v17, v18, v19, v20, v21, v22,
                v23, v24, v25,
                
            };
            return list;
        }
        public static List<Vertex> StartVertices2()
        {
            Vertex v1 = new Vertex(new Point(84, 60), null, null);
            Vertex v2 = new Vertex(new Point(118, 43), v1, null);
            v1.next = v2;
            Vertex v3 = new Vertex(new Point(148, 60), v2, null);
            v2.next = v3;
            v3.nextRelation = Relation.Vertical;
            Vertex v4 = new Vertex(new Point(148, 110), v3, null);
            v3.next = v4;
            v4.prevRelation = Relation.Vertical;
            v4.nextRelation = Relation.Horizontal;
            Vertex v5 = new Vertex(new Point(40, 110), v4, null);
            v4.next = v5;
            v5.prevRelation = Relation.Horizontal;
            Vertex v6 = new Vertex(new Point(30, 30), v5, null);
            v5.next = v6;
            Vertex v7 = new Vertex(new Point(104, 90), v6, v1);
            v6.next = v7;
            v1.prev = v7;
            List<Vertex> list = new List<Vertex> 
            { 
                v1, v2, v3, v4, v5 ,v6 ,v7
            
            };


            return list;
        }
    }
}