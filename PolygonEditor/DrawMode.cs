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
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            graph = Canvas.CreateGraphics();
        }

        private void BrenshamDrawLine(Pen pen, Point p1, Point p2)
        {
            //TODO: Brensham algorithm

            graph.DrawLine(pen, p1, p2);
        }

        private void CreateSegment(Point next_point)
        {
            if (current_point == next_point) return;

            if (CheckIfStartPoint(next_point))
                next_point = current_polygon.start_point;

            current_polygon.segments.Add(((Point)current_point, next_point));
            current_polygon.apex.Add(next_point);

            BrenshamDrawLine(pen, (Point)current_point, next_point);
            previous_point = null;

            current_point = next_point;

            if (next_point == current_polygon.start_point)
                ResetVariables();
        }

        private void CreateLine(MouseEventArgs e)
        {
            if (previous_point != null)
            {
                BrenshamDrawLine(new Pen(Color.White), (Point)current_point, (Point)previous_point);

                PaintInstersectSegments((Point)current_point, (Point) previous_point);
            }
            previous_point = new Point(e.Location.X, e.Location.Y);

            BrenshamDrawLine(pen, (Point)current_point, (Point)previous_point);
        }

        private void PaintInstersectSegments(Point p1, Point p2)
        {
            foreach (var polygon in polygons)
            {
                foreach (var segment in polygon.segments)
                {
                    if (CheckSegmentIntersection(p1, p2, segment.p1, segment.p2))
                        BrenshamDrawLine(pen, segment.p1, segment.p2);
                }
            }
        }


        private bool CheckIfStartPoint(Point p1)
        {
            if (Math.Abs(current_polygon.start_point.X - p1.X) <= 10 && Math.Abs(current_polygon.start_point.Y - p1.Y) <= 10)
                return true;

            return false;
        }
    }
}
