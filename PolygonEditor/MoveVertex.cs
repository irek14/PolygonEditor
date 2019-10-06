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
        Point vertex_to_move;
        private void MoveVertex(Point p)
        {
            graph.FillRectangle(Brushes.White, vertex_to_move.X - 2, vertex_to_move.Y - 2, 4, 4);

            for (int i=0; i<current_polygon.apex.Count; i++)
            {
                if(current_polygon.apex[i] == vertex_to_move)
                {
                    current_polygon.apex[i] = p;
                    break;
                }
            }

            List<(Point, Point)> toDelete = new List<(Point, Point)>();
            List<(Point, Point)> modifySegments = new List<(Point, Point)>();

            for (int i = 0; i < current_polygon.segments.Count; i++)
            {
                if (current_polygon.segments[i].p1 == vertex_to_move)
                {
                    toDelete.Add(current_polygon.segments[i]);
                    current_polygon.segments[i] = (p, current_polygon.segments[i].p2);
                    modifySegments.Add(current_polygon.segments[i]);
                }
                if(current_polygon.segments[i].p2 == vertex_to_move)
                {
                    toDelete.Add(current_polygon.segments[i]);
                    current_polygon.segments[i] = (current_polygon.segments[i].p1,p);
                    modifySegments.Add(current_polygon.segments[i]);
                }
            }

            vertex_to_move = p;

            foreach (var segment in toDelete)
                DeleteSegment(segment);

            foreach (var segment in modifySegments)
                BrenshamDrawLine(pen, segment.Item1, segment.Item2);
        }
    }
}
