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
        private (Polygon, (Point,Point)) GetPolygonWithPointOnSegment(Point p)
        {
            foreach(var polygon in polygons)
            {
                foreach(var segment in polygon.segments)
                {
                    if (GetDistanceFromLine(segment, p) <= 3)
                        return (polygon,segment);
                }
            }
            return (null, (new Point(), new Point()));
        }

        private void DeletePolygon(Polygon polygon)
        {
            polygons.Remove(polygon);
            PaintAllPoints(Brushes.White, polygon);

            foreach(var segment in polygon.segments)
            {
                DeleteSegment(segment);
            }
        }

        private double GetDistanceFromLine((Point a, Point b) segment, Point p)
        {
            return Math.Abs((segment.b.X - segment.a.X) * (segment.a.Y - p.Y) - (segment.a.X - p.X) * (segment.b.Y - segment.a.Y)) /
                    Math.Sqrt(Math.Pow(segment.b.X - segment.a.X, 2) + Math.Pow(segment.b.Y - segment.a.Y, 2));
        }
    }
}
