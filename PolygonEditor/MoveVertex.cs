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
            Polygon tmp = new Polygon(current_polygon);

            List<(Point, Point)> toDelete = new List<(Point, Point)>();

            int index = tmp.apex.IndexOf(vertex_to_move);
            CorrectPolygonAfterRelation(ref tmp, vertex_to_move, p);
            
            Polygon newPolygon = RelationPossible(tmp, index);

            if(newPolygon == null)
            {
                return;
            }
            else
            {
                polygons.Remove(current_polygon);
                polygons.Add(newPolygon);
                current_polygon = newPolygon;

                vertex_to_move = current_polygon.apex[index];
            }
        }
    }
}
