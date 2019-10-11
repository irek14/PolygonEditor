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
            graph = Canvas.CreateGraphics();

            PaintAll();
        }

        private void BrenshamDrawLine(Pen pen, Point p1, Point p2)
        {
            //TODO: Brensham algorithm

            graph.DrawLine(pen, p1, p2);

            //int dx = Math.Abs(p2.X - p1.X), sx = p1.X < p2.X ? 1 : -1;
            //int dy = Math.Abs(p2.Y - p1.Y), sy = p1.Y < p2.Y ? 1 : -1;
            //int err = (dx > dy ? dx : -dy) / 2, e2;
            //for (; ; )
            //{
            //    graph.FillRectangle(pen.Brush, p1.X, p1.Y, 1, 1);
            //    if (p1.X == p2.X && p1.Y == p2.Y) break;
            //    e2 = err;
            //    if (e2 > -dx) { err -= dy; p1.X += sx; }
            //    if (e2 < dy) { err += dx; p1.Y += sy; }
            //}
        }

        private void CreateSegment(Point next_point)
        {
            if (current_point == next_point) return;

            if (CheckIfStartPoint(next_point))
            {
                BrenshamDrawLine(new Pen(Color.White), (Point)current_point, (Point)next_point);
                next_point = current_polygon.start_point;
            }
                

            current_polygon.segments.Add(((Point)current_point, next_point));
            current_polygon.apex.Add(next_point);

            //BrenshamDrawLine(pen, (Point)current_point, next_point);
            previous_point = null;

            current_point = next_point;

            if (next_point == current_polygon.start_point)
            {
                bool end = true;
                for(int i =0; i<current_polygon.segments.Count; i++)
                {
                    if(current_polygon.segments[i].p2 != current_polygon.segments[(i+1)% current_polygon.segments.Count].p1)
                    {
                        end = false;
                        CorrectSegmentAndApex(ref current_polygon, i);
                        break;
                    }
                }
                
                if(end)
                {
                    current_polygon.apex.RemoveAt(current_polygon.apex.Count - 1);
                    ResetVariables();
                }
            }
        }

        private void CorrectSegmentAndApex(ref Polygon polygon,int index)
        {
            List<(Point, Point)> newSegments = new List<(Point, Point)>();
            List<Point> newApex = new List<Point>();

            for (int i = index + 1; i < polygon.segments.Count; i++)
            {
                newSegments.Add(polygon.segments[i]);
                newApex.Add(polygon.segments[i].p1);
            }

            for (int i = 0; i <= index; i++)
            {
                newSegments.Add(polygon.segments[i]);
                newApex.Add(polygon.segments[i].p1);
            }

            newApex.Add(polygon.segments[index].p2);

            polygon.segments = newSegments;
            polygon.apex = newApex;
            polygon.start_point = polygon.apex.First();
            current_point = polygon.apex.Last();
        }

        private void CreateLine(MouseEventArgs e)
        {
            if (previous_point != null)
            {
                BrenshamDrawLine(new Pen(Color.White), (Point)current_point, (Point)previous_point);

                //PaintInstersectSegments((Point)current_point, (Point)previous_point);
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
            if (Math.Abs(current_polygon.start_point.X - p1.X) <= 5 && Math.Abs(current_polygon.start_point.Y - p1.Y) <= 5)
                return true;

            return false;
        }
    }
}
