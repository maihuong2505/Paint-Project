using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Paint.Shapes
{
    public class clsEllipse : clsShape
    {
        public override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                Point p1Temp = p1, p2Temp = p2;
                setTempPoint(ref p1Temp, ref p2Temp);
                Rectangle rectangle = new Rectangle(p1Temp.X, p1Temp.Y, p2Temp.X - p1Temp.X, p2Temp.Y - p1Temp.Y);
                path.AddEllipse(rectangle);
                return path;
            }
        }

        public override void updatePoint()
        {
            setTempPoint(ref p1, ref p2);
            updateLocatedPoints();
        }
    }
}
