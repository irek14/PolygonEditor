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

        public void AddRelation(Polygon polygon, (Point p1, Point p2) segment)
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

            if(first_to_relation.p1 == segment.p1 && first_to_relation.p2 == segment.p2)
            {
                MessageBox.Show("You cannot assign relation between the same segment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                first_to_relation = (new Point(-1, -1), new Point(-1, -1));
                polygonToRelation = null;
                return;
            }

            RelationTypes type = current_mode == Mode.AddSameLengthRelation ? RelationTypes.SameLength : RelationTypes.Perpendicular;

            polygon.relations.Add(new Relation(type, first_to_relation, segment));
            first_to_relation = (new Point(-1, -1), new Point(-1, -1));
            polygonToRelation = null;

            MessageBox.Show("Relation complete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
