using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Paint.Shapes
{
    public class clsCurve : clsShape
    {
        public override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddCurve(points.ToArray());
                return path;
            }
        }

        public override void updatePoint()
        {
            int minX, minY, maxX, maxY;
            minX = maxX = points[0].X;
            minY = maxY = points[0].Y;
            for (int i = 1; i < points.Count; i++)
            {
                if (minX > points[i].X)
                    minX = points[i].X;
                if (minY > points[i].Y)
                    minY = points[i].Y;
                if (maxX < points[i].X)
                    maxX = points[i].X;
                if (maxY < points[i].Y)
                    maxY = points[i].Y;
            }
            p1.X = minX;
            p1.Y = minY;
            p2.X = maxX;
            p2.Y = maxY;
            updateLocatedPoints();
        }
    }
}
