namespace PolygonEditor
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.vertexMoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonMoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segmentMoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addVertexToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addRelationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sameLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perpendicularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTable = new System.Windows.Forms.TableLayoutPanel();
            this.Canvas = new System.Windows.Forms.Panel();
            this.Menu.SuspendLayout();
            this.MainTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawToolStripMenuItem,
            this.moveToolStripMenuItem2,
            this.deleteToolStripMenuItem,
            this.addVertexToolStripMenuItem,
            this.addRelationToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1582, 28);
            this.Menu.TabIndex = 1;
            this.Menu.Text = "menuStrip1";
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawToolStripMenuItem1,
            this.clearAllToolStripMenuItem1});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.drawToolStripMenuItem.Text = "Main";
            // 
            // drawToolStripMenuItem1
            // 
            this.drawToolStripMenuItem1.Checked = true;
            this.drawToolStripMenuItem1.CheckOnClick = true;
            this.drawToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawToolStripMenuItem1.Name = "drawToolStripMenuItem1";
            this.drawToolStripMenuItem1.Size = new System.Drawing.Size(138, 26);
            this.drawToolStripMenuItem1.Text = "Draw";
            this.drawToolStripMenuItem1.Click += new System.EventHandler(this.drawToolStripMenuItem1_Click);
            // 
            // clearAllToolStripMenuItem1
            // 
            this.clearAllToolStripMenuItem1.Name = "clearAllToolStripMenuItem1";
            this.clearAllToolStripMenuItem1.Size = new System.Drawing.Size(138, 26);
            this.clearAllToolStripMenuItem1.Text = "Clear all";
            this.clearAllToolStripMenuItem1.Click += new System.EventHandler(this.clearAllToolStripMenuItem1_Click);
            // 
            // moveToolStripMenuItem2
            // 
            this.moveToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vertexMoveToolStripMenuItem,
            this.polygonMoveToolStripMenuItem,
            this.segmentMoveToolStripMenuItem});
            this.moveToolStripMenuItem2.Name = "moveToolStripMenuItem2";
            this.moveToolStripMenuItem2.Size = new System.Drawing.Size(58, 24);
            this.moveToolStripMenuItem2.Text = "Move";
            // 
            // vertexMoveToolStripMenuItem
            // 
            this.vertexMoveToolStripMenuItem.CheckOnClick = true;
            this.vertexMoveToolStripMenuItem.Name = "vertexMoveToolStripMenuItem";
            this.vertexMoveToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.vertexMoveToolStripMenuItem.Text = "Vertex move";
            this.vertexMoveToolStripMenuItem.Click += new System.EventHandler(this.vertexMoveToolStripMenuItem_Click);
            // 
            // polygonMoveToolStripMenuItem
            // 
            this.polygonMoveToolStripMenuItem.CheckOnClick = true;
            this.polygonMoveToolStripMenuItem.Name = "polygonMoveToolStripMenuItem";
            this.polygonMoveToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.polygonMoveToolStripMenuItem.Text = "Polygon move";
            this.polygonMoveToolStripMenuItem.Click += new System.EventHandler(this.polygonMoveToolStripMenuItem_Click);
            // 
            // segmentMoveToolStripMenuItem
            // 
            this.segmentMoveToolStripMenuItem.CheckOnClick = true;
            this.segmentMoveToolStripMenuItem.Name = "segmentMoveToolStripMenuItem";
            this.segmentMoveToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.segmentMoveToolStripMenuItem.Text = "Segment move";
            this.segmentMoveToolStripMenuItem.Click += new System.EventHandler(this.segmentMoveToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.CheckOnClick = true;
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polygonToolStripMenuItem,
            this.vertexToolStripMenuItem});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // polygonToolStripMenuItem
            // 
            this.polygonToolStripMenuItem.CheckOnClick = true;
            this.polygonToolStripMenuItem.Name = "polygonToolStripMenuItem";
            this.polygonToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.polygonToolStripMenuItem.Text = "Polygon";
            this.polygonToolStripMenuItem.Click += new System.EventHandler(this.polygonToolStripMenuItem_Click);
            // 
            // vertexToolStripMenuItem
            // 
            this.vertexToolStripMenuItem.CheckOnClick = true;
            this.vertexToolStripMenuItem.Name = "vertexToolStripMenuItem";
            this.vertexToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.vertexToolStripMenuItem.Text = "Vertex";
            this.vertexToolStripMenuItem.Click += new System.EventHandler(this.vertexToolStripMenuItem_Click);
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Checked = true;
            this.addVertexToolStripMenuItem.CheckOnClick = true;
            this.addVertexToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addVertexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem1});
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.addVertexToolStripMenuItem.Text = "Add";
            // 
            // addVertexToolStripMenuItem1
            // 
            this.addVertexToolStripMenuItem1.CheckOnClick = true;
            this.addVertexToolStripMenuItem1.Name = "addVertexToolStripMenuItem1";
            this.addVertexToolStripMenuItem1.Size = new System.Drawing.Size(156, 26);
            this.addVertexToolStripMenuItem1.Text = "Add vertex";
            this.addVertexToolStripMenuItem1.Click += new System.EventHandler(this.addVertexToolStripMenuItem1_Click);
            // 
            // addRelationToolStripMenuItem
            // 
            this.addRelationToolStripMenuItem.CheckOnClick = true;
            this.addRelationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sameLengthToolStripMenuItem,
            this.perpendicularToolStripMenuItem});
            this.addRelationToolStripMenuItem.Name = "addRelationToolStripMenuItem";
            this.addRelationToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.addRelationToolStripMenuItem.Text = "Add relation";
            // 
            // sameLengthToolStripMenuItem
            // 
            this.sameLengthToolStripMenuItem.CheckOnClick = true;
            this.sameLengthToolStripMenuItem.Name = "sameLengthToolStripMenuItem";
            this.sameLengthToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.sameLengthToolStripMenuItem.Text = "Same length";
            this.sameLengthToolStripMenuItem.Click += new System.EventHandler(this.sameLengthToolStripMenuItem_Click);
            // 
            // perpendicularToolStripMenuItem
            // 
            this.perpendicularToolStripMenuItem.CheckOnClick = true;
            this.perpendicularToolStripMenuItem.Name = "perpendicularToolStripMenuItem";
            this.perpendicularToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.perpendicularToolStripMenuItem.Text = "Perpendicular";
            this.perpendicularToolStripMenuItem.Click += new System.EventHandler(this.perpendicularToolStripMenuItem_Click);
            // 
            // MainTable
            // 
            this.MainTable.ColumnCount = 1;
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTable.Controls.Add(this.Canvas, 0, 0);
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(0, 28);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowCount = 1;
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.Size = new System.Drawing.Size(1582, 825);
            this.MainTable.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(3, 3);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1576, 819);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.MainTable);
            this.Controls.Add(this.Menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1600, 900);
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "EditorForm";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polygon editor";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.MainTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem vertexMoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonMoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segmentMoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRelationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sameLengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perpendicularToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel MainTable;
        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem1;
    }
}

