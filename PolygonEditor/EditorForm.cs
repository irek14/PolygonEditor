using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ToolboxPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = Canvas.CreateGraphics();
            graph.DrawEllipse(Pens.Red, 0, 0, 25, 25);
        }
    }
}
