using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Paint.Shapes
{
    public class clsLine : clsShape
    {
        public override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                Point p1Temp = p1, p2Temp = p2;
                setTempPoint(ref p1Temp, ref p2Temp);
                path.AddLine(p1Temp, p2Temp);
                return path;
            }
        }

        public override void setTempPoint(ref Point p1Temp, ref Point p2Temp)
        {
            if (!isSpecial)
                return;
            Point p3 = new Point(p2.X, p1.Y);
            double d1 = distance(p1, p3);
            double d2 = distance(p2, p3);
            double tan = d2 / d1;
            if (Math.Tan(Math.PI / 8) < tan && tan <= Math.Tan(3 * Math.PI / 8))
            {
                if (p2.Y > p1.Y)
                    p3.Y += Math.Abs(p3.X - p1.X);
                else
                    p3.Y -= Math.Abs(p3.X - p1.X);
            }
            else if (Math.Tan(3 * Math.PI / 8) < tan)
            {
                p3.X = p1.X;
                p3.Y = p2.Y;
            }
            p2Temp = p3;
        }

        public override void updatePoint()
        {
            setTempPoint(ref p1, ref p2);
            locatedPoints[0] = p1;
            locatedPoints[1] = p2;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public override void showFrame(Graphics myGraphic)
        {
            Rectangle rect = new Rectangle();
            SolidBrush brush = new SolidBrush(Color.DarkGray);
            rect.Width = 10;
            rect.Height = 10;
            for (int i = 0; i < 2; i++)
            {
                pointPaths[i] = new GraphicsPath();
                rect.X = locatedPoints[i].X - 5;
                rect.Y = locatedPoints[i].Y - 5;
                pointPaths[i].AddEllipse(rect);
                myGraphic.FillPath(brush, pointPaths[i]);
            }
        }

        public override int hitLocatedPoint(Point point)
        {
            for (int i = 0; i < 2; i++)
                if (pointPaths[i].IsVisible(point))
                    return i;
            return -1;
        }
    }
}
