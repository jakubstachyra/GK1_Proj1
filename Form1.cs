namespace GK_Proj1
{
    public partial class Form1 : Form
    {
        private Mode mode;
        private Bitmap tempBitmap;
        private List<Polygon> polygons = new List<Polygon>();
        private List<Vertex> currPoints = new List<Vertex>();
        private int eps = 5;
        private int offset = 5;
        private int vertexSize = 10;
        private bool isDrawingLine = false;
        private bool isEditing = false;
        private bool isEditingLine = false;
        private bool isMovingPolygon = false;
        private Polygon movingPolygon = null;
        private Vertex? prevPoint = null;
        private Vertex? editingVertex = null;
        private Point prevMousePosition;
        
        private Vertex PolygonStart;
        private Point lineStartPoint;
        private Point lineEndPoint;
        public Form1()
        {
            InitializeComponent();
            tempBitmap = new Bitmap(3840, 2160);
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                g.Clear(Color.Transparent); // Inicjalizacja t³a bitmapy
            }
        }

        private void bitMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                switch(mode)
                {
                    case Mode.Draw:
                        if (!isDrawingLine)
                        {
                            // Klikniêcie lewym przyciskiem myszy - dodaj prostok¹t
                            var point = new Point(e.X - offset, e.Y - offset);
                            PolygonStart = new Vertex(point, prevPoint, null);
                            prevPoint = PolygonStart;
                            currPoints.Add(PolygonStart);
                            lineStartPoint = PolygonStart.point;
                            isDrawingLine = true;
                        }
                        else
                        {
                            // Klikniêcie lewym przyciskiem myszy po rozpoczêciu rysowania linii - dodaj wierzcho³ek linii

                            foreach (var rect in currPoints)
                            {
                                if (Math.Abs(e.Location.X - PolygonStart.point.X) <= eps && Math.Abs(e.Location.Y - PolygonStart.point.Y) <= eps)
                                {
                                    PolygonStart.prev = prevPoint;
                                    if (prevPoint != null)
                                    {
                                        prevPoint.next = PolygonStart;
                                    }
                                    CreatePolygon(e);
                                }


                                if (Math.Abs(e.Location.X - rect.point.X) <= eps && Math.Abs(e.Location.Y - rect.point.Y) <= eps)
                                {
                                    return;
                                }
                            }

                            lineEndPoint = e.Location;
                            Point point = new Point(e.X - offset, e.Y - offset);
                            Vertex newPoint = new Vertex(point, prevPoint, null);
                            if (prevPoint != null)
                            {
                                prevPoint.next = newPoint;
                            }
                            prevPoint = newPoint;
                            currPoints.Add(newPoint);
                            lineStartPoint = point;
                        }
                     break;
                     case Mode.Delete:
                            foreach (var polygon in polygons)
                            {
                                foreach (var vertex in polygon.vertices)
                                {

                                    if (Math.Abs(e.Location.X - vertex.point.X) <= 2 * eps && Math.Abs(e.Location.Y - vertex.point.Y) <= 2 * eps)
                                    {
                                        if (vertex.prev != null && vertex.next != null)
                                        {
                                            vertex.prev.next = vertex.next;
                                            vertex.next.prev = vertex.prev;
                                        }
                                        if (polygon.vertices.Count > 3)
                                        {
                                            polygon.vertices.Remove(vertex);
                                        }
                                        else
                                        {
                                            polygons.Remove(polygon);
                                        }
                                        bitMap.Invalidate();
                                        return;
                                    }
                                }
                            }
                    break;
                    
                }
  
                bitMap.Invalidate(); // Odœwie¿enie obszaru rysowania
            }
        }
        private void CreatePolygon(MouseEventArgs e)
        {
            Polygon newPolygon = new Polygon(currPoints);
            currPoints.Last().next = PolygonStart;

            isDrawingLine = false;
            polygons.Add(newPolygon);
            currPoints.Clear();
        }

        private void bitMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingLine)
            {
                // Aktualizuj pozycjê koñcow¹ linii podczas przesuwania myszki
                lineEndPoint = e.Location;
            }
            if (isEditing)
            {
                if (editingVertex != null)
                {
                    Functions.MoveVertexByMouse(editingVertex, e, prevMousePosition);
                    
                }
            }
            if(isEditingLine)
            {
                Functions.MoveLineByMouse(editingVertex, e, prevMousePosition);
            }
            if(isMovingPolygon)
            { 
                Functions.MovePolygonByMouse(movingPolygon, e, prevMousePosition);
            }
            
            prevMousePosition = e.Location;
            bitMap.Invalidate(); // Odœwie¿enie obszaru rysowania
        }
        
        private void bitMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(tempBitmap, 0, 0);
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                g.Clear(Color.Transparent); // Inicjalizacja t³a bitmapy
            }
            Functions.DrawPolygon(e, polygons, vertexSize, offset);

            foreach (var vertex in currPoints)
            {
                e.Graphics.FillEllipse(Brushes.Black, vertex.point.X - offset, vertex.point.Y - offset, vertexSize, vertexSize);
                if (vertex.next != null)
                {
                    e.Graphics.DrawLine(Pens.Black, vertex.point, vertex.next.point);
                }

            }
            if (isDrawingLine)
            {
                // Rysuj liniê od  prostok¹ta do pozycji myszki
                e.Graphics.DrawLine(Pens.Black, lineStartPoint, lineEndPoint);

            }
            bitMap.Invalidate();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bitMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mode == Mode.Edit)
            {
                foreach (var polygon in polygons)
                {
                    foreach (var vertex in polygon.vertices)
                    {
                        if (vertex.next != null && vertex.prev != null)
                        {
                            if (Functions.isPointOnLine(vertex.point, vertex.next.point, e.Location, eps))
                            {
                                var newVertex = new Vertex(Functions.FindCenterOfLine(vertex.point, vertex.next.point), vertex, vertex.next);
                                vertex.next.prev = newVertex;
                                vertex.next = newVertex;

                                polygon.vertices.Insert(polygon.vertices.IndexOf(vertex) + 1, newVertex);

                                return;
                            }
                        }

                    }
                }
            }
        }
        
        private void bitMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (mode)
                {
                    case Mode.Edit:
                    foreach (var polygon in polygons)
                    {
                        foreach (var vertex in polygon.vertices)
                        {
                            if (Functions.CalculateDistance(vertex.point,e.Location) <= 2 * eps)
                            {
                                isEditing = true;
                                editingVertex = vertex;
                                return;
                            }    
                            
                            if (vertex.next != null)
                            {
                                if (Functions.isPointOnLine(vertex.point, vertex.next.point, e.Location, eps) &&
                                        Functions.CalculateDistance(vertex.point, e.Location) > eps &&
                                        Functions.CalculateDistance(vertex.next.point, e.Location) > eps)
                                {
                                    isEditingLine = true;
                                    editingVertex = vertex;
                                    prevMousePosition = e.Location;
                                    return;
                                }
                            }
                            }
                    }
                    break;
                    case Mode.Move:
                            foreach (var polygon in polygons)
                            {
                                if (Functions.IsPointInsidePolygon(polygon, e))
                                {
                                    movingPolygon = polygon;
                                    isMovingPolygon = true;
                                    return;
                                }
                            }
                        break;
                }
            }
        }

        private void bitMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEditing = false;
                isEditingLine = false;
                isMovingPolygon = false;
            }
        }
        private void DrawButton_MouseClick(object sender, MouseEventArgs e)
        {
            mode = Mode.Draw;
        }

        private void EditButton_MouseClick(object sender, MouseEventArgs e)
        {
            mode = Mode.Edit;
        }

        private void DeleteButton_MouseClick(object sender, MouseEventArgs e)
        {
            mode = Mode.Delete;
        }

        private void MoveButton_MouseClick(object sender, MouseEventArgs e)
        {
            mode = Mode.Move;
        }
    }
    enum Mode
    {
        Draw,
        Edit,
        Delete,
        Move,
    }
}