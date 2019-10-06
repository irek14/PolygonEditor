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
        private (Polygon,Point) GetPolygonWithVertex(Point p)
        {
            foreach (var polygon in polygons)
            {
                foreach (var vertex in polygon.apex)
                {
                    if (CheckIfVertex(p, vertex))
                    {
                        return (polygon,vertex);
                    }
                }
            }
            return (null,new Point(-1,-1));
        }

        private bool CheckIfVertex(Point p, Point vertex)
        {
            if (Math.Abs(p.X - vertex.X) <= 5 && Math.Abs(p.Y - vertex.Y) <= 5)
                return true;

            return false;
        }

        private void DeleteVertex(Polygon polygon, Point vertex)
        {
            List<(Point, Point)> toDelete = new List<(Point, Point)>();

            foreach(var segment in polygon.segments)
            {
                if(segment.p1 == vertex || segment.p2 == vertex)
                {
                    toDelete.Add(segment);
                }
            }

            foreach(var segmentToDelete in toDelete)
            {
                polygon.segments.Remove(segmentToDelete);
                DeleteSegment(segmentToDelete);
            }

            for (int i = 0; i < polygon.segments.Count; i++)
            {
                if (polygon.segments[i].p2 != polygon.segments[(i + 1) % polygon.segments.Count].p1)
                {
                    CorrectSegmentAndApex(ref polygon, i);
                    break;
                }
            }

            graph.FillRectangle(Brushes.White, vertex.X - 2, vertex.Y - 2, 4, 4);
        }

        private void DeleteSegment((Point p1, Point p2) segment)
        {
            BrenshamDrawLine(new Pen(Color.White), segment.p1, segment.p2);
            foreach (var pol in polygons)
            {
                foreach (var s in pol.segments)
                {
                    if (CheckSegmentIntersection(segment.p1, segment.p2, s.p1, s.p2))
                        BrenshamDrawLine(pen, s.p1, s.p2);
                }
            }
        }
    }
}
