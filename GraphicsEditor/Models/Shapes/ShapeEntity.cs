using Avalonia.Controls.Shapes;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GraphicsEditor.ViewModels;

namespace GraphicsEditor.Models.Shapes
{
    [XmlInclude(typeof(LineShape))]
    [XmlInclude(typeof(PolyLineShape))]
    [XmlInclude(typeof(PolygonShape))]
    [XmlInclude(typeof(RectangleShape))]
    [XmlInclude(typeof(EllipseShape))]
    [XmlInclude(typeof(PathShape))]
    public abstract class ShapeEntity
    {
        public string Name { get; set; }
        public string StrokeColor { get; set; }
        public int StrokeThickness { get; set; }
        public ShapeEntity() { }
        public ShapeEntity(string name, string strokeColor, int strokeThickness)
        {
            Name = name;
            StrokeColor = strokeColor;
            StrokeThickness = strokeThickness;
        }

        public abstract Shape? AddThisShape(ShapeCreator creator);
        public abstract Shape AddThisShape();
        public abstract ShapeEntity AddToList(ShapeCreator cr);
        public abstract void SetPropertiesOfCurrentShape(MainWindowViewModel main);
    }
}
