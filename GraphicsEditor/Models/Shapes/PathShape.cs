using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class PathShape : ShapeEntity 
    {
        public string FillColor { get; set; }
        public string CommandPath { get; set; }
        public PathShape() { }
        public PathShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness)
        {
            FillColor = cr.shapeFillColor;
            CommandPath = cr.shapeCommandPath;
        }

        public override PathShape AddToList(ShapeCreator cr)
        {
            return new PathShape(cr);
        }
        public override Shape? AddThisShape(ShapeCreator cr)
        {
            if (cr.shapeCommandPath == null) return null;
            Geometry gem;
            try
            {
                gem = Geometry.Parse(cr.shapeCommandPath);
            }
            catch
            {
                return null;
            }
            if (gem == null) return null;

            return new Path
            {
                Name = cr.shapeName,
                Data = gem,
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(cr.shapeFillColor))
            };
        }
        public override Shape AddThisShape()
        {
            Geometry gem = Geometry.Parse(this.CommandPath);

            return new Path
            {
                Name = this.Name,
                Data = gem,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(this.FillColor))
            };
        }
    }
}
