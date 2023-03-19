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

        public PathShape(string name, string strokeColor, int strokeThickness) : base(name, strokeColor, strokeThickness)
        {

        }
        public override PathShape AddToList(ShapeCreator cr)
        {
            Name = cr.shapeName;
            StrokeColor = cr.shapeStrokeColor;
            StrokeThickness = cr.shapeStrokeThickness;
            return this;
        }
        public override Shape AddThisShape(ShapeCreator cr)
        {
            //var stpoint = cr.shapeStartPoint.Split(',');
            //var endpoint = cr.shapeEndPoint.Split(",");
            //startPoint[0] = int.Parse(stpoint[0]);
            //startPoint[1] = int.Parse(stpoint[1]);

            return new Line
            {
                Name = cr.shapeName,
                //StartPoint = new Point(startPoint[0], startPoint[1]),
                Stroke = new SolidColorBrush(Color.Parse(cr.shapeStrokeColor)),
                StrokeThickness = cr.shapeStrokeThickness,
            };
        }
    }
}
