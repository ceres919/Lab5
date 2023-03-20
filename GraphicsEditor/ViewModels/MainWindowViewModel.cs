using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using GraphicsEditor.Models;
using GraphicsEditor.Models.Shapes;
using GraphicsEditor.Views;
using GraphicsEditor.Views.ShapesPages;
using JetBrains.Annotations;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace GraphicsEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl shapeContent;
        private int selectedShapeIndex = 0;
        public ShapesCollection list;
        public Canvas canv;
        public ShapeCreator creator;
        public ObservableCollection<UserControl> shapesPagesCollection = new() 
        { 
            new StraightLineShapePage(),
            new PolyLineShapePage(),
            new PolygonShapePage(),
            new RectangleShapePage(),
            new EllipsShapePage(),
            new PathShapePage()
        };
        public MainWindowViewModel(Window window) 
        {
            UserControl shape = shapesPagesCollection.ElementAt(SelectedShapeIndex);
            ShapeContent = shape;
            list = new ShapesCollection();
            canv = new Canvas();
            canv = window.Find<Canvas>("canvas");

            AddButton = ReactiveCommand.Create(() => {
                ShapeCreator creator = new ShapeCreator(this);
                Shape newShape = creator.Create(SelectedShapeIndex, list, canv);
                canv.Children.Add(newShape);
            });
            ClearButton = ReactiveCommand.Create(() => { Clear(); });
            DeleteButton = ReactiveCommand.Create<ShapeEntity>(DeleteShape);
        }

        public void Clear()
        {
            ShapeName = " ";
            ShapeStartPoint = null;
            ShapeEndPoint = null;
            ShapePoints = null;
            ShapeWidth = 0;
            ShapeHeight = 0;
            ShapeCommandPath = null;
            shapeStrokeThickness = 1;
        }
        public void DeleteShape(ShapeEntity item)
        {
            list.DeleteItem(item, canv);

        }

        public ShapeEntity SelectedEntity { get; set; }
        public ObservableCollection<string> colors = new()
        {
            "BlueViolet", "DeepSkyBlue", "Lavender", "AliceBlue", "Black"
        };
        
        public ObservableCollection<string> Colors { get => colors; }
        public ObservableCollection<ShapeEntity> ShapeList
        { 
            get => list.shapeList;
            set=> this.RaiseAndSetIfChanged(ref list.shapeList, value);
        }
        public int SelectedShapeIndex 
        { 
            get => selectedShapeIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedShapeIndex, value);
                Clear();
                ShapeContent = shapesPagesCollection.ElementAt(SelectedShapeIndex);
            } 
        }
        public UserControl ShapeContent
        {
            get => shapeContent;
            set
            {
                this.RaiseAndSetIfChanged(ref shapeContent, value);
            }
        }
        
        public string shapeName, shapeStartPoint, shapeEndPoint, shapePoints, shapeStrokeColor, shapeFillColor, shapeCommandPath;
        public int shapeHeight, shapeWidth, shapeStrokeThickness = 1;
        public string? ShapeName { get => shapeName; set=>this.RaiseAndSetIfChanged(ref shapeName, value); }
        public string? ShapeStartPoint { get => shapeStartPoint; set => this.RaiseAndSetIfChanged(ref shapeStartPoint, value); }
        public string? ShapeEndPoint { get => shapeEndPoint; set => this.RaiseAndSetIfChanged(ref shapeEndPoint, value); }
        public string? ShapePoints { get => shapePoints; set => this.RaiseAndSetIfChanged(ref shapePoints, value); }
        public int ShapeWidth { get => shapeWidth; set => this.RaiseAndSetIfChanged(ref shapeWidth, value); }
        public int ShapeHeight { get => shapeHeight; set => this.RaiseAndSetIfChanged(ref shapeHeight, value); }
        public string ShapeStrokeColor { get => shapeStrokeColor; set => this.RaiseAndSetIfChanged(ref shapeStrokeColor, value); }
        public string ShapeFillColor { get => shapeFillColor; set => this.RaiseAndSetIfChanged(ref shapeFillColor, value); }
        public int ShapeStrokeThickness { get => shapeStrokeThickness; set => this.RaiseAndSetIfChanged(ref shapeStrokeThickness, value); }
        public string? ShapeCommandPath { get => shapeCommandPath; set => this.RaiseAndSetIfChanged(ref shapeCommandPath, value); }
        public ReactiveCommand<Unit, Unit> AddButton { get; }
        public ReactiveCommand<Unit, Unit> ClearButton { get; }
        public ReactiveCommand<ShapeEntity, Unit> DeleteButton { get; }



    }
}