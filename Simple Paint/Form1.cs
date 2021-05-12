using Simple_Paint.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Paint
{
    public partial class Form1 : Form
    {
        #region Global variables
        private bool isSelecting = false;
        private bool selectManyShapes = false;        
        private bool newCurveOrPolygon = false;
        private string lastKey;
        private int locatedPointNo;
        private int shapeStyle = 1;//1: No fill shape; 2: Fill shape with no line; 3: Fill shape with solid line
        private int chooseShape = 0;     //0: Haven't chosen; 1: Line; 2: Ellipse; 3: Rectangle;
        private bool haveClicked = false;//4: Curve; 5: Polygon; 6: Select shapes
        private Point oldLocation;
        private Point newLocation;
        private clsShape myShape;
        private List<clsShape> shapes = new List<clsShape>();
        private BufferedGraphicsContext currentContext;
        private BufferedGraphics myBuffer;
        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();
            cbbDashStyle.Text = "Solid";
            cbbBrushStyle.Text = "Solid";
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            pnlPaint.Focus();
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(pnlPaint.CreateGraphics(), pnlPaint.DisplayRectangle);
        }
        #endregion

        #region Methods
        public void redraw()
        {
            Graphics myGraphic = myBuffer.Graphics;
            myGraphic.SmoothingMode = SmoothingMode.AntiAlias;
            myGraphic.Clear(Color.White);
            for (int i = 0; i < shapes.Count; i++)
                shapes[i].draw(myGraphic);
            myBuffer.Render(pnlPaint.CreateGraphics());
        }

        private Pen setPen()
        {
            Pen myPen = new Pen(btnLineColor.BackColor, trbWidth.Value);
            string cbbString = cbbDashStyle.SelectedItem.ToString();
            if (cbbString == "Dash")
                myPen.DashStyle = DashStyle.Dash;
            else if (cbbString == "DashDot")
                myPen.DashStyle = DashStyle.DashDot;
            else if (cbbString == "DashDotDot")
                myPen.DashStyle = DashStyle.DashDotDot;
            else if (cbbString == "Dot")
                myPen.DashStyle = DashStyle.Dot;
            else
                myPen.DashStyle = DashStyle.Solid;
            return myPen;
        }

        private HatchBrush setHatchBrush()
        {
            HatchStyle style;
            string cbbString = cbbBrushStyle.SelectedItem.ToString();
            if (cbbString == "BackwardDiagonal")
                style = HatchStyle.BackwardDiagonal;
            else if (cbbString == "DarkDownwardDiagonal")
                style = HatchStyle.DarkDownwardDiagonal;
            else if (cbbString == "LightUpwardDiagonal")
                style = HatchStyle.LightUpwardDiagonal;
            else if (cbbString == "Percent50")
                style = HatchStyle.Percent50;
            else
                style = HatchStyle.Shingle;
            return new HatchBrush(style, btnFillColor.BackColor);
        }

        private int indexOfShapeSelected(Point point)
        {
            int i = shapes.Count - 1;
            while (i >= 0 && !shapes[i].isHit(point))
                i--;
            return i;
        }

        private void deselectAll()
        {
            for (int i = 0; i < shapes.Count; i++)
                shapes[i].isSelected = false;
            redraw();
        }

        private void moveShape(int dX, int dY)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].isSelected)
                {
                    if (shapes[i] is clsLine || shapes[i] is clsEllipse || shapes[i] is clsRectangle)
                    {
                        shapes[i].p1.X += dX;
                        shapes[i].p1.Y += dY;
                        shapes[i].p2.X += dX;
                        shapes[i].p2.Y += dY;                        
                    }
                    else if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                    {
                        Point temp = new Point();
                        for (int j = 0; j < shapes[i].points.Count; j++)
                        {
                            temp = shapes[i].points[j];
                            temp.X += dX;
                            temp.Y += dY;
                            shapes[i].points[j] = temp;
                        }                        
                    }
                    else if (shapes[i] is clsGroup group)
                    {
                        group.move(dX, dY);
                    }
                    shapes[i].updatePoint();
                }
            }
            redraw();
        }

        private void changeSize(int dX, int dY)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].isSelected)
                {
                    shapes[i].isSpecial = false;
                    Point temp = new Point();
                    if (locatedPointNo == 0)//upper-left corner
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point =>
                            point.X + dX == shapes[i].p2.X || point.Y + dY == shapes[i].p2.Y))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.X == shapes[i].p2.X)
                                {
                                    if (temp.Y == shapes[i].p2.Y)
                                        continue;
                                    else
                                        temp.Y += dY;
                                }
                                else if (temp.Y == shapes[i].p2.Y)
                                {
                                    temp.X += dX;
                                }
                                else
                                {
                                    temp.X += dX;
                                    temp.Y += dY;
                                }
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p1.X += dX;
                            shapes[i].p1.Y += dY;
                        }
                        else if (shapes[i] is clsLine)
                        {
                            shapes[i].p1.X += dX;
                            shapes[i].p1.Y += dY;
                        }
                        else
                        {
                            if (shapes[i].p1.X + dX + 15 < shapes[i].p2.X)
                                shapes[i].p1.X += dX;
                            if (shapes[i].p1.Y + dY + 15 < shapes[i].p2.Y)
                                shapes[i].p1.Y += dY;
                        }
                    }
                    else if (locatedPointNo == 1)//upper or upper-left corner if shape is line
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point => point.Y + dY == shapes[i].p2.Y))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.Y != shapes[i].p2.Y)
                                    temp.Y += dY;
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p1.Y += dY;
                        }
                        else if (shapes[i] is clsLine)
                        {
                            shapes[i].p2.X += dX;
                            shapes[i].p2.Y += dY;
                        }
                        else
                        {
                            if (shapes[i].p1.Y + dY + 15 < shapes[i].p2.Y)
                                shapes[i].p1.Y += dY;
                        }
                    }
                    else if (locatedPointNo == 2)//upper-right corner
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point =>
                            point.X + dX == shapes[i].p1.X || point.Y + dY == shapes[i].p2.Y))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.X == shapes[i].p1.X)
                                {
                                    if (temp.Y == shapes[i].p2.Y)
                                        continue;
                                    else
                                        temp.Y += dY;
                                }
                                else if (temp.Y == shapes[i].p2.Y)
                                {
                                    temp.X += dX;
                                }
                                else
                                {
                                    temp.X += dX;
                                    temp.Y += dY;
                                }
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p1.Y += dY;
                            shapes[i].p2.X += dX;
                        }
                        else
                        {
                            if (shapes[i].p1.Y + dY + 15 < shapes[i].p2.Y)
                                shapes[i].p1.Y += dY;
                            if (shapes[i].p2.X + dX - 15 > shapes[i].p1.X)
                                shapes[i].p2.X += dX;
                        }
                    }
                    else if (locatedPointNo == 3)//left
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point => point.X + dX == shapes[i].p2.X))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.X != shapes[i].p2.X)
                                    temp.X += dX;
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p1.X += dX;
                        }
                        else
                        {
                            if (shapes[i].p1.X + dX + 15 < shapes[i].p2.X)
                                shapes[i].p1.X += dX;
                        }
                    }
                    else if (locatedPointNo == 4)//right
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point => point.X + dX == shapes[i].p1.X))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.X != shapes[i].p1.X)
                                    temp.X += dX;
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p2.X += dX;
                        }
                        else
                        {
                            if (shapes[i].p2.X + dX - 15 > shapes[i].p1.X)
                                shapes[i].p2.X += dX;
                        }
                    }
                    else if (locatedPointNo == 5)//lower-left corner
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point =>
                            point.X + dX == shapes[i].p2.X || point.Y + dY == shapes[i].p1.Y))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.X == shapes[i].p2.X)
                                {
                                    if (temp.Y == shapes[i].p1.Y)
                                        continue;
                                    else
                                        temp.Y += dY;
                                }
                                else if (temp.Y == shapes[i].p1.Y)
                                {
                                    temp.X += dX;
                                }
                                else
                                {
                                    temp.X += dX;
                                    temp.Y += dY;
                                }
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p1.X += dX;
                            shapes[i].p2.Y += dY;
                        }
                        else
                        {
                            if (shapes[i].p1.X + dX + 15 < shapes[i].p2.X)
                                shapes[i].p1.X += dX;
                            if (shapes[i].p2.Y + dY - 15 > shapes[i].p1.Y)
                                shapes[i].p2.Y += dY;
                        }
                    }
                    else if (locatedPointNo == 6)//lower
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point => point.Y + dY == shapes[i].p1.Y))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.Y != shapes[i].p1.Y)
                                    temp.Y += dY;
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p2.Y += dY;
                        }
                        else
                        {
                            if (shapes[i].p2.Y + dY - 15 > shapes[i].p1.Y)
                                shapes[i].p2.Y += dY;
                        }
                    }
                    else
                    {
                        if (shapes[i] is clsCurve || shapes[i] is clsPolygon)
                        {
                            if (shapes[i].points.Exists(point =>
                            point.X + dX == shapes[i].p1.X || point.Y + dY == shapes[i].p1.Y))
                                break;
                            for (int j = 0; j < shapes[i].points.Count; j++)
                            {
                                temp = shapes[i].points[j];
                                if (temp.X == shapes[i].p1.X)
                                {
                                    if (temp.Y == shapes[i].p1.Y)
                                        continue;
                                    else
                                        temp.Y += dY;
                                }
                                else if (temp.Y == shapes[i].p1.Y)
                                {
                                    temp.X += dX;
                                }
                                else
                                {
                                    temp.X += dX;
                                    temp.Y += dY;
                                }
                                shapes[i].points[j] = temp;
                            }
                            shapes[i].p2.X += dX;
                            shapes[i].p2.Y += dY;
                        }
                        else
                        {
                            if (shapes[i].p2.X + dX - 15 > shapes[i].p1.X)
                                shapes[i].p2.X += dX;
                            if (shapes[i].p2.Y + dY - 15 > shapes[i].p1.Y)
                                shapes[i].p2.Y += dY;
                        }
                    }
                    shapes[i].updatePoint();
                }
            }
            redraw();
        }

        private void showPropertiesOfShape(clsShape shape)
        {
            if (shape.shapeStyle == 1)
            {
                btnLineColor.BackColor = shape.pen.Color;
                trbWidth.Value = (int)shape.pen.Width;
                lblWidth.Text = trbWidth.Value.ToString();
                if (shape.pen.DashStyle == DashStyle.Dash)
                    cbbDashStyle.Text = "Dash";
                else if (shape.pen.DashStyle == DashStyle.DashDot)
                    cbbDashStyle.Text = "DashDot";
                else if (shape.pen.DashStyle == DashStyle.DashDotDot)
                    cbbDashStyle.Text = "DashDotDot";
                else if (shape.pen.DashStyle == DashStyle.Dot)
                    cbbDashStyle.Text = "Dot";
                else
                    cbbDashStyle.Text = "Solid";
            }
            else if (shape.shapeStyle == 2)
            {
                if (shape.useSolidBrush)
                {
                    btnFillColor.BackColor = shape.solidBrush.Color;
                    cbbBrushStyle.Text = "Solid";
                }
                else
                {
                    btnFillColor.BackColor = shape.hatchBrush.ForegroundColor;
                    if (shape.hatchBrush.HatchStyle == HatchStyle.BackwardDiagonal)
                        cbbBrushStyle.Text = "BackwardDiagonal";
                    else if (shape.hatchBrush.HatchStyle == HatchStyle.DarkDownwardDiagonal)
                        cbbBrushStyle.Text = "DarkDownwardDiagonal";
                    else if (shape.hatchBrush.HatchStyle == HatchStyle.LightUpwardDiagonal)
                        cbbBrushStyle.Text = "LightUpwardDiagonal";
                    else if (shape.hatchBrush.HatchStyle == HatchStyle.Percent50)
                        cbbBrushStyle.Text = "Percent50";
                    else
                        cbbBrushStyle.Text = "Shingle";
                }
            }
            else
            {
                btnLineColor.BackColor = shape.pen.Color;
                trbWidth.Value = (int)shape.pen.Width;
                lblWidth.Text = trbWidth.Value.ToString();
                if (shape.pen.DashStyle == DashStyle.Dash)
                    cbbDashStyle.Text = "Dash";
                else if (shape.pen.DashStyle == DashStyle.DashDot)
                    cbbDashStyle.Text = "DashDot";
                else if (shape.pen.DashStyle == DashStyle.DashDotDot)
                    cbbDashStyle.Text = "DashDotDot";
                else if (shape.pen.DashStyle == DashStyle.Dot)
                    cbbDashStyle.Text = "Dot";
                else
                    cbbDashStyle.Text = "Solid";

                if (shape.useSolidBrush)
                {
                    btnFillColor.BackColor = shape.solidBrush.Color;
                    cbbBrushStyle.Text = "Solid";
                }
                else
                {
                    btnFillColor.BackColor = shape.hatchBrush.ForegroundColor;
                    if (shape.hatchBrush.HatchStyle == HatchStyle.BackwardDiagonal)
                        cbbBrushStyle.Text = "BackwardDiagonal";
                    else if (shape.hatchBrush.HatchStyle == HatchStyle.DarkDownwardDiagonal)
                        cbbBrushStyle.Text = "DarkDownwardDiagonal";
                    else if (shape.hatchBrush.HatchStyle == HatchStyle.LightUpwardDiagonal)
                        cbbBrushStyle.Text = "LightUpwardDiagonal";
                    else if (shape.hatchBrush.HatchStyle == HatchStyle.Percent50)
                        cbbBrushStyle.Text = "Percent50";
                    else
                        cbbBrushStyle.Text = "Shingle";
                }
            }
        }
        #endregion

        #region Events

        #region Click buttons
        private void BtnLine_Click(object sender, EventArgs e)
        {
            chooseShape = 1;
            isSelecting = false;
            deselectAll();
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            pnlPaint.Cursor = Cursors.Cross;
            pnlPaint.Focus();
        }

        private void BtnEllipse_Click(object sender, EventArgs e)
        {
            chooseShape = 2;
            isSelecting = false;
            deselectAll();
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            pnlPaint.Cursor = Cursors.Cross;
            pnlPaint.Focus();
        }

        private void BtnRectangle_Click(object sender, EventArgs e)
        {
            chooseShape = 3;
            isSelecting = false;
            deselectAll();
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            pnlPaint.Cursor = Cursors.Cross;
            pnlPaint.Focus();
        }

        private void BtnCurve_Click(object sender, EventArgs e)
        {
            chooseShape = 4;
            isSelecting = false;
            deselectAll();
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            newCurveOrPolygon = true;
            pnlPaint.Cursor = Cursors.Cross;
            pnlPaint.Focus();
        }

        private void BtnPolygon_Click(object sender, EventArgs e)
        {
            chooseShape = 5;
            isSelecting = false;
            deselectAll();
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            newCurveOrPolygon = true;
            pnlPaint.Cursor = Cursors.Cross;
            pnlPaint.Focus();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            chooseShape = 6;
            isSelecting = true;
            btnGroup.Enabled = true;
            btnUngroup.Enabled = true;
            pnlPaint.Cursor = Cursors.Default;
            pnlPaint.Focus();
        }

        private void BtnLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnLineColor.BackColor = dialog.Color;
                if (shapes.Exists(shape => shape.isSelected))
                {
                    for (int i = 0; i < shapes.Count; i++)
                        if (shapes[i].isSelected && shapes[i].shapeStyle != 2)
                            shapes[i].pen = setPen();
                    redraw();
                }
            }
            newCurveOrPolygon = true;
            pnlPaint.Focus();
        }

        private void BtnFillColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnFillColor.BackColor = dialog.Color;
                if (shapes.Exists(shape => shape.isSelected))
                {
                    for (int i = 0; i < shapes.Count; i++)
                        if (shapes[i].isSelected && shapes[i].shapeStyle != 1)
                        {
                            shapes[i].solidBrush = new SolidBrush(btnFillColor.BackColor);
                            shapes[i].hatchBrush = setHatchBrush();
                        }
                    redraw();
                }
            }
            newCurveOrPolygon = true;
            pnlPaint.Focus();
        }

        private void BtnSwap_Click(object sender, EventArgs e)
        {
            Color temp = btnLineColor.BackColor;
            btnLineColor.BackColor = btnFillColor.BackColor;
            btnFillColor.BackColor = temp;
            if (shapes.Exists(shape => shape.isSelected))
            {
                for (int i = 0; i < shapes.Count; i++)
                    if (shapes[i].isSelected)
                    {
                        shapes[i].pen = setPen();
                        shapes[i].solidBrush = new SolidBrush(btnFillColor.BackColor);
                        shapes[i].hatchBrush = setHatchBrush();
                    }
                redraw();
            }
            newCurveOrPolygon = true;
            pnlPaint.Focus();
        }

        private void BtnGroup_Click(object sender, EventArgs e)
        {
            if (shapes.Count(shape => shape.isSelected) > 1)
            {
                clsGroup group = new clsGroup();
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i].isSelected)
                    {
                        group.add(shapes[i]);
                        shapes.RemoveAt(i--);
                    }
                }
                group.isSelected = true;
                shapes.Add(group);
                redraw();
                pnlPaint.Focus();
            }
        }

        private void BtnUngroup_Click(object sender, EventArgs e)
        {
            if (shapes.Find(shape => shape.isSelected) is clsGroup group)
            {
                foreach (clsShape shape in group)
                {
                    shape.isSelected = true;
                    shapes.Add(shape);
                }
                shapes.Remove(group);
                redraw();
                pnlPaint.Focus();
            }
        }
        #endregion

        #region Mouse
        private void PnlPaint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                haveClicked = true;
                if (chooseShape == 1)
                {
                    myShape = new clsLine();
                    myShape.shapeStyle = 1;
                    myShape.p1 = e.Location;
                    myShape.pen = setPen();
                    shapes.Add(myShape);
                }
                else if (chooseShape == 2)
                {
                    myShape = new clsEllipse();
                    myShape.p1 = e.Location;
                    if (shapeStyle == 1)
                    {
                        myShape.pen = setPen();
                        myShape.shapeStyle = 1;
                    }
                    else
                    {
                        if (cbbBrushStyle.SelectedItem.ToString() == "Solid")
                        {
                            myShape.solidBrush = new SolidBrush(btnFillColor.BackColor);
                            myShape.useSolidBrush = true;
                        }
                        else
                        {
                            myShape.hatchBrush = setHatchBrush();
                            myShape.useSolidBrush = false;
                        }
                        if (shapeStyle == 2)
                            myShape.shapeStyle = 2;
                        else
                        {
                            myShape.pen = setPen();
                            myShape.shapeStyle = 3;
                        }
                    }
                    shapes.Add(myShape);
                }
                else if (chooseShape == 3)
                {
                    myShape = new clsRectangle();
                    myShape.p1 = e.Location;
                    if (shapeStyle == 1)
                    {
                        myShape.pen = setPen();
                        myShape.shapeStyle = 1;
                    }
                    else
                    {
                        if (cbbBrushStyle.SelectedItem.ToString() == "Solid")
                        {
                            myShape.solidBrush = new SolidBrush(btnFillColor.BackColor);
                            myShape.useSolidBrush = true;
                        }
                        else
                        {
                            myShape.hatchBrush = setHatchBrush();
                            myShape.useSolidBrush = false;
                        }
                        if (shapeStyle == 2)
                            myShape.shapeStyle = 2;
                        else
                        {
                            myShape.pen = setPen();
                            myShape.shapeStyle = 3;
                        }
                    }
                    shapes.Add(myShape);
                }
                else if (chooseShape == 4)
                {
                    if (newCurveOrPolygon)
                    {
                        myShape = new clsCurve();
                        myShape.points.Add(e.Location);
                        myShape.points.Add(e.Location);
                        myShape.shapeStyle = 1;
                        myShape.pen = setPen();
                        shapes.Add(myShape);
                        newCurveOrPolygon = false;
                    }
                    else
                    {
                        myShape.points.Add(e.Location);
                        myShape.points.Add(e.Location);
                    }
                }
                else if (chooseShape == 5)
                {
                    if (newCurveOrPolygon)
                    {
                        myShape = new clsPolygon();
                        myShape.points.Add(e.Location);
                        myShape.points.Add(e.Location);
                        if (shapeStyle == 1)
                        {
                            myShape.pen = setPen();
                            myShape.shapeStyle = 1;
                        }
                        else
                        {
                            if (cbbBrushStyle.SelectedItem.ToString() == "Solid")
                            {
                                myShape.solidBrush = new SolidBrush(btnFillColor.BackColor);
                                myShape.useSolidBrush = true;
                            }
                            else
                            {
                                myShape.hatchBrush = setHatchBrush();
                                myShape.useSolidBrush = false;
                            }
                            if (shapeStyle == 2)
                                myShape.shapeStyle = 2;
                            else
                            {
                                myShape.pen = setPen();
                                myShape.shapeStyle = 3;
                            }
                        }
                        shapes.Add(myShape);
                        newCurveOrPolygon = false;
                    }
                    else
                    {
                        myShape.points.Add(e.Location);
                        myShape.points.Add(e.Location);
                    }
                }
                else if (chooseShape == 6)
                {
                    oldLocation = e.Location;
                    isSelecting = true;
                    int i = indexOfShapeSelected(e.Location);
                    if (i < 0)
                    {
                        if (pnlPaint.Cursor != Cursors.Default)
                            return;
                        if (!selectManyShapes)
                            deselectAll();
                    }
                    else
                    {
                        if (selectManyShapes)
                        {
                            shapes[i].isSelected = !shapes[i].isSelected;                            
                        }
                        else
                        {
                            deselectAll();
                            shapes[i].isSelected = true;
                            if (!(shapes[i] is clsGroup))
                                showPropertiesOfShape(shapes[i]);
                        }                        
                        redraw();
                    }
                }
            }
            else
            {
                if (chooseShape == 4 || chooseShape == 5)
                {
                    newCurveOrPolygon = true;
                    haveClicked = false;
                    shapes[shapes.Count - 1].updatePoint();
                }
            }
        }

        private void PnlPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if (!haveClicked && !isSelecting)
                return;
            int n = shapes.Count;
            if (n == 0)
                return;
            if (chooseShape == 1 || chooseShape == 2 || chooseShape == 3)
            {
                shapes[n - 1].p2 = e.Location;
                redraw();
            }
            else if (chooseShape == 4 || chooseShape == 5)
            {
                int m = shapes[n - 1].points.Count;
                shapes[n - 1].points[m - 1] = e.Location;
                redraw();
            }
            else
            {
                if (!haveClicked)
                {
                    int i = indexOfShapeSelected(e.Location);
                    if (i >= 0)
                        pnlPaint.Cursor = Cursors.SizeAll;
                    else
                    {
                        for (i = 0; i < n; i++)
                        {
                            if (!shapes[i].isSelected)
                                continue;
                            locatedPointNo = shapes[i].hitLocatedPoint(e.Location);
                            if (locatedPointNo >= 0)
                            {
                                if (shapes[i] is clsLine)
                                    pnlPaint.Cursor = Cursors.SizeNWSE;
                                else
                                {
                                    if (locatedPointNo == 0 || locatedPointNo == 7)
                                        pnlPaint.Cursor = Cursors.SizeNWSE;
                                    else if (locatedPointNo == 1 || locatedPointNo == 6)
                                        pnlPaint.Cursor = Cursors.SizeNS;
                                    else if (locatedPointNo == 2 || locatedPointNo == 5)
                                        pnlPaint.Cursor = Cursors.SizeNESW;
                                    else
                                        pnlPaint.Cursor = Cursors.SizeWE;
                                }
                                break;
                            }
                        }
                        if (i == n)
                            pnlPaint.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    newLocation = e.Location;
                    int dX, dY;
                    dX = newLocation.X - oldLocation.X;
                    dY = newLocation.Y - oldLocation.Y;
                    if (pnlPaint.Cursor == Cursors.SizeAll)
                        moveShape(dX, dY);
                    else if (pnlPaint.Cursor != Cursors.Default)
                        changeSize(dX, dY);
                    oldLocation = newLocation;
                }
            }
        }

        private void PnlPaint_MouseUp(object sender, MouseEventArgs e)
        {
            if (!haveClicked)
                return;
            haveClicked = false;
            if (chooseShape == 0)
                return;
            int n = shapes.Count;
            if (n == 0)
                return;
            if (chooseShape == 1 || chooseShape == 2 || chooseShape == 3)
            {
                shapes[n - 1].p2 = e.Location;
                redraw();
                shapes[n - 1].updatePoint();
            }
            else if (chooseShape == 4 || chooseShape == 5)
            {
                int m = shapes[n - 1].points.Count;
                shapes[n - 1].points[m - 1] = e.Location;
                redraw();
                shapes[n - 1].updatePoint();
            }
        }
        #endregion

        #region Key
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey && haveClicked)
            {
                lastKey = "Shift";
                shapes[shapes.Count - 1].isSpecial = true;
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                lastKey = "Ctrl";
                selectManyShapes = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (shapes.Count == 0)
                    return;
                lastKey = "Delete";                
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i].isSelected)
                    {
                        shapes.RemoveAt(i);
                        i--;
                    }
                }
                redraw();
            }
            else if (e.KeyCode == Keys.Left)
            {
                lastKey = "Arrow";
                moveShape(-1, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                lastKey = "Arrow";
                moveShape(1, 0);
            }
            else if (e.KeyCode == Keys.Up)
            {
                lastKey = "Arrow";
                moveShape(0, -1);
            }
            else if (e.KeyCode == Keys.Down)
            {
                lastKey = "Arrow";
                moveShape(0, 1);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (lastKey == "Delete")
            {
                pnlPaint.Cursor = Cursors.Default;
                return;
            }
            else if (lastKey == "Shift")
                shapes[shapes.Count - 1].isSpecial = false;
            else if (lastKey == "Ctrl")
                selectManyShapes = false;
        }
        #endregion

        #region Others
        private void RdbNoFill_CheckedChanged(object sender, EventArgs e)
        {
            btnLine.Enabled = true;
            btnCurve.Enabled = true;
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            shapeStyle = 1;
            chooseShape = 0;
            newCurveOrPolygon = true;
            deselectAll();
            pnlPaint.Cursor = Cursors.Default;
        }

        private void RdbFillShape_CheckedChanged(object sender, EventArgs e)
        {
            btnLine.Enabled = false;
            btnCurve.Enabled = false;
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            shapeStyle = 2;
            chooseShape = 0;
            newCurveOrPolygon = true;
            deselectAll();
            pnlPaint.Cursor = Cursors.Default;
        }

        private void RdbFillWithLine_CheckedChanged(object sender, EventArgs e)
        {
            btnLine.Enabled = false;
            btnCurve.Enabled = false;
            btnGroup.Enabled = false;
            btnUngroup.Enabled = false;
            shapeStyle = 3;
            chooseShape = 0;
            newCurveOrPolygon = true;
            deselectAll();
            pnlPaint.Cursor = Cursors.Default;
        }

        private void CbbDashStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            newCurveOrPolygon = true;
            if (shapes.Exists(shape => shape.isSelected))
            {
                for (int i = 0; i < shapes.Count; i++)
                    if (shapes[i].isSelected)
                        shapes[i].pen = setPen();
                redraw();
            }
        }

        private void CbbBrushStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            newCurveOrPolygon = true;
            if (shapes.Exists(shapes => shapes.isSelected))
            {
                for (int i = 0; i < shapes.Count; i++)
                    if (shapes[i].isSelected)
                    {
                        if (cbbBrushStyle.SelectedItem.ToString() == "Solid")
                        {
                            shapes[i].useSolidBrush = true;
                            shapes[i].solidBrush = new SolidBrush(btnFillColor.BackColor);
                        }
                        else
                        {
                            shapes[i].useSolidBrush = false;
                            shapes[i].hatchBrush = setHatchBrush();
                        }
                    }
                redraw();
            }
        }

        private void TrbWidth_Scroll(object sender, EventArgs e)
        {
            lblWidth.Text = trbWidth.Value.ToString();
            newCurveOrPolygon = true;
            if (shapes.Exists(shape => shape.isSelected))
            {
                for (int i = 0; i < shapes.Count; i++)
                    if (shapes[i].isSelected)
                        shapes[i].pen = setPen();
                redraw();
            }
        }
        #endregion

        #endregion
    }
}
