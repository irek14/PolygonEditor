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
        private void AddVertex(Polygon polygon, (Point p1, Point p2) segment, Point newPoint)
        {
            DeleteRelation(polygon, segment);

            List<Point> newApex = new List<Point>();
            List<(Point, Point)> newSegments = new List<(Point, Point)>();
            polygons.Remove(polygon);

            for(int i=0; i<polygon.segments.Count; i++)
            {
                if(polygon.segments[i].p1 == segment.p1 && polygon.segments[i].p2 == segment.p2)
                {
                    (Point,Point) newSegment1 = (polygon.segments[i].p1, newPoint);
                    (Point, Point) newSegment2 = (newPoint, polygon.segments[i].p2);
                    newSegments.Add(newSegment1);
                    newSegments.Add(newSegment2);
                    DeleteSegment(polygon.segments[i]);
                    //BrenshamDrawLine(pen, newSegment1.Item1, newSegment1.Item2);
                    //BrenshamDrawLine(pen, newSegment2.Item1, newSegment2.Item2);

                    newApex.Add(polygon.segments[i].p1);
                    newApex.Add(newPoint);
                }
                else
                {
                    newSegments.Add(polygon.segments[i]);
                    newApex.Add(polygon.segments[i].p1);
                }
            }
            polygon.apex = newApex;
            polygon.segments = newSegments;

            polygons.Add(polygon);
        }
    }
}
