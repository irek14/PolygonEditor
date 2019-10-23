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
            graph = e.Graphics;

            PaintAll();
        }

        private void CreateSegment(Point next_point)
        {
            if (current_point == next_point) return;

            if (CheckIfStartPoint(next_point))
                next_point = current_polygon.start_point;              

            current_polygon.segments.Add(((Point)current_point, next_point));
            current_polygon.apex.Add(next_point);

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
            previous_point = new Point(e.Location.X, e.Location.Y);
        }



        private bool CheckIfStartPoint(Point p1)
        {
            if (Math.Abs(current_polygon.start_point.X - p1.X) <= 5 && Math.Abs(current_polygon.start_point.Y - p1.Y) <= 5)
                return true;

            return false;
        }
    }
}
