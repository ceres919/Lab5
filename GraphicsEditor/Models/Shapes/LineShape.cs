using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GraphicsEditor.Models.Shapes
{
    public class LineShape : ShapeEntity
    {
        private int[] startPoint = { 0, 0 };

        public int[] endPoint = { 0, 0 };

        public override LineShape AddToList(ShapeCreator cr)
        {
            Name = cr.shapeName;
            PointsParse(cr.shapeStartPoint, startPoint);
            PointsParse(cr.shapeEndPoint, endPoint);
            StrokeColor = cr.shapeStrokeColor;
            StrokeThickness = cr.shapeStrokeThickness;
            return this;
        }
        public override Shape AddThisShape(ShapeCreator cr)
        {
            PointsParse(cr.shapeStartPoint, startPoint);
            PointsParse(cr.shapeEndPoint, endPoint);
            return new Line
            {
                Name = cr.shapeName,
                StartPoint = new Point(startPoint[0], startPoint[1]),
                EndPoint = new Point(endPoint[0], endPoint[1]),
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness
            };
        }
        public void PointsParse(string str, int[] point)
        {
            var str_point = str.Split(",");
            point[0] = int.Parse(str_point[0]);
            point[1] = int.Parse(str_point[1]);

        }
    }
}
