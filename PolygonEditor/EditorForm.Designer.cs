﻿namespace PolygonEditor
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
            this.MainTable = new System.Windows.Forms.TableLayoutPanel();
            this.Canvas = new System.Windows.Forms.Panel();
            this.ToolboxPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ClearAllButton = new System.Windows.Forms.Button();
            this.MainTable.SuspendLayout();
            this.ToolboxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTable
            // 
            this.MainTable.ColumnCount = 2;
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.53469F));
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.46532F));
            this.MainTable.Controls.Add(this.Canvas, 1, 0);
            this.MainTable.Controls.Add(this.ToolboxPanel, 0, 0);
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(0, 0);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowCount = 1;
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainTable.Size = new System.Drawing.Size(1182, 653);
            this.MainTable.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(293, 3);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(886, 647);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // ToolboxPanel
            // 
            this.ToolboxPanel.ColumnCount = 2;
            this.ToolboxPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ToolboxPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ToolboxPanel.Controls.Add(this.ClearAllButton, 0, 0);
            this.ToolboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolboxPanel.Location = new System.Drawing.Point(3, 3);
            this.ToolboxPanel.Name = "ToolboxPanel";
            this.ToolboxPanel.RowCount = 4;
            this.ToolboxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolboxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolboxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolboxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolboxPanel.Size = new System.Drawing.Size(284, 647);
            this.ToolboxPanel.TabIndex = 1;
            // 
            // ClearAllButton
            // 
            this.ClearAllButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClearAllButton.Image = global::PolygonEditor.Properties.Resources.icon_clear_19;
            this.ClearAllButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ClearAllButton.Location = new System.Drawing.Point(3, 3);
            this.ClearAllButton.Name = "ClearAllButton";
            this.ClearAllButton.Size = new System.Drawing.Size(136, 155);
            this.ClearAllButton.TabIndex = 0;
            this.ClearAllButton.UseVisualStyleBackColor = true;
            this.ClearAllButton.Click += new System.EventHandler(this.ClearAllButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.MainTable);
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "EditorForm";
            this.RightToLeftLayout = true;
            this.Text = "Polygon editor";
            this.MainTable.ResumeLayout(false);
            this.ToolboxPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTable;
        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.TableLayoutPanel ToolboxPanel;
        private System.Windows.Forms.Button ClearAllButton;
    }
}
