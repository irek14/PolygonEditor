﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor
{
    public class Polygon
    {
        public Point start_point;
        public List<Point> apex = new List<Point>();
        public List<(Point p1, Point p2)> segments = new List<(Point, Point)>();
        public List<Relation> relations;

        public Polygon(Point start_point)
        {
            this.start_point = start_point;
            apex = new List<Point>();
            apex.Add(start_point);

            segments = new List<(Point p1, Point p2)>();
            relations = new List<Relation>();
        }

        public Polygon()
        {

        }
    }
}
