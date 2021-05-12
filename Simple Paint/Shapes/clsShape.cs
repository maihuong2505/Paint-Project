using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Paint.Shapes
{
    public abstract class clsShape
    {
        public Point p1;
        public Point p2;
        public List<Point> points = new List<Point>();
        public Point[] locatedPoints = new Point[8];
        public GraphicsPath[] pointPaths = new GraphicsPath[8];
        public Pen pen;
        public SolidBrush solidBrush;
        public HatchBrush hatchBrush;
        public int shapeStyle;
        public bool useSolidBrush;
        public bool isSpecial;
        public bool isSelected;

        public abstract GraphicsPath GraphicsPath { get; }
        public abstract void updatePoint();

        public virtual void draw(Graphics myGraphic)
        {
            if (shapeStyle == 1)
                myGraphic.DrawPath(pen, GraphicsPath);
            else
            {
                if (useSolidBrush)
                    myGraphic.FillPath(solidBrush, GraphicsPath);
                else
                    myGraphic.FillPath(hatchBrush, GraphicsPath);
                if (shapeStyle == 3)
                    myGraphic.DrawPath(pen, GraphicsPath);
            }
            if (isSelected)
                showFrame(myGraphic);
        }

        public virtual bool isHit(Point point)
        {
            if (shapeStyle != 1)
                return GraphicsPath.IsVisible(point);
            Pen pen = new Pen(Color.Blue, 0.5f);
            return GraphicsPath.IsOutlineVisible(point, pen);
        }

        public virtual void setTempPoint(ref Point p1Temp, ref Point p2Temp)
        {
            int temp;
            if (p1Temp.X < p2Temp.X)
            {
                if (p1Temp.Y > p2Temp.Y)
                {
                    if (isSpecial)
                    {
                        temp = p2Temp.X - p1Temp.X;
                        p2Temp.X = p1Temp.X + temp;
                        p2Temp.Y = p1Temp.Y;
                        p1Temp.Y -= temp;
                    }
                    else
                    {
                        temp = p2Temp.Y;
                        p2Temp.Y = p1Temp.Y;
                        p1Temp.Y = temp;
                    }
                }
                else
                {
                    if (isSpecial)
                    {
                        temp = p2Temp.X - p1Temp.X;
                        p2Temp.X = p1Temp.X + temp;
                        p2Temp.Y = p1Temp.Y + temp;
                    }
                }
            }
            else
            {
                if (p1Temp.Y > p2Temp.Y)
                {
                    if (isSpecial)
                    {
                        temp = p1Temp.X - p2Temp.X;
                        p2Temp.X = p1Temp.X - temp;
                        p2Temp.Y = p1Temp.Y - temp;
                    }
                    Point tempPoint = p1Temp;
                    p1Temp = p2Temp;
                    p2Temp = tempPoint;
                }
                else
                {
                    if (isSpecial)
                    {
                        temp = p1Temp.X - p2Temp.X;
                        p2Temp.X = p1Temp.X - temp;
                        p2Temp.Y = p1Temp.Y + temp;
                    }
                    temp = p1Temp.X;
                    p1Temp.X = p2Temp.X;
                    p2Temp.X = temp;
                }
            }
        }

        public virtual void updateLocatedPoints()
        {
            Point point = new Point();
            locatedPoints[0] = p1;
            point.X = p1.X + (p2.X - p1.X) / 2;
            point.Y = p1.Y;
            locatedPoints[1] = point;
            point.X = p2.X;
            locatedPoints[2] = point;
            point.X = p1.X;
            point.Y = p1.Y + (p2.Y - p1.Y) / 2;
            locatedPoints[3] = point;
            point.X = p2.X;
            locatedPoints[4] = point;
            point.X = p1.X;
            point.Y = p2.Y;
            locatedPoints[5] = point;
            point.X = p1.X + (p2.X - p1.X) / 2;
            locatedPoints[6] = point;
            locatedPoints[7] = p2;
        }

        public virtual void showFrame(Graphics myGraphic)
        {
            Pen pen = new Pen(Color.Blue, 0.5f);
            pen.DashStyle = DashStyle.Dash;
            Rectangle rect = new Rectangle();
            rect = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            pointPaths[0] = new GraphicsPath();
            pointPaths[0].AddRectangle(rect);
            myGraphic.DrawPath(pen, pointPaths[0]);

            SolidBrush brush = new SolidBrush(Color.DarkGray);
            rect.Width = 10;
            rect.Height = 10;
            for (int i = 0; i < 8; i++)
            {
                pointPaths[i] = new GraphicsPath();
                rect.X = locatedPoints[i].X - 5;
                rect.Y = locatedPoints[i].Y - 5;
                pointPaths[i].AddEllipse(rect);
                myGraphic.FillPath(brush, pointPaths[i]);
            }
        }

        public virtual int hitLocatedPoint(Point point)
        {
            for (int i = 0; i < 8; i++)
                if (pointPaths[i].IsVisible(point))
                    return i;
            return -1;
        }
    }
}
