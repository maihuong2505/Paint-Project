using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Paint.Shapes
{
    public class clsGroup : clsShape, IEnumerable
    {
        private List<clsShape> shapes = new List<clsShape>();

        public override GraphicsPath GraphicsPath => null;

        public GraphicsPath[] GraphicsPaths
        {
            get
            {
                GraphicsPath[] paths = new GraphicsPath[shapes.Count];
                for (int i = 0; i < shapes.Count; i++)
                {
                    paths[i] = new GraphicsPath();
                    if (shapes[i] is clsLine)
                        paths[i].AddLine(shapes[i].p1, shapes[i].p2);
                    else if (shapes[i] is clsEllipse)
                        paths[i].AddEllipse(new RectangleF(shapes[i].p1.X, shapes[i].p1.Y, shapes[i].p2.X - shapes[i].p1.X, shapes[i].p2.Y - shapes[i].p1.Y));
                    else if (shapes[i] is clsRectangle)
                        paths[i].AddRectangle(new RectangleF(shapes[i].p1.X, shapes[i].p1.Y, shapes[i].p2.X - shapes[i].p1.X, shapes[i].p2.Y - shapes[i].p1.Y));
                    else if (shapes[i] is clsCurve)
                        paths[i].AddCurve(shapes[i].points.ToArray());
                    else if (shapes[i] is clsPolygon)
                        paths[i].AddPolygon(shapes[i].points.ToArray());
                    else if (shapes[i] is clsGroup group)
                    {
                        for (int j = 0; j < group.GraphicsPaths.Length; j++)
                            paths[i].AddPath(group.GraphicsPaths[j], false);
                    }
                }
                return paths;
            }
        }

        public void add(clsShape shape)
        {
            shapes.Add(shape);
        }

        public override void draw(Graphics myGraphic)
        {            
            for (int i = 0; i < GraphicsPaths.Length; i++)
            {
                if (!(shapes[i] is clsGroup))
                {
                    if (shapes[i].shapeStyle == 1)
                        myGraphic.DrawPath(shapes[i].pen, shapes[i].GraphicsPath);
                    else
                    {
                        if (shapes[i].useSolidBrush)
                            myGraphic.FillPath(shapes[i].solidBrush, shapes[i].GraphicsPath);
                        else
                            myGraphic.FillPath(shapes[i].hatchBrush, shapes[i].GraphicsPath);
                        if (shapes[i].shapeStyle == 3)
                            myGraphic.DrawPath(shapes[i].pen, shapes[i].GraphicsPath);
                    }
                }
                else if (shapes[i] is clsGroup group)
                {
                    group.isSelected = false;
                    group.draw(myGraphic);
                }                    
            }
            updatePoint();
            if (isSelected)
                showFrame(myGraphic);
        }

        public override bool isHit(Point point)
        {            
            for (int i = 0; i < GraphicsPaths.Length; i++)
            {
                if (!(shapes[i] is clsGroup))
                {
                    if (shapes[i].shapeStyle != 1)
                    {
                        if (shapes[i].GraphicsPath.IsVisible(point))
                            return true;
                    }
                    else
                    {
                        Pen pen = new Pen(Color.Blue, 0.5f);
                        if (shapes[i].GraphicsPath.IsOutlineVisible(point, pen))
                            return true;
                    }
                }
                else if (shapes[i] is clsGroup group)
                    return group.isHit(point);
            }
            return false;
        }

        public override void updatePoint()
        {
            int minX, minY, maxX, maxY;
            minX = Math.Min(shapes[0].p1.X, shapes[0].p2.X);
            maxX = Math.Max(shapes[0].p1.X, shapes[0].p2.X);
            minY = Math.Min(shapes[0].p1.Y, shapes[0].p2.Y);
            maxY = Math.Max(shapes[0].p1.Y, shapes[0].p2.Y);            
            for (int i = 1; i < GraphicsPaths.Length; i++)
            {
                int min = Math.Min(shapes[i].p1.X, shapes[i].p2.X);
                int max = Math.Max(shapes[i].p1.X, shapes[i].p2.X);
                if (minX > min)
                    minX = min;
                if (maxX < max)
                    maxX = max;
                min = Math.Min(shapes[i].p1.Y, shapes[i].p2.Y);
                max = Math.Max(shapes[i].p1.Y, shapes[i].p2.Y);
                if (minY > min)
                    minY = min;
                if (maxY < max)
                    maxY = max;
            }
            p1.X = minX;
            p1.Y = minY;
            p2.X = maxX;
            p2.Y = maxY;
            updateLocatedPoints();
        }

        public void move(int dX, int dY)
        {
            for (int i = 0; i < shapes.Count; i++)
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
                    group.move(dX, dY);
                shapes[i].updatePoint();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return shapes.GetEnumerator();
        }        
    }
}
