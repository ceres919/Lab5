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
    public class EllipseShape : ShapeEntity
    {
        public string StartPoint { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string FillColor { get; set; }
        public EllipseShape() { }
        public EllipseShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness) 
        {
            StartPoint = cr.shapeStartPoint;
            Width = cr.shapeWidth;
            Height = cr.shapeHeight;
            FillColor = cr.shapeFillColor;
        }

        public override EllipseShape AddToList(ShapeCreator cr)
        {
            return new EllipseShape(cr);
        }
        public override Shape? AddThisShape(ShapeCreator cr)
        {
            if (cr.shapeStartPoint == null || cr.shapeWidth == 0 || cr.shapeHeight == 0) return null;
            var startPoint = PointsParse(cr.shapeStartPoint);
            if (startPoint == null) return null;

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
        public override Shape AddThisShape()
        {
            var startPoint = PointsParse(this.StartPoint);
            
            return new Ellipse
            {
                Name = this.Name,
                Margin = new(startPoint[0], startPoint[1], 0, 0),
                Height = this.Height,
                Width = this.Width,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(this.FillColor))
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
            main.ShapeHeight = this.Height;
            main.ShapeWidth = this.Width;
            main.ShapeStrokeColor = this.StrokeColor;
            main.ShapeStrokeThickness = this.StrokeThickness;
            main.ShapeFillColor = this.FillColor;
        }
    }
}
