namespace GK_Proj1
{
    public partial class Form1 : Form
    {
        static Image imageVertical = Image.FromFile("D:\\Kuba\\GK_Proj1\\GK_Proj1\\Images\\vertical.png");
        static Image imageHorizontal = Image.FromFile("D:\\Kuba\\GK_Proj1\\GK_Proj1\\Images\\horizontal.png");
        
        private Mode mode;
        private Bitmap tempBitmap;
        private List<Polygon> polygons = new List<Polygon>();
        private List<Vertex> currPoints = new List<Vertex>();

        private int eps = 5;
        private int mouseOffset = 5;
        private int vertexSize = 10;
        private int offset = 50;
        private bool isDrawingLine = false;
        private bool isEditingVertex = false;
        private bool isEditingLine = false;
        private bool isMovingPolygon = false;
        private bool isHorizontal = false;
        private bool isVertical = false;
        private bool isDeletingRel = false;
        private bool isOffset = false;

        private Polygon? movingPolygon = null;
        private Vertex? prevPoint = null;
        private Vertex? editingVertex = null;
        private Vertex PolygonStart;

        private Point prevMousePosition;
        private Point lineStartPoint;
        private Point lineEndPoint;

        public Form1()
        {
            InitializeComponent();
            tempBitmap = new Bitmap(3840, 2160);
            using  (Graphics g = Graphics.FromImage(tempBitmap))
            {
                g.Clear(Color.Transparent); // Inicjalizacja t³a bitmapy
            }
        }

        private void bitMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                switch (mode)
                {
                    case Mode.Draw:
                        if (!isDrawingLine)
                        {
                            // Klikniêcie lewym przyciskiem myszy - dodaj prostok¹t
                            var point = new Point(e.X - mouseOffset, e.Y - mouseOffset);
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
                            Point point = new Point(e.X - mouseOffset, e.Y - mouseOffset);
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
                    case Mode.Edit:
                        if (isHorizontal || isVertical || isDeletingRel)
                        {
                            foreach (var polygon in polygons)
                            {
                                foreach (var vertex in polygon.vertices)
                                {
                                    if (vertex.next != null && vertex.prev != null)
                                    {
                                        if (Functions.isPointOnLine(vertex.point, vertex.next.point, e.Location, 2 * eps))
                                        {
                                            if (isHorizontal && vertex.next.nextRelation != Relation.Horizontal && vertex.prevRelation != Relation.Horizontal && vertex.nextRelation
                                                != Relation.Horizontal)
                                            {
                                                vertex.nextRelation = Relation.Horizontal;
                                                vertex.next.prevRelation = Relation.Horizontal;
                                                vertex.next.point = new Point(vertex.next.point.X, vertex.point.Y);
                                                var center = Functions.FindCenterOfLine(vertex.point, vertex.next.point);
                                            }
                                            else if (isVertical && vertex.next.prevRelation != Relation.Vertical && vertex.prevRelation != Relation.Vertical)
                                            {
                                                vertex.nextRelation = Relation.Vertical;
                                                vertex.next.prevRelation = Relation.Vertical;
                                                vertex.next.point = new Point(vertex.point.X, vertex.next.point.Y);
                                                var center = Functions.FindCenterOfLine(vertex.point, vertex.next.point);

                                            }
                                            else if(isDeletingRel)
                                            {
                                                vertex.nextRelation = Relation.None;
                                                vertex.next.prevRelation = Relation.None;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Mode.Delete:
                        foreach (var polygon in polygons)
                        {
                            if (Functions.IsPointInsidePolygon(polygon, e))
                            {
                                polygons.Remove(polygon);
                                break;

                            }
                        }
                        break;

                }

            }
            if (e.Button == MouseButtons.Right)
            {
                switch (mode)
                {
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
                                        vertex.prev.nextRelation = Relation.None;
                                        vertex.next.prevRelation = Relation.None;
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
                    case Mode.Edit:
                        foreach (var polygon in polygons)
                        {
                            foreach (var vertex in polygon.vertices)
                            {
                                if (vertex.next != null)
                                {
                                    if (Functions.isPointOnLine(vertex.point, vertex.next.point, e.Location, 2 * eps))
                                    {
                                        
                                    }
                                }
                            }
                        }
                        break;
                }

            }
            bitMap.Invalidate(); // Odœwie¿enie obszaru rysowania
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
                lineEndPoint = e.Location;
            }
            if (isEditingVertex && editingVertex != null)
            {
                Functions.MoveVertexByMouse(editingVertex, e, prevMousePosition);
            }
            if (isEditingLine)
            {
                Functions.MoveLineByMouse(editingVertex, e, prevMousePosition);
            }
            if (isMovingPolygon)
            {
                Functions.MovePolygonByMouse(movingPolygon, e, prevMousePosition);
            }

            prevMousePosition = e.Location;
            bitMap.Invalidate();
        }

        private void bitMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(tempBitmap, 0, 0);
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                g.Clear(Color.Transparent); // Inicjalizacja t³a bitmapy
            }
            Functions.DrawPolygon(e, polygons, vertexSize, mouseOffset, isOffset);
           if(isOffset)
            {
                foreach (var polygon in polygons)
                {
                    Polygon PolygonOffset = Functions.OffsetPolygon(polygon, offset);
                        foreach (var vertex in PolygonOffset.vertices)
                        {
                            e.Graphics.FillEllipse(Brushes.Black, vertex.point.X - mouseOffset, vertex.point.Y - mouseOffset, vertexSize, vertexSize);
                            if (vertex.next != null)
                            {
                                e.Graphics.DrawLine(Pens.Red, vertex.point, vertex.next.point);
                            bitMap.Invalidate();
                            }

                        }
                   
                }
            }

            foreach (var vertex in currPoints)
            {
                e.Graphics.FillEllipse(Brushes.Black, vertex.point.X - mouseOffset, vertex.point.Y - mouseOffset, vertexSize, vertexSize);
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
                                vertex.nextRelation = Relation.None;
                                vertex.prevRelation = Relation.None;
                                vertex.next.prevRelation = Relation.None;
                                vertex.prev.nextRelation = Relation.None;
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
                                if (Functions.CalculateDistance(vertex.point, e.Location) <= 2 * eps)
                                {
                                    isEditingVertex = true;
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
                isEditingVertex = false;
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
            DrawButton.Checked = false;
            MoveButton.Checked = false;
            DeleteButton.Checked = false;
            EditButton.Checked = true;
            isDrawingLine = false;
            currPoints.Clear();
            mode = Mode.Edit;
        }

        private void DeleteButton_MouseClick(object sender, MouseEventArgs e)
        {
            isDrawingLine = false;
            currPoints.Clear();
            mode = Mode.Delete;
        }

        private void MoveButton_MouseClick(object sender, MouseEventArgs e)
        {
            isDrawingLine = false;
            currPoints.Clear();
            mode = Mode.Move;
        }

        private void OffsetBar_Scroll(object sender, EventArgs e)
        {
            offset = OffsetBar.Value;
            // Tutaj mo¿esz zaktualizowaæ wartoœæ offsetu w swoim programie
            // Mo¿esz tak¿e wyœwietlaæ tê wartoœæ w jakimœ polu tekstowym lub etykiecie, jeœli jest to konieczne.
            OffsetBar.Text = "Offset: " + offset.ToString();
        }
        private void OffsetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isOffset = !isOffset;
        }
        private void VerticalCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            EditButton_MouseClick(sender, e);
            isVertical = VerticalCheckBox.Checked;
            isHorizontal = false;
            isDeletingRel = false;
            DeleteRelationCheckBox.Checked = false;
            HorizontalCheckBox.Checked = false;
        }
        private void HorizontalCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            EditButton_MouseClick(sender, e);
            isHorizontal = HorizontalCheckBox.Checked;
            isVertical = false;
            isDeletingRel = false;
            DeleteRelationCheckBox.Checked = false;
            VerticalCheckBox.Checked = false;
        }
        enum Mode
        {
            Draw,
            Edit,
            Delete,
            Move,
        }

        private void DeleteRelationCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            EditButton_MouseClick(sender, e);
            isDeletingRel = DeleteRelationCheckBox.Checked;
            HorizontalCheckBox.Checked = false;
            VerticalCheckBox.Checked = false;
            isHorizontal = false;
            isVertical = false;
            
        }
    }
}