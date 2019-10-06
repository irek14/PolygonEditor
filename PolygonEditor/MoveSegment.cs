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
            graph.FillRectangle(Brushes.White, segmentToMove.p1.X - 2, segmentToMove.p1.Y - 2, 4, 4);
            graph.FillRectangle(Brushes.White, segmentToMove.p2.X - 2, segmentToMove.p2.Y - 2, 4, 4);

            int dX = start_move_point.X - p.X;
            int dY = start_move_point.Y - p.Y;
            (Point, Point) segmentToDelete;

            Point newApex1 = new Point(segmentToMove.p1.X - dX, segmentToMove.p1.Y - dY);
            Point newApex2 = new Point(segmentToMove.p2.X - dX, segmentToMove.p2.Y - dY);

            for(int i=0; i<current_polygon.segments.Count; i++)
            {
                if(current_polygon.segments[i].p1 == segmentToMove.p1 && current_polygon.segments[i].p2 == segmentToMove.p2)
                {
                    segmentToDelete = current_polygon.segments[i];
                    current_polygon.segments[i] = (newApex1, newApex2);
                    DeleteSegment(segmentToDelete);
                    BrenshamDrawLine(pen, newApex1, newApex2);
                }
                else if(current_polygon.segments[i].p2 == segmentToMove.p1)
                {
                    segmentToDelete = current_polygon.segments[i];                   
                    current_polygon.segments[i] = (current_polygon.segments[i].p1, newApex1);
                    DeleteSegment(segmentToDelete);
                    BrenshamDrawLine(pen,current_polygon.segments[i].p1, newApex1);
                }
                else if(current_polygon.segments[i].p1 == segmentToMove.p2)
                {
                    segmentToDelete = current_polygon.segments[i];
                    current_polygon.segments[i] = (newApex2, current_polygon.segments[i].p2);
                    DeleteSegment(segmentToDelete);
                    BrenshamDrawLine(pen,newApex2, current_polygon.segments[i].p2);
                }
            }

            for(int i =0; i<current_polygon.apex.Count; i++)
            {
                if (current_polygon.apex[i] == segmentToMove.p1)
                    current_polygon.apex[i] = newApex1;
                else if(current_polygon.apex[i] == segmentToMove.p2)
                    current_polygon.apex[i] = newApex2;
            }

            segmentToMove = (newApex1, newApex2);
            start_move_point = p;
        }
    }
}
