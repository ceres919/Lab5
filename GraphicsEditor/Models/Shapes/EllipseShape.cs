using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class EllipseShape : ShapeEntity
    {
        private int[] startPoint = { 0, 0 };
        public int Width { get; set; }
        public int Height { get; set; }
        public string FillColor { get; set; }
        public override EllipseShape AddToList(ShapeCreator cr)
        {
            Name = cr.shapeName;
            PointsParse(cr.shapeStartPoint, startPoint);
            Width = cr.shapeWidth;
            Height = cr.shapeHeight;
            StrokeColor = cr.shapeStrokeColor;
            StrokeThickness = cr.shapeStrokeThickness;
            FillColor = cr.shapeFillColor;
            return this;
        }
        public override Shape AddThisShape(ShapeCreator cr)
        {
            var stpoint = cr.shapeStartPoint.Split(',');
            startPoint[0] = int.Parse(stpoint[0]);
            startPoint[1] = int.Parse(stpoint[1]);

            return new Ellipse
            {
                Name = cr.shapeName,
                Margin = new(startPoint[0], startPoint[1],0,0),
                Height = cr.shapeHeight,
                Width = cr.shapeWidth,
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(cr.shapeFillColor))
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
