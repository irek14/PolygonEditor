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
        enum Mode { None, FirstPoint, Draw, DeleteVertex, DeletePolygon, MovePolygonStart, MovePolygon, MoveVertexStart, MoveVertex, MoveSegmentStart, MoveSegment, AddVertex, AddSameLengthRelation, AddPerpendicularRelation};
        public EditorForm()
        {
            InitializeComponent();
        }

        Pen pen = new Pen(Color.Black);
        Point? current_point = null;
        Point? previous_point = null;
        List<Polygon> polygons = new List<Polygon>();
        Polygon current_polygon;
        Mode current_mode = Mode.FirstPoint;
        Graphics graph;

        private void PaintAll()
        {
            foreach (Polygon polygon in polygons)
            {
                PaintAllPoints(Brushes.Black, polygon);
                foreach (var segment in polygon.segments)
                    BrenshamDrawLine(pen, segment.p1, segment.p2);
            }               
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (current_mode != Mode.AddPerpendicularRelation && current_mode != Mode.AddSameLengthRelation)
                first_to_relation = (new Point(-1, -1), new Point(-1, -1));

            if (current_mode == Mode.None)
            {
                current_mode = Mode.FirstPoint;
            }
            else if(current_mode == Mode.Draw)
            {
                //if (previous_point != null)
                //{
                //    BrenshamDrawLine(new Pen(Color.White), (Point)current_point, (Point)previous_point);

                //    PaintInstersectSegments((Point)current_point, (Point)previous_point);
                //}
                Point next_point = new Point(e.Location.X, e.Location.Y);
                CreateSegment(next_point);
                PaintAll();
            }
            else if(current_mode == Mode.DeleteVertex)
            {
                (Polygon polygon, Point vertex) = GetPolygonWithVertex(new Point(e.Location.X, e.Location.Y));
                if (polygon != null)
                {
                    DeleteVertex(polygon, vertex);
                    PaintAll();
                }
                polygons.RemoveAll(x => x.segments.Count == 0);
            }
            else if(current_mode == Mode.DeletePolygon)
            {
                (Polygon toDelete, (Point, Point) segment) = GetPolygonWithPointOnSegment(new Point(e.Location.X, e.Location.Y));
                if (toDelete != null)
                {
                    DeletePolygon(toDelete);
                    PaintAll();
                }
                    
            }
            else if(current_mode == Mode.MovePolygon)
            {
                current_mode = Mode.MovePolygonStart;
            }
            else if(current_mode == Mode.MoveVertex)
            {
                current_mode = Mode.MoveVertexStart;
            }
            else if(current_mode == Mode.MoveSegment)
            {
                current_mode = Mode.MoveSegmentStart;
            }
            else if(current_mode == Mode.AddVertex)
            {
                Point newPoint = new Point(e.Location.X, e.Location.Y);
                (Polygon toModify, (Point,Point) segment) = GetPolygonWithPointOnSegment(newPoint);
                if (toModify != null)
                {
                    AddVertex(toModify, segment, newPoint);
                    PaintAll();
                }                
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (current_mode == Mode.MovePolygonStart)
            {
                Point p = new Point(e.Location.X, e.Location.Y);
                (Polygon toMove, (Point, Point) segment) = GetPolygonWithPointOnSegment(p);
                if(toMove != null)
                {
                    start_move_point = p;
                    current_polygon = toMove;
                    current_mode = Mode.MovePolygon;
                    Cursor.Current = Cursors.NoMove2D;
                }
            }
            else if(current_mode == Mode.MoveVertexStart)
            {
                Point p = new Point(e.Location.X, e.Location.Y);
                (Polygon polygon, Point vertex) = GetPolygonWithVertex(p);
                if (polygon != null)
                {
                    start_move_point = p;
                    current_polygon = polygon;
                    vertex_to_move = vertex;
                    current_mode = Mode.MoveVertex;
                    Cursor.Current = Cursors.Hand;
                }
            }
            else if(current_mode == Mode.MoveSegmentStart)
            {
                Point p = new Point(e.Location.X, e.Location.Y);
                (Polygon toMove, (Point, Point) segment) = GetPolygonWithPointOnSegment(p);
                if (toMove != null)
                {
                    start_move_point = p;
                    segmentToMove = segment;
                    current_polygon = toMove;
                    current_mode = Mode.MoveSegment;
                    Cursor.Current = Cursors.NoMove2D;
                }
            }
            else if(current_mode == Mode.AddSameLengthRelation || current_mode == Mode.AddPerpendicularRelation)
            {
                Point p = new Point(e.Location.X, e.Location.Y);
                (Polygon toRelation, (Point p1, Point p2) segment) = GetPolygonWithPointOnSegment(p);
                if(toRelation != null)
                {
                    AddRelation(toRelation, segment);
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(current_mode != Mode.AddPerpendicularRelation && current_mode != Mode.AddSameLengthRelation)
                first_to_relation = (new Point(-1, -1), new Point(-1, -1));

            if (current_mode == Mode.FirstPoint && e.Button == MouseButtons.Left)
            {
                current_mode = Mode.Draw;
                Point point = new Point(e.Location.X, e.Location.Y);

                bool createNew = GetNotCompletedPolygon();
                if(createNew)
                {
                    current_polygon = new Polygon(point);
                    polygons.Add(current_polygon);
                    current_point = point;
                }
            }

            if (current_mode == Mode.Draw && e.Button == MouseButtons.Left)
            {                
                CreateLine(e);
                PaintAll();
            }

            if(current_mode == Mode.MovePolygon)
            {
                MovePolygon(new Point(e.Location.X, e.Location.Y));
                PaintAll();
            }

            if(current_mode == Mode.MoveVertex)
            {
                MoveVertex(new Point(e.Location.X, e.Location.Y));
                PaintAll();
            }

            if(current_mode == Mode.MoveSegment)
            {
                MoveSegment(new Point(e.Location.X, e.Location.Y));
                PaintAll();
            }

        }

        private void PaintAllPoints(Brush brush, Polygon polygon)
        {
            foreach(var apex in polygon.apex)
            {
                graph.FillRectangle(brush, apex.X-2, apex.Y-2, 4, 4);
            }  
        }

        private bool GetNotCompletedPolygon()
        {
            foreach(var polygon in polygons)
            {
                for(int i=0; i<polygon.segments.Count; i++)
                {
                    if (polygon.segments[i].p2 != polygon.segments[(i + 1) % polygon.segments.Count].p1)
                    {
                        current_polygon = polygon;
                        CorrectSegmentAndApex(ref current_polygon, i);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckSegmentIntersection(Point p1, Point p2, Point p3, Point p4)
        {
            int d1 = (p4.X - p3.X) * (p1.Y - p3.Y) - (p1.X - p3.X) * (p4.Y - p3.Y);
            int d2 = (p4.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p4.Y - p3.Y);
            int d3 = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            int d4 = (p2.X - p1.X) * (p4.Y - p1.Y) - (p4.X - p1.X) * (p2.Y - p1.Y);

            int d12 = d1 * d2;
            int d34 = d3 * d4;

            if (d12 > 0 || d34 > 0) return false;

            if (d12 < 0 || d34 < 0) return true;

            return OnRectangle(p1,p3,p4) || OnRectangle(p2,p3,p4) || OnRectangle(p3,p1,p2) || OnRectangle(p4,p1,p2);
        }

        private bool OnRectangle(Point q, Point p1, Point p2)
        {
            return Math.Min(p1.X, p2.X) <= q.X && q.X <= Math.Max(p1.X, p2.X) && Math.Min(p1.Y, p2.Y) <= q.Y && q.Y <= Math.Max(p1.Y, p2.Y);
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            polygons.Clear();
            ResetVariables();
            Canvas.Invalidate();
        }

        private void ResetVariables()
        {
            current_point = null;
            previous_point = null;
            current_mode = Mode.FirstPoint;
        }

        private void vertexMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.MoveVertexStart;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void polygonMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.MovePolygonStart;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void segmentMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.MoveSegmentStart;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.DeletePolygon;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void vertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.DeleteVertex;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void drawToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            current_mode = Mode.FirstPoint;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void clearAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            polygons.Clear();
            ResetVariables();
            Canvas.Invalidate();
        }

        private void addVertexToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            current_mode = Mode.AddVertex;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        public void UncheckOtherToolStripMenuItems(ToolStripMenuItem selectedMenuItem)
        {

            foreach(ToolStripMenuItem menu_item in Menu.Items)
            {
                foreach(ToolStripMenuItem item in menu_item.DropDown.Items)
                {
                    item.Checked = false;
                   
                }
                
             
            }

            selectedMenuItem.Checked = true;
        }

        private void sameLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.AddSameLengthRelation;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void perpendicularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.AddPerpendicularRelation;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
    }
}
