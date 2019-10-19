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
        (Point p1, Point p2) first_to_relation = (new Point(-1, -1), new Point(-1, -1));
        Polygon polygonToRelation;

        private void AddRelation(Polygon polygon, (Point p1, Point p2) segment)
        {
            if (polygon.relations.Any(x => (x.first_segment.p1 == segment.p1 && x.first_segment.p2 == segment.p2) || (x.second_segment.p1 == segment.p1 && x.second_segment.p2 == segment.p2)))
            {
                MessageBox.Show("This segment already has a relation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (first_to_relation.p1 == new Point(-1, -1))
            {
                first_to_relation = segment;
                polygonToRelation = polygon;
                return;
            }

            if (!polygonToRelation.segments.Contains(segment))
            {
                MessageBox.Show("You cannot assign relation between two diffrent polygons", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                first_to_relation = (new Point(-1, -1), new Point(-1, -1));
                polygonToRelation = null;
                return;
            }

            if (first_to_relation.p1 == segment.p1 && first_to_relation.p2 == segment.p2)
            {
                MessageBox.Show("You cannot assign relation between the same segment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                first_to_relation = (new Point(-1, -1), new Point(-1, -1));
                polygonToRelation = null;
                return;
            }

            RelationTypes type = current_mode == Mode.AddSameLengthRelation ? RelationTypes.SameLength : RelationTypes.Perpendicular;

            int indexOfApex = polygon.apex.IndexOf(segment.p2);
            Polygon tmp = new Polygon(polygon);
            tmp.relations.Add(new Relation(type, first_to_relation, segment));
            tmp.relations.Add(new Relation(type, segment, first_to_relation));

            Polygon newPolygon = RelationPossible(tmp, indexOfApex);

            if (newPolygon != null)
            {
                polygons.Remove(polygon);
                polygons.Add(newPolygon);
                MessageBox.Show("Relation complete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Canvas.Invalidate();
            }
            else
            {
                MessageBox.Show("Zjebało sie", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            first_to_relation = (new Point(-1, -1), new Point(-1, -1));
            polygonToRelation = null;


        }

        private void DeleteRelation(Polygon polygon, (Point p1, Point p2) segment)
        {
            var result = polygon.relations.RemoveAll(x => (x.first_segment.p1 == segment.p1 && x.first_segment.p2 == segment.p2) || (x.second_segment.p1 == segment.p1 && x.second_segment.p2 == segment.p2));

            if (result != 0)
                MessageBox.Show($"Relation was deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private Point GetPointSameLength(Point p, Point previous_point, (Point p1, Point p2) segment)
        {
            double x1, x2;
            double length = Math.Sqrt((segment.p1.X - segment.p2.X) * (segment.p1.X - segment.p2.X) + (segment.p1.Y - segment.p2.Y) * (segment.p1.Y - segment.p2.Y));

            double straightA = ((double)(previous_point.Y - p.Y)) / ((double)(previous_point.X - p.X));
            if(double.IsNegativeInfinity(straightA))
            {
                return new Point(p.X, p.Y - (int)length);
            }
            if(double.IsPositiveInfinity(straightA))
            {
                return new Point(p.X, p.Y + (int)length);
            }
            if(double.IsNaN(straightA))
            {
                if(Math.Abs(p.Y-length)<Math.Abs(p.Y+length))
                    return new Point(p.X, p.Y - (int)length);

                return new Point(p.X, p.Y + (int)length);
            }
            double straightB = p.Y - straightA * p.X;

            double a = 1 + straightA * straightA;
            double b = 2 * straightA * straightB - 2 * p.X - 2 * straightA * p.Y;
            double c = p.X * p.X + p.Y * p.Y + straightB * straightB - 2 * p.Y * straightB - length * length;

            double d = b * b - 4 * a * c;

            if (d == 0)
            {
                x1 = Math.Ceiling(-b / (2.0 * a));
                x2 = x1;
            }
            else
            {
                x1 = Math.Ceiling((-b + Math.Sqrt(d)) / (2 * a));
                x2 = Math.Ceiling((-b - Math.Sqrt(d)) / (2 * a));
            }

            double newX = Math.Abs(previous_point.X - x1) < Math.Abs(previous_point.X - x2) ? x1 : x2;
            double newY = straightA * newX + straightB;

            return new Point((int)newX, (int)newY);
        }

        private Point GetPointPerpendicular(Point p, Point previous_point, (Point p1, Point p2) segment)
        {
            Point result;

            double straightA = 0;
            double perpendicularStraightA = ((double)(segment.p1.Y - segment.p2.Y)) / ((double)(segment.p1.X - segment.p2.X));
            if (double.IsInfinity(perpendicularStraightA))
                straightA = 0;
            else if(perpendicularStraightA == 0)
            {
                result = new Point(p.X, previous_point.Y);
            }
            else
                straightA = (-1) * perpendicularStraightA;

            double straightB = p.Y - straightA * p.X;

            result = new Point(previous_point.X, (int)(straightA * previous_point.X + straightB));

            return result;
        }

        private Polygon RelationPossible(Polygon polygon, int index)
        {
            int i = index-1;
            while(true)
            {
                if(i==-1)
                    i = polygon.segments.Count - 1;

                if (i == index)
                {
                    if (CheckAllRelation(polygon))
                        return polygon;

                    return null;
                }

                if (!polygon.relations.Any(x => (x.first_segment.p1 == polygon.segments[i].p1 && x.first_segment.p2 == polygon.segments[i].p2)))
                {
                    break;
                }

                Relation relation = polygon.relations.First(x => (x.first_segment.p1 == polygon.segments[i].p1 && x.first_segment.p2 == polygon.segments[i].p2));

                (Point, Point) first_segment = relation.first_segment;
                (Point, Point) second_segment = relation.second_segment;

                Point newPoint = GetPointSameLength(first_segment.Item2, first_segment.Item1, second_segment);

                //BrenshamDrawLine(new Pen(Color.White), first_segment.Item1, first_segment.Item2);
                //BrenshamDrawLine(new Pen(Color.Red), first_segment.Item1, newPoint);

                CorrectPolygonAfterRelation(ref polygon, first_segment.Item1, newPoint);

                i--;
            }

            i = index;
            while (true)
            {
                if (i == polygon.segments.Count)
                    i = 0;

                if (i == index-1)
                {
                    if (CheckAllRelation(polygon))
                        return polygon;

                    return null;
                }

                if (!polygon.relations.Any(x => (x.first_segment.p1 == polygon.segments[i].p1 && x.first_segment.p2 == polygon.segments[i].p2)))
                {
                    break;
                }

                Relation relation = polygon.relations.First(x => (x.first_segment.p1 == polygon.segments[i].p1 && x.first_segment.p2 == polygon.segments[i].p2));

                (Point, Point) first_segment = relation.first_segment;
                (Point, Point) second_segment = relation.second_segment;

                Point newPoint = GetPointSameLength(first_segment.Item2, first_segment.Item1, second_segment);

                //BrenshamDrawLine(new Pen(Color.White), first_segment.Item1, first_segment.Item2);
                //BrenshamDrawLine(new Pen(Color.Red), first_segment.Item1, newPoint);

                CorrectPolygonAfterRelation(ref polygon, first_segment.Item1, newPoint);
                i++;
            }

            return polygon;
        }

        private void CorrectPolygonAfterRelation(ref Polygon polygon, Point old_point, Point new_point)
        {
            if (polygon.start_point == old_point)
                polygon.start_point = new_point;

            for(int i=0; i<polygon.apex.Count; i++)
            {
                if (polygon.apex[i] == old_point)
                    polygon.apex[i] = new_point;
            }

            for(int i=0; i<polygon.segments.Count; i++)
            {
                if (polygon.segments[i].p1 == old_point)
                    polygon.segments[i] = (new_point, polygon.segments[i].p2);

                if (polygon.segments[i].p2 == old_point)
                    polygon.segments[i] = (polygon.segments[i].p1, new_point);
            }

            for(int i=0; i<polygon.relations.Count; i++)
            {
                if (polygon.relations[i].first_segment.p1 == old_point)
                    polygon.relations[i].first_segment = (new_point, polygon.relations[i].first_segment.p2);

                if (polygon.relations[i].first_segment.p2 == old_point)
                    polygon.relations[i].first_segment = (polygon.relations[i].first_segment.p1, new_point);

                if (polygon.relations[i].second_segment.p1 == old_point)
                    polygon.relations[i].second_segment = (new_point, polygon.relations[i].second_segment.p2);

                if (polygon.relations[i].second_segment.p2 == old_point)
                    polygon.relations[i].second_segment = (polygon.relations[i].second_segment.p1, new_point);
            }
        }

        private bool CheckAllRelation(Polygon polygon)
        {
            foreach(var relation in polygon.relations)
            {
                double length1 = GetSegmentLenght(relation.first_segment.p1, relation.first_segment.p2);
                double length2 = GetSegmentLenght(relation.second_segment.p1, relation.second_segment.p2);

                if (Math.Abs(length1 - length2) > 50)
                    return false;
            }
            return true;
        }

        private double GetSegmentLenght(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }

}
