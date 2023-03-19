using Avalonia.Controls.Shapes;
using GraphicsEditor.Models.Shapes;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class ShapeCreator
    {
        public readonly ObservableCollection<ShapeEntity> shapes = new()
        {
            new LineShape(),
            new PolyLineShape()

        };
        public string shapeName;
        public string? shapeStartPoint;
        public string? shapeEndPoint;
        public string? shapePoints;
        public int? shapeWidth;
        public int? shapeHeight;
        public string shapeStrokeColor;
        public string? shapeFillColor;
        public int shapeStrokeThickness;
        public string? shapeCommandPath;
        
        public ShapeCreator(MainWindowViewModel main) 
        {
            shapeName = main.ShapeName;
            shapeStartPoint = main.ShapeStartPoint;
            shapeEndPoint = main.ShapeEndPoint;
            shapePoints = main.ShapePoints;
            shapeWidth = main.ShapeWidth;
            shapeHeight = main.ShapeHeight;
            shapeStrokeColor = main.ShapeStrokeColor;
            shapeFillColor = main.ShapeFillColor;
            shapeStrokeThickness = main.ShapeStrokeThickness;
            shapeCommandPath = main.ShapeCommandPath;
        }
        public Shape Create(int index, ShapesCollection list) 
        {
            Shape newShape = shapes.ElementAt(index).AddThisShape(this);
            ShapeEntity newItem = shapes.ElementAt(index).AddToList(this);
            list.AddItem(newItem);
            return newShape;
        }
        //public ShapeCreator() { }
    }
}
