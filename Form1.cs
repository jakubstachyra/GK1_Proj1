namespace GK_Proj1
{
    public partial class Form : System.Windows.Forms.Form
    {
        // our collection of strokes for drawing
        private List<Point> _points = new List<Point>();
        private List<Point> _currStroke = new List<Point>();
        private Point _startPoint;
        private Point _endPoint;
        private bool _isDrawing = false;
        public Form()
        {
            InitializeComponent();
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
         
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        { }
    }
}