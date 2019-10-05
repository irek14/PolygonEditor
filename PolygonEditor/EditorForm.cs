using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PolygonEditor
{
    public partial class EditorForm : Form
    {
        enum Mode { None, Draw };
        public EditorForm()
        {
            InitializeComponent();
        }

        Pen pen = new Pen(Color.Black);
        Point? current_point = null;
        Point? previous_point = null;
        List<Point> apex = new List<Point>();
        List<(Point p1, Point p2)> segments = new List<(Point, Point)>();
        Mode current_mode = Mode.None;
        Graphics graph;

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            graph = Canvas.CreateGraphics();
            //graph.DrawEllipse(Pens.Red, 0, 0, 25, 25);
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if(current_mode == Mode.None)
            {
                current_mode = Mode.Draw;
                current_point = new Point(e.Location.X, e.Location.Y);
                apex.Add((Point)current_point);
            }
            else if(current_mode == Mode.Draw)
            {
                Point next_point = new Point(e.Location.X, e.Location.Y);
                segments.Add(((Point)current_point, next_point));
                apex.Add(next_point);

                graph.DrawLine(pen, (Point)current_point, next_point);
                previous_point = null;

                current_point = next_point;            
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (current_mode == Mode.Draw && e.Button == MouseButtons.Left)
            {
                if(previous_point != null)
                {
                    graph.DrawLine(new Pen(Color.White), (Point)current_point, (Point)previous_point);
                    foreach(var segment in segments)
                    {
                        if(CheckSegmentIntersection((Point)current_point, (Point)previous_point, segment.p1, segment.p2))
                            graph.DrawLine(pen, segment.p1, segment.p2);
                    }
                }
                previous_point = new Point(e.Location.X, e.Location.Y);

                graph.DrawLine(pen, (Point)current_point, (Point)previous_point);
            }
        }

        private bool CheckSegmentIntersection(Point p1, Point p2, Point p3, Point p4)
        {
            int d1 = (p4.X - p3.X) * (p1.Y - p3.Y) - (p1.X - p3.X) * (p4.Y - p3.Y);
            int d2 = (p4.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p4.Y - p3.Y);
            int d3 = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            int d4 = (p2.X - p1.X) * (p4.Y - p1.Y) - (p4.X - p1.X) * (p2.Y - p1.Y);

            int d12 = d1 * d2;
            int d34 = d3 * d4;

            if (d12 > 0 || d34 > 0) return false;

            if (d12 < 0 || d34 < 0) return true;

            return OnRectangle(p1,p3,p4) || OnRectangle(p2,p3,p4) || OnRectangle(p3,p1,p2) || OnRectangle(p4,p1,p2);
        }

        private bool OnRectangle(Point q, Point p1, Point p2)
        {
            return Math.Min(p1.X, p2.X) <= q.X && q.X <= Math.Max(p1.X, p2.X) && Math.Min(p1.Y, p2.Y) <= q.Y && q.Y <= Math.Max(p1.Y, p2.Y);
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            current_point = null;
            previous_point = null;
            apex = new List<Point>();
            segments = new List<(Point, Point)>();
            current_mode = Mode.None;
            Canvas.Invalidate();
        }
    }
}
