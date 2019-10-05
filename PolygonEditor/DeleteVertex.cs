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
            if(vertex == polygon.start_point)
            {
                polygon.segments.RemoveAt(polygon.segments.Count - 1);
                polygon.segments.RemoveAt(0);
                polygon.apex.RemoveAt(0);

                polygon.start_point = polygon.apex.First();

                current_polygon = polygon;
                current_point = polygon.apex.Last();
                previous_point = null;

                return;
            }

            int segmentNumber = GetSegmentNumber(polygon, vertex);

            List<(Point, Point)> newSegments = new List<(Point, Point)>();
            List<Point> newApex = new List<Point>();

            for(int i=segmentNumber+1; i<polygon.segments.Count; i++)
            {
                newSegments.Add(polygon.segments[i]);
                newApex.Add(polygon.segments[i].p1);
            }

            for(int i=0; i<segmentNumber-1; i++)
            {
                newSegments.Add(polygon.segments[i]);
                newApex.Add(polygon.segments[i].p1);
            }

            newApex.Add(polygon.segments[segmentNumber - 1].p1);

            (Point, Point)? toDelete1;
            (Point, Point)? toDelete2;
            try
            {
                toDelete1 = polygon.segments[segmentNumber - 1];
            }
            catch(Exception e)
            {
                toDelete1 = null;
            }
            
            try
            {
                toDelete2 = polygon.segments[segmentNumber];
            }
            catch(Exception e)
            {
                toDelete2 = null;
            }
            

            polygon.segments = newSegments;
            polygon.apex = newApex;
            polygon.start_point = newApex.First();
            current_polygon = polygon;
            current_point = newApex.Last();
            previous_point = null;

            if(toDelete1 != null) DeleteSegment(polygon, ((Point,Point))toDelete1);
            if (toDelete2 != null) DeleteSegment(polygon, ((Point, Point))toDelete2);
        }

        private int GetSegmentNumber(Polygon polygon, Point vertex)
        {
            int i = 0;
            foreach(var segment in polygon.segments)
            {
                if(segment.p1 == vertex || segment.p2 == vertex)
                {
                    return i + 1;
                }
                i++;
            }
            return -1;
        }

        private void DeleteSegment(Polygon polygon, (Point p1, Point p2) segment)
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
