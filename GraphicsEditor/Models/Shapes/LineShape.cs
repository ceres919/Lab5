using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace GraphicsEditor.Models.Shapes
{
    
    public class LineShape : ShapeEntity
    {
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        [JsonConstructor]
        public LineShape() { }
        public LineShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness)
        {
            StartPoint = cr.shapeStartPoint;
            EndPoint = cr.shapeEndPoint;
        }

        public override LineShape AddToList(ShapeCreator cr)
        {
            return new LineShape(cr);
        }
        public override Shape? AddThisShape(ShapeCreator cr)
        {
            if (cr.shapeEndPoint == null || cr.shapeEndPoint == null) return null;
            var startPoint = PointsParse(cr.shapeStartPoint);
            var endPoint = PointsParse(cr.shapeEndPoint);
            if (startPoint == null || endPoint == null) return null;

            return new Line
            {
                Name = cr.shapeName,
                StartPoint = new Point(startPoint[0], startPoint[1]),
                EndPoint = new Point(endPoint[0], endPoint[1]),
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness
            };
        }
        public override Shape AddThisShape()
        {
            var startPoint = PointsParse(this.StartPoint);
            var endPoint = PointsParse(this.EndPoint);

            return new Line
            {
                Name = this.Name,
                StartPoint = new Point(startPoint[0], startPoint[1]),
                EndPoint = new Point(endPoint[0], endPoint[1]),
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness
            };
        }
        public double[] PointsParse(string str)
        {
            double[] point = { 0, 0 };
            try
            {
                var str_point = str.Split(",");
                point[0] = double.Parse(str_point[0]);
                point[1] = double.Parse(str_point[1]);

            }
            catch
            {
                return null;
            }
            return point;
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            main.ShapeName = this.Name;
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeEndPoint = this.EndPoint;
            main.ShapeStrokeColor = this.StrokeColor;
            main.ShapeStrokeThickness = this.StrokeThickness;
        }
    }
}
