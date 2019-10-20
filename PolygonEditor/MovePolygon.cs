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
        private Point start_move_point;

        private void MovePolygon(Point p)
        {
            int dX = start_move_point.X - p.X;
            int dY = start_move_point.Y - p.Y;

            start_move_point = p;

            List<Point> newApex = new List<Point>();
            List<(Point, Point)> newSegments = new List<(Point, Point)>();

            polygons.Remove(current_polygon);
            //PaintAllPoints(Brushes.White, current_polygon);
            foreach (var segment in current_polygon.segments)
            {
                //DeleteSegment(segment);
                (Point, Point) newSegment = (new Point(segment.p1.X - dX, segment.p1.Y - dY), new Point(segment.p2.X - dX, segment.p2.Y - dY));
                newSegments.Add(newSegment);
            }

            foreach(var apex in current_polygon.apex)
            {
                Point point = new Point(apex.X - dX, apex.Y - dY);
                newApex.Add(point);
            }

            current_polygon.relations = GetRelationAfterMovePolygon(current_polygon.relations, current_polygon.segments, newSegments);
            current_polygon.segments = newSegments;
            current_polygon.apex = newApex;

            //foreach(var segment in current_polygon.segments)
            //{
            //    BrenshamDrawLine(pen, segment.p1, segment.p2);
            //}

            polygons.Add(current_polygon);
        }

        private List<Relation> GetRelationAfterMovePolygon(List<Relation> relation, List<(Point p1, Point p2)> old_segment, List<(Point p1, Point p2)> new_segment)
        {
            for (int i = 0; i < relation.Count; i++)
            {
                int index = old_segment.IndexOf(relation[i].first_segment);
                if (index != -1)
                    relation[i].first_segment = new_segment[index];

                index = old_segment.IndexOf(relation[i].second_segment);
                if (index != -1)
                    relation[i].second_segment = new_segment[index];
            }
            return relation;
        }
    }
}
