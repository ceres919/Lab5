using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public abstract class ShapeEntity
    {
        public string Name { get; set; }
        public string StrokeColor { get; set; }
        public int StrokeThickness { get; set; }

        //public ShapeEntity(string name, string strokeColor, int strokeThickness) 
        //{
        //    Name = name;
        //    StrokeColor = strokeColor;
        //    StrokeThickness = strokeThickness;
        //}
        public ShapeEntity() { }
        public abstract Shape AddThisShape(ShapeCreator creator);
        public abstract ShapeEntity AddToList(ShapeCreator cr);
    }
}
