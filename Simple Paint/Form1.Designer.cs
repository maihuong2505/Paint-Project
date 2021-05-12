namespace Simple_Paint
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUngroup = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.cbbBrushStyle = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSwap = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFillColor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rdbFillWithLine = new System.Windows.Forms.RadioButton();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnLineColor = new System.Windows.Forms.Button();
            this.rdbFillShape = new System.Windows.Forms.RadioButton();
            this.rdbNoFill = new System.Windows.Forms.RadioButton();
            this.lblWidth = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trbWidth = new System.Windows.Forms.TrackBar();
            this.cbbDashStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnCurve = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.pnlPaint = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUngroup);
            this.panel1.Controls.Add(this.btnGroup);
            this.panel1.Controls.Add(this.cbbBrushStyle);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnSwap);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnFillColor);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rdbFillWithLine);
            this.panel1.Controls.Add(this.btnRectangle);
            this.panel1.Controls.Add(this.btnLineColor);
            this.panel1.Controls.Add(this.rdbFillShape);
            this.panel1.Controls.Add(this.rdbNoFill);
            this.panel1.Controls.Add(this.lblWidth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.trbWidth);
            this.panel1.Controls.Add(this.cbbDashStyle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.btnPolygon);
            this.panel1.Controls.Add(this.btnCurve);
            this.panel1.Controls.Add(this.btnEllipse);
            this.panel1.Controls.Add(this.btnLine);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1136, 610);
            this.panel1.TabIndex = 0;
            // 
            // btnUngroup
            // 
            this.btnUngroup.Location = new System.Drawing.Point(331, 62);
            this.btnUngroup.Name = "btnUngroup";
            this.btnUngroup.Size = new System.Drawing.Size(113, 23);
            this.btnUngroup.TabIndex = 24;
            this.btnUngroup.Text = "Ungroup";
            this.btnUngroup.UseVisualStyleBackColor = true;
            this.btnUngroup.Click += new System.EventHandler(this.BtnUngroup_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.Location = new System.Drawing.Point(207, 63);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(113, 23);
            this.btnGroup.TabIndex = 23;
            this.btnGroup.Text = "Group";
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.BtnGroup_Click);
            // 
            // cbbBrushStyle
            // 
            this.cbbBrushStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBrushStyle.FormattingEnabled = true;
            this.cbbBrushStyle.Items.AddRange(new object[] {
            "BackwardDiagonal",
            "DarkDownwardDiagonal",
            "LightUpwardDiagonal",
            "Percent50",
            "Shingle",
            "Solid"});
            this.cbbBrushStyle.Location = new System.Drawing.Point(740, 50);
            this.cbbBrushStyle.Name = "cbbBrushStyle";
            this.cbbBrushStyle.Size = new System.Drawing.Size(121, 21);
            this.cbbBrushStyle.TabIndex = 22;
            this.cbbBrushStyle.SelectedIndexChanged += new System.EventHandler(this.CbbBrushStyle_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(677, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Brush Style";
            // 
            // btnSwap
            // 
            this.btnSwap.Location = new System.Drawing.Point(541, 51);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(52, 23);
            this.btnSwap.TabIndex = 20;
            this.btnSwap.Text = "Swap";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.BtnSwap_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "<=====";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(544, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "=====>";
            // 
            // btnFillColor
            // 
            this.btnFillColor.BackColor = System.Drawing.Color.Blue;
            this.btnFillColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillColor.Location = new System.Drawing.Point(607, 14);
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(40, 40);
            this.btnFillColor.TabIndex = 17;
            this.btnFillColor.UseVisualStyleBackColor = false;
            this.btnFillColor.Click += new System.EventHandler(this.BtnFillColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(605, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Fill Color";
            // 
            // rdbFillWithLine
            // 
            this.rdbFillWithLine.AutoSize = true;
            this.rdbFillWithLine.Location = new System.Drawing.Point(26, 57);
            this.rdbFillWithLine.Name = "rdbFillWithLine";
            this.rdbFillWithLine.Size = new System.Drawing.Size(116, 17);
            this.rdbFillWithLine.TabIndex = 15;
            this.rdbFillWithLine.TabStop = true;
            this.rdbFillWithLine.Text = "Fill Shape with Line";
            this.rdbFillWithLine.UseVisualStyleBackColor = true;
            this.rdbFillWithLine.CheckedChanged += new System.EventHandler(this.RdbFillWithLine_CheckedChanged);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Location = new System.Drawing.Point(369, 6);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(75, 23);
            this.btnRectangle.TabIndex = 2;
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.BtnRectangle_Click);
            // 
            // btnLineColor
            // 
            this.btnLineColor.BackColor = System.Drawing.Color.Black;
            this.btnLineColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLineColor.Location = new System.Drawing.Point(486, 14);
            this.btnLineColor.Name = "btnLineColor";
            this.btnLineColor.Size = new System.Drawing.Size(40, 40);
            this.btnLineColor.TabIndex = 14;
            this.btnLineColor.UseVisualStyleBackColor = false;
            this.btnLineColor.Click += new System.EventHandler(this.BtnLineColor_Click);
            // 
            // rdbFillShape
            // 
            this.rdbFillShape.AutoSize = true;
            this.rdbFillShape.Location = new System.Drawing.Point(26, 34);
            this.rdbFillShape.Name = "rdbFillShape";
            this.rdbFillShape.Size = new System.Drawing.Size(133, 17);
            this.rdbFillShape.TabIndex = 13;
            this.rdbFillShape.TabStop = true;
            this.rdbFillShape.Text = "Fill Shape with No Line";
            this.rdbFillShape.UseVisualStyleBackColor = true;
            this.rdbFillShape.CheckedChanged += new System.EventHandler(this.RdbFillShape_CheckedChanged);
            // 
            // rdbNoFill
            // 
            this.rdbNoFill.AutoSize = true;
            this.rdbNoFill.Location = new System.Drawing.Point(26, 11);
            this.rdbNoFill.Name = "rdbNoFill";
            this.rdbNoFill.Size = new System.Drawing.Size(88, 17);
            this.rdbNoFill.TabIndex = 0;
            this.rdbNoFill.TabStop = true;
            this.rdbNoFill.Text = "No Fill Shape";
            this.rdbNoFill.UseVisualStyleBackColor = true;
            this.rdbNoFill.CheckedChanged += new System.EventHandler(this.RdbNoFill_CheckedChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidth.Location = new System.Drawing.Point(1097, 35);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(15, 16);
            this.lblWidth.TabIndex = 12;
            this.lblWidth.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(889, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Width";
            // 
            // trbWidth
            // 
            this.trbWidth.Location = new System.Drawing.Point(930, 21);
            this.trbWidth.Minimum = 1;
            this.trbWidth.Name = "trbWidth";
            this.trbWidth.Size = new System.Drawing.Size(161, 45);
            this.trbWidth.TabIndex = 10;
            this.trbWidth.Value = 1;
            this.trbWidth.Scroll += new System.EventHandler(this.TrbWidth_Scroll);
            // 
            // cbbDashStyle
            // 
            this.cbbDashStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDashStyle.FormattingEnabled = true;
            this.cbbDashStyle.Items.AddRange(new object[] {
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot",
            "Solid"});
            this.cbbDashStyle.Location = new System.Drawing.Point(740, 21);
            this.cbbDashStyle.Name = "cbbDashStyle";
            this.cbbDashStyle.Size = new System.Drawing.Size(121, 21);
            this.cbbDashStyle.TabIndex = 9;
            this.cbbDashStyle.SelectedIndexChanged += new System.EventHandler(this.CbbDashStyle_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(677, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dash Style";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(480, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Line Color";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(369, 35);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(288, 35);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(75, 23);
            this.btnPolygon.TabIndex = 6;
            this.btnPolygon.Text = "Polygon";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.BtnPolygon_Click);
            // 
            // btnCurve
            // 
            this.btnCurve.Location = new System.Drawing.Point(207, 35);
            this.btnCurve.Name = "btnCurve";
            this.btnCurve.Size = new System.Drawing.Size(75, 23);
            this.btnCurve.TabIndex = 5;
            this.btnCurve.Text = "Curve";
            this.btnCurve.UseVisualStyleBackColor = true;
            this.btnCurve.Click += new System.EventHandler(this.BtnCurve_Click);
            // 
            // btnEllipse
            // 
            this.btnEllipse.Location = new System.Drawing.Point(288, 6);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(75, 23);
            this.btnEllipse.TabIndex = 3;
            this.btnEllipse.Text = "Ellipse";
            this.btnEllipse.UseVisualStyleBackColor = true;
            this.btnEllipse.Click += new System.EventHandler(this.BtnEllipse_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(207, 6);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(75, 23);
            this.btnLine.TabIndex = 1;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.BtnLine_Click);
            // 
            // pnlPaint
            // 
            this.pnlPaint.BackColor = System.Drawing.Color.White;
            this.pnlPaint.Location = new System.Drawing.Point(3, 93);
            this.pnlPaint.Name = "pnlPaint";
            this.pnlPaint.Size = new System.Drawing.Size(1129, 515);
            this.pnlPaint.TabIndex = 1;
            this.pnlPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlPaint_MouseDown);
            this.pnlPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PnlPaint_MouseMove);
            this.pnlPaint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PnlPaint_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 610);
            this.Controls.Add(this.pnlPaint);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Paint";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trbWidth;
        private System.Windows.Forms.ComboBox cbbDashStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnCurve;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Panel pnlPaint;
        private System.Windows.Forms.RadioButton rdbFillShape;
        private System.Windows.Forms.RadioButton rdbNoFill;
        private System.Windows.Forms.Button btnLineColor;
        private System.Windows.Forms.RadioButton rdbFillWithLine;
        private System.Windows.Forms.ComboBox cbbBrushStyle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUngroup;
        private System.Windows.Forms.Button btnGroup;
    }
}

