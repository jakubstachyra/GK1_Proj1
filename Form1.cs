namespace GK_Proj1
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Bitmap tempBitmap;
        private List<Polygon> polygons = new List<Polygon>();
        private List<Tuple<Point,Rectangle>> currPoints = new List<Tuple<Point, Rectangle>>();
        private List<Line> currLines = new List<Line>();
        private bool isDrawingLine = false;
        private Point PolygonStart;
        private Point lineStartPoint;
        private Point lineEndPoint;
        public Form()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);
            this.Paint += bitMap_Paint;
            this.MouseClick += bitMap_MouseClick;
            this.MouseMove += bitMap_MouseMove;
            this.BackColor = Color.White;
            tempBitmap = new Bitmap(800, 600);
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                g.Clear(Color.Transparent); // Inicjalizacja t³a bitmapy
            }
        }

        private void bitMap_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void bitMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!isDrawingLine)
                {
                    // Klikniêcie lewym przyciskiem myszy - dodaj prostok¹t
                    if (currPoints.Count == 0)
                    {
                        PolygonStart = e.Location;
                    }
                    Point point = new Point(e.X, e.Y);
                    lineStartPoint = point;
                    Rectangle newRect = new Rectangle(e.X - 3, e.Y - 3, 8, 8);
                    Tuple<Point, Rectangle> newPoint = new Tuple<Point, Rectangle>(point, newRect);
                    currPoints.Add(newPoint);
                    isDrawingLine = true;
                }
                else
                {
                    // Klikniêcie lewym przyciskiem myszy po rozpoczêciu rysowania linii - dodaj wierzcho³ek linii
                    lineEndPoint = e.Location;
                    Rectangle newRect = new Rectangle(e.X - 3, e.Y - 3, 8, 8);
                    Point point = new Point(e.X, e.Y);
                    Tuple<Point, Rectangle> newPoint = new Tuple<Point, Rectangle>(point, newRect);
                    currPoints.Add(newPoint);
                    Line newLine = new Line(lineStartPoint, lineEndPoint);
                    currLines.Add(newLine);
                    lineStartPoint = point;

                    if (e.Location.X - PolygonStart.X <= 7 && e.Location.Y - PolygonStart.Y <= 7)
                    {
                        Polygon newPolygon = new Polygon(currPoints, currLines);

                        // Rysuj nowy poligon na tempBitmap
                        using (Graphics g = Graphics.FromImage(tempBitmap))
                        {
                            foreach (var rect in currPoints)
                            {
                                g.FillRectangle(Brushes.Black, rect.Item2);
                            }
                            foreach (var line in currLines)
                            {
                                g.DrawLine(Pens.Black, line.start, line.end);
                            }
                        }

                        isDrawingLine = false;
                        polygons.Add(newPolygon);
                        currPoints.Clear();
                        currLines.Clear();
                        lineStartPoint = new Point(0, 0);

                    }
                    // Co jesli naciskam na juz istniejay wierzcholek w obecnym wielokacie
                }

                this.Invalidate(); // Odœwie¿enie obszaru rysowania
            }
        }
           
        

        private void bitMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(tempBitmap, 0, 0);
            
            foreach (var rect in currPoints)
            {
                e.Graphics.FillRectangle(Brushes.Black, rect.Item2);
            }
            foreach(var line in currLines)
            {
                e.Graphics.DrawLine(Pens.Black, line.start, line.end);
            }
            if (isDrawingLine)
            {
                // Rysuj liniê od  prostok¹ta do pozycji myszki
                e.Graphics.DrawLine(Pens.Black, lineStartPoint, lineEndPoint);
                
            }
            this.Invalidate();
        }

        private void bitMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingLine)
            {
                // Aktualizuj pozycjê koñcow¹ linii podczas przesuwania myszki
               lineEndPoint = e.Location;
               this.Invalidate(); // Odœwie¿enie obszaru rysowania

            }
        }

        private void bitMap_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
    }
}