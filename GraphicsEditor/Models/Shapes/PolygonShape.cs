using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class PolygonShape : ShapeEntity
    {
        public string Points { get; set; }
        public string FillColor { get; set; }
        
        public override PolygonShape AddToList(ShapeCreator cr)
        {
            Name = cr.shapeName;
            Points = cr.shapePoints;
            StrokeColor = cr.shapeStrokeColor;
            StrokeThickness = cr.shapeStrokeThickness;
            FillColor = cr.shapeFillColor;
            return this;
        }
        public override Shape AddThisShape(ShapeCreator cr)
        {
            Points points = PointsParse(cr.shapePoints);

            return new Polygon
            {
                Name = cr.shapeName,
                Points = points,
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(cr.shapeFillColor))
            };
        }
        public Points PointsParse(string str)
        {
            Points points = new Points();
            var str_points = str.Split(" ");
            foreach (var point in str_points)
            {
                var str_point = point.Split(",");
                var p1 = int.Parse(str_point[0]);
                var p2 = int.Parse(str_point[1]);
                points.Add(new Point(p1, p2));
            }
            return points;
        }
    }
}
