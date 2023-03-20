using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class PathShape : ShapeEntity 
    {
        public string FillColor { get; set; }
        public string CommandPath { get; set; }

        public override PathShape AddToList(ShapeCreator cr)
        {
            Name = cr.shapeName;
            CommandPath = cr.shapeCommandPath;
            StrokeColor = cr.shapeStrokeColor;
            StrokeThickness = cr.shapeStrokeThickness;
            FillColor = cr.shapeFillColor;
            return this;
        }
        public override Shape AddThisShape(ShapeCreator cr)
        {
            Geometry gem = Geometry.Parse(cr.shapeCommandPath);
            return new Path
            {
                Name = cr.shapeName,
                Data = gem,
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(cr.shapeFillColor))
            };
        }
    }
}
