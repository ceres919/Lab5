using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class PolygonShape : ShapeEntity
    {
        public string Points { get; set; }
        public string FillColor { get; set; }
        public PolygonShape() { }
        public PolygonShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness)
        {
            Points = cr.shapePoints;
            FillColor = cr.shapeFillColor;
        }

        public override PolygonShape AddToList(ShapeCreator cr)
        {
            return new PolygonShape(cr);
        }
        public override Shape? AddThisShape(ShapeCreator cr)
        {
            if (cr.shapePoints == null) return null;
            Points? points = PointsParse(cr.shapePoints);
            if (points == null) return null;

            return new Polygon
            {
                Name = cr.shapeName,
                Points = points,
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(cr.shapeFillColor))
            };
        }
        public override Shape AddThisShape()
        {
            Points points = PointsParse(this.Points);

            return new Polygon
            {
                Name = this.Name,
                Points = points,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(this.FillColor))
            };
        }
        public Points PointsParse(string str)
        {
            Points points = new Points();
            try
            {
                var str_points = str.Split(" ");
                foreach (var point in str_points)
                {
                    var str_point = point.Split(",");
                    var p1 = double.Parse(str_point[0]);
                    var p2 = double.Parse(str_point[1]);
                    points.Add(new Point(p1, p2));
                }
            }
            catch
            {
                return null;
            }
            return points;
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            main.ShapeName = this.Name;
            main.ShapePoints = this.Points;
            main.ShapeStrokeColor = this.StrokeColor;
            main.ShapeStrokeThickness = this.StrokeThickness;
            main.ShapeFillColor = this.FillColor;
        }
    }
}
