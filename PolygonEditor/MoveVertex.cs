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

            Polygon tmp = new Polygon(current_polygon);

            List<(Point, Point)> toDelete = new List<(Point, Point)>();

            for (int i = 0; i < tmp.segments.Count; i++)
            {
                if (tmp.segments[i].p1 == vertex_to_move)
                    toDelete.Add(tmp.segments[i]);

                if(tmp.segments[i].p2 == vertex_to_move)
                    toDelete.Add(tmp.segments[i]);
            }

            int index = tmp.apex.IndexOf(vertex_to_move);
            CorrectPolygonAfterRelation(ref tmp, vertex_to_move, p);
            
            Polygon newPolygon = RelationPossible(tmp, index);

            if(newPolygon == null || index == -1)
            {
                MessageBox.Show("Zjebało sie", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                current_mode = Mode.MoveVertexStart;
            }
            else
            {

                foreach (var segment in toDelete)
                    DeleteSegment(segment);

                polygons.Remove(current_polygon);
                polygons.Add(newPolygon);
                current_polygon = newPolygon;

                vertex_to_move = current_polygon.apex[index];
            }
        }
    }
}
