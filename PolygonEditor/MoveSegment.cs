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
        (Point p1, Point p2) segmentToMove;

        private void MoveSegment(Point p)
        {
            int dX = start_move_point.X - p.X;
            int dY = start_move_point.Y - p.Y;

            Polygon tmp = new Polygon(current_polygon);

            Point newApex1 = new Point(segmentToMove.p1.X - dX, segmentToMove.p1.Y - dY);
            Point newApex2 = new Point(segmentToMove.p2.X - dX, segmentToMove.p2.Y - dY);

            int index1 = tmp.apex.IndexOf(segmentToMove.p1);
            int index2 = tmp.apex.IndexOf(segmentToMove.p2);

            CorrectPolygonAfterRelation(ref tmp, segmentToMove.p1, newApex1);
            CorrectPolygonAfterRelation(ref tmp, segmentToMove.p2, newApex2);

            Polygon newPolygon = RelationPossible(tmp, index1);
            if(newPolygon == null)
            {
                MessageBox.Show("This operation is blocked because of the polygon relations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                current_mode = Mode.MoveVertexStart;
                return;
            }

            newPolygon = RelationPossible(tmp, index2);
            if (newPolygon == null)
            {
                MessageBox.Show("This operation is blocked because of the polygon relations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                current_mode = Mode.MoveVertexStart;
                return;
            }

            polygons.Remove(current_polygon);
            polygons.Add(newPolygon);
            current_polygon = newPolygon;

            segmentToMove = (newPolygon.apex[index1], newPolygon.apex[index2]);
            start_move_point = p;
        }
    }
}
