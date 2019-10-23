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

        private void MyDrawLine(Pen pen, Point p1, Point p2)
        {
            if (current_line_algorithm == LineAlgorithm.Bresenham)
            {
                //algorithm source: https://pl.wikipedia.org/wiki/Algorytm_Bresenhama
                BresenhamDrawLine(pen, p1, p2);
            }
            else if (current_line_algorithm == LineAlgorithm.Library)
            {
                //graph.DrawLine() function
                LibraryDrawLine(pen, p1, p2);
            }
            else if (current_line_algorithm == LineAlgorithm.BresenhamSymetry)
            {
                //based on wikipedia code source of Bresenham, but symmetry modification is inspired by http://www.mini.pw.edu.pl/~kotowski/gk/RasterDrawing/RasterDrawing.pdf
                BresenhamSymmetryDrawLine(pen, p1, p2);
            }
            else if (current_line_algorithm == LineAlgorithm.AntialiasingWU)
            {
                //algorithm source: https://rosettacode.org/wiki/Xiaolin_Wu%27s_line_algorithm
                Line line = new Line(p1.X, p1.Y, p2.X, p2.Y, Color.Black, 0, 3);
                line.draw(graph);
            }
        }

        private void LibraryDrawLine(Pen pen, Point p1, Point p2)
        {
            graph.DrawLine(pen, p1, p2);
        }

        private void BresenhamSymmetryDrawLine(Pen pen, Point p1, Point p2)
        {
            int d, dx, dy, incre, incre2, increX, increY;
            int x1 = p1.X, x2 = p2.X, y1 = p1.Y, y2 = p2.Y;

            if (x1 < x2)
            {
                increX = 1;
                dx = x2 - x1;
            }
            else
            {
                increX = -1;
                dx = x1 - x2;
            }

            if (y1 < y2)
            {
                increY = 1;
                dy = y2 - y1;
            }
            else
            {
                increY = -1;
                dy = y1 - y2;
            }

            graph.FillRectangle(pen.Brush, x1, y1, 1, 1);

            if (dx > dy) //OX
            {
                incre = (dy - dx) * 2;
                incre2 = dy * 2;
                d = incre2 - dx;

                int c;
                if (x1 > x2)
                {
                    c = x1;
                    x1 = x2;
                    x2 = c;
                    c = y1;
                    y1 = y2;
                    y2 = c;
                    increX *= -1;
                    increY *= -1;
                }

                while (x1 < x2)
                {
                    x1 += increX;
                    x2 -= increX;
                    if (d >= 0)
                    {
                        y1 += increY;
                        y2 -= increY;
                        d += incre;
                    }
                    else
                    {
                        d += incre2;
                    }
                    graph.FillRectangle(pen.Brush, x1, y1, 1, 1);
                    graph.FillRectangle(pen.Brush, x2, y2, 1, 1);
                }
            }
            else //OY
            {
                incre = (dx - dy) * 2;
                incre2 = dx * 2;
                d = incre2 - dy;

                if (y1 > y2)
                {
                    int c;
                    c = x1;
                    x1 = x2;
                    x2 = c;
                    c = y1;
                    y1 = y2;
                    y2 = c;
                    increX *= -1;
                    increY *= -1;
                }

                while (y1 < y2)
                {
                    y1 += increY;
                    y2 -= increY;
                    if (d >= 0)
                    {
                        x1 += increX;
                        x2 -= increX;
                        d += incre;
                    }
                    else
                    {
                        d += incre2;
                    }
                    graph.FillRectangle(pen.Brush, x1, y1, 1, 1);
                    graph.FillRectangle(pen.Brush, x2, y2, 1, 1);
                }
            }
        }

        private void BresenhamDrawLine(Pen pen, Point p1, Point p2)
        {
            int d, dx, dy, incre, incre2, increX, increY;
            int x1 = p1.X, x2 = p2.X, y1 = p1.Y, y2 = p2.Y;

            if (x1 < x2)
            {
                increX = 1;
                dx = x2 - x1;
            }
            else
            {
                increX = -1;
                dx = x1 - x2;
            }

            if (y1 < y2)
            {
                increY = 1;
                dy = y2 - y1;
            }
            else
            {
                increY = -1;
                dy = y1 - y2;
            }

            graph.FillRectangle(pen.Brush, x1, y1, 1, 1);

            if (dx > dy) //OX
            {
                incre = (dy - dx) * 2;
                incre2 = dy * 2;
                d = incre2 - dx;

                while (x1 != x2)
                {
                    if (d >= 0)
                    {
                        x1 += increX;
                        y1 += increY;
                        d += incre;
                    }
                    else
                    {
                        d += incre2;
                        x1 += increX;
                    }
                    graph.FillRectangle(pen.Brush, x1, y1, 1, 1);
                }
            }
            else //OY
            {
                incre = (dx - dy) * 2;
                incre2 = dx * 2;
                d = incre2 - dy;
                while (y1 != y2)
                {
                    if (d >= 0)
                    {
                        x1 += increX;
                        y1 += increY;
                        d += incre;
                    }
                    else
                    {
                        y1 += increY;
                        d += incre2;
                    }
                    graph.FillRectangle(pen.Brush, x1, y1, 1, 1);
                }
            }
        }

        #region WUAntialiasing
        public class Line
        {
            private double x0, y0, x1, y1;
            private Color foreColor;
            private byte lineStyleMask;
            private int thickness;
            private float globalm;

            public Line(double x0, double y0, double x1, double y1, Color color, byte lineStyleMask, int thickness)
            {
                this.x0 = x0;
                this.y0 = y0;
                this.y1 = y1;
                this.x1 = x1;

                this.foreColor = color;

                this.lineStyleMask = lineStyleMask;

                this.thickness = thickness;
            }

            private void plot(Graphics graphics, double x, double y, double c)
            {
                int alpha = (int)(c * 255);
                if (alpha > 255) alpha = 255;
                if (alpha < 0) alpha = 0;
                Color color = Color.FromArgb(alpha, foreColor);
                try
                {
                    graphics.FillRectangle(new SolidBrush(color), (int)x, (int)y, 1, 1);
                }
                catch(Exception e)
                {

                }
            }

            int ipart(double x) { return (int)x; }

            int round(double x) { return ipart(x + 0.5); }

            double fpart(double x)
            {
                if (x < 0) return (1 - (x - Math.Floor(x)));
                return (x - Math.Floor(x));
            }

            double rfpart(double x)
            {
                return 1 - fpart(x);
            }


            public void draw(Graphics graph)
            {
                bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
                double temp;
                if (steep)
                {
                    temp = x0; x0 = y0; y0 = temp;
                    temp = x1; x1 = y1; y1 = temp;
                }
                if (x0 > x1)
                {
                    temp = x0; x0 = x1; x1 = temp;
                    temp = y0; y0 = y1; y1 = temp;
                }

                double dx = x1 - x0;
                double dy = y1 - y0;
                double gradient = dy / dx;

                double xEnd = round(x0);
                double yEnd = y0 + gradient * (xEnd - x0);
                double xGap = rfpart(x0 + 0.5);
                double xPixel1 = xEnd;
                double yPixel1 = ipart(yEnd);

                if (steep)
                {
                    plot(graph, yPixel1, xPixel1, rfpart(yEnd) * xGap);
                    plot(graph, yPixel1 + 1, xPixel1, fpart(yEnd) * xGap);
                }
                else
                {
                    plot(graph, xPixel1, yPixel1, rfpart(yEnd) * xGap);
                    plot(graph, xPixel1, yPixel1 + 1, fpart(yEnd) * xGap);
                }
                double intery = yEnd + gradient;

                xEnd = round(x1);
                yEnd = y1 + gradient * (xEnd - x1);
                xGap = fpart(x1 + 0.5);
                double xPixel2 = xEnd;
                double yPixel2 = ipart(yEnd);
                if (steep)
                {
                    plot(graph, yPixel2, xPixel2, rfpart(yEnd) * xGap);
                    plot(graph, yPixel2 + 1, xPixel2, fpart(yEnd) * xGap);
                }
                else
                {
                    plot(graph, xPixel2, yPixel2, rfpart(yEnd) * xGap);
                    plot(graph, xPixel2, yPixel2 + 1, fpart(yEnd) * xGap);
                }

                if (steep)
                {
                    for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                    {
                        plot(graph, ipart(intery), x, rfpart(intery));
                        plot(graph, ipart(intery) + 1, x, fpart(intery));
                        intery += gradient;
                    }
                }
                else
                {
                    for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                    {
                        plot(graph, x, ipart(intery), rfpart(intery));
                        plot(graph, x, ipart(intery) + 1, fpart(intery));
                        intery += gradient;
                    }
                }
            }
        }

        #endregion
    }
}
