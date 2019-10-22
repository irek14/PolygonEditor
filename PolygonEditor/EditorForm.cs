using PolygonEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PolygonEditor
{
    public partial class EditorForm : Form
    {
        enum Mode { None, FirstPoint, Draw, DeleteVertex, DeletePolygon,DeleteRelation, MovePolygonStart, MovePolygon, MoveVertexStart, MoveVertex, MoveSegmentStart, MoveSegment, AddVertex, AddSameLengthRelation, AddPerpendicularRelation};
        public EditorForm()
        {
            InitializeComponent();
        }

        bool drawCurrentLine = false;
        Pen pen = new Pen(Color.Black);
        Point? current_point = null;
        Point? previous_point = null;
        List<Polygon> polygons = new List<Polygon>();
        Polygon current_polygon;
        Mode current_mode = Mode.FirstPoint;
        Graphics graph;
        (Point, Point) current_line;

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
                Point next_point = new Point(e.Location.X, e.Location.Y);
                CreateSegment(next_point);
                drawCurrentLine = false;
            }
            else if(current_mode == Mode.DeleteVertex)
            {
                (Polygon polygon, Point vertex) = GetPolygonWithVertex(new Point(e.Location.X, e.Location.Y));
                if (polygon != null)
                {
                    DeleteVertex(polygon, vertex);
                }
                polygons.RemoveAll(x => x.segments.Count == 0);
            }
            else if(current_mode == Mode.DeletePolygon)
            {
                (Polygon toDelete, (Point, Point) segment) = GetPolygonWithPointOnSegment(new Point(e.Location.X, e.Location.Y));
                if (toDelete != null)
                {
                    DeletePolygon(toDelete);
                }                    
            }
            else if(current_mode == Mode.DeleteRelation)
            {
                (Polygon polygon, (Point, Point) segment) = GetPolygonWithPointOnSegment(new Point(e.Location.X, e.Location.Y));
                if (polygon != null)
                {
                    DeleteRelation(polygon, segment);
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
                }                
            }

            Canvas.Invalidate();
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
                current_line = ((Point)current_point, (Point)previous_point);
                drawCurrentLine = true;
                Canvas.Invalidate();
            }

            if(current_mode == Mode.MovePolygon)
            {
                MovePolygon(new Point(e.Location.X, e.Location.Y));
            }

            if(current_mode == Mode.MoveVertex)
            {
                MoveVertex(new Point(e.Location.X, e.Location.Y));
                Canvas.Invalidate();
            }

            if(current_mode == Mode.MoveSegment)
            {
                MoveSegment(new Point(e.Location.X, e.Location.Y));
                Canvas.Invalidate();
            }

            Canvas.Invalidate();
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

        private void PaintGraphics(Polygon polygon)
        {
            Color[] colors = new Color[] { Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Pink, Color.Pink, Color.Fuchsia, Color.Fuchsia, Color.Orange, Color.Orange, Color.Gold, Color.Gold, Color.Coral, Color.Coral };
            int colorIndex = 0;

            foreach (var relation in polygon.relations)
            {
                int x1 = Math.Max(relation.first_segment.p1.X, relation.first_segment.p2.X) - Math.Abs((relation.first_segment.p1.X - relation.first_segment.p2.X)) / 2;
                int y1 = Math.Max(relation.first_segment.p1.Y, relation.first_segment.p2.Y) - Math.Abs((relation.first_segment.p1.Y - relation.first_segment.p2.Y)) / 2;
                int x2 = Math.Max(relation.second_segment.p1.X, relation.second_segment.p2.X) - Math.Abs((relation.second_segment.p1.X - relation.second_segment.p2.X)) / 2;
                int y2 = Math.Max(relation.second_segment.p1.Y, relation.second_segment.p2.Y) - Math.Abs((relation.second_segment.p1.Y - relation.second_segment.p2.Y)) / 2;

                try
                {
                    graph.FillRectangle(new SolidBrush(colors[colorIndex % colors.Length]), x1 - 2, y1 - 2, 20, 20);
                    graph.FillRectangle(new SolidBrush(colors[colorIndex % colors.Length]), x2 - 2, y2 - 2, 20, 20);
                    colorIndex++;
                    if (relation.type == RelationTypes.Perpendicular)
                    {
                        graph.DrawImage(Resources.PerpendicularIcon, new Point(x1, y1));
                        graph.DrawImage(Resources.PerpendicularIcon, new Point(x2, y2));
                    }
                    else
                    {
                        graph.DrawImage(Resources.EqualsIcon, new Point(x1, y1));
                        graph.DrawImage(Resources.EqualsIcon, new Point(x2, y2));
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
        }

        private void PaintAll()
        {
            if (drawCurrentLine)
                BresenhamDrawLine(pen, current_line.Item1, current_line.Item2);
            foreach (Polygon polygon in polygons)
            {
                PaintGraphics(polygon);
                PaintAllPoints(Brushes.Black, polygon);
                foreach (var segment in polygon.segments)
                    BresenhamDrawLine(pen, segment.p1, segment.p2);
            }

            if (first_to_relation.p1.X != -1)
            {
                graph.FillRectangle(Brushes.Red, first_to_relation.p1.X - 3, first_to_relation.p1.Y - 3, 6, 6);
                graph.FillRectangle(Brushes.Red, first_to_relation.p2.X - 3, first_to_relation.p2.Y - 3, 6, 6);
            }
        }

        private void ResetVariables()
        {
            current_point = null;
            previous_point = null;
            current_mode = Mode.FirstPoint;

            foreach (ToolStripMenuItem menu_item in Menu.Items)
            {
                foreach (ToolStripMenuItem item in menu_item.DropDown.Items)
                {
                    if (item.Text == "Draw")
                        item.Checked = true;
                    else
                        item.Checked = false;

                }
            }
        }

        #region MenuButtons

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
        private void relationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_mode = Mode.DeleteRelation;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        #endregion

    }
}
