using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor
{
    public class Relation
    {
        public RelationTypes type;
        public (Point p1, Point p2) first_segment;
        public (Point p1, Point p2) second_segment;

        public Relation(RelationTypes type, (Point p1, Point p2) first_segment, (Point p1, Point p2) second_segment)
        {
            this.type = type;
            this.first_segment = first_segment;
            this.second_segment = second_segment;
        }
    }

    public enum RelationTypes { Perpendicular, SameLength };
}
