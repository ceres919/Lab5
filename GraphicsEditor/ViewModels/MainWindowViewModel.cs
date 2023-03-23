using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using GraphicsEditor.Models;
using GraphicsEditor.Models.LoadAndSave;
using GraphicsEditor.Models.Shapes;
using GraphicsEditor.Views;
using GraphicsEditor.Views.ShapesPages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;

namespace GraphicsEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindow mainWindow;
        private UserControl shapeContent;
        public ShapesCollection list;
        public Canvas canv;
        public ShapeCreator creator;
        public IEnumerable<ISaverLoaderFactory> SaverLoaderFactoryCollection { get; set; }
        public ShapeEntity selectedEntity;
        private int selectedShapeIndex = 0;

        public static ObservableCollection<string> colors = new()
        {
            "BlueViolet", "DeepSkyBlue", "Lavender", "AliceBlue", "Black"
        };
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
            mainWindow = (MainWindow)window;
            AddButton = ReactiveCommand.Create(() => { AddShape(); });
            ClearButton = ReactiveCommand.Create(() => { Clear(); });
            DeleteButton = ReactiveCommand.Create<ShapeEntity>(DeleteShape);
            ImportButton = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.OpenFileDialogMenu(param);
            });
            ExportButton = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.SaveFileDialogMenu(param);
            });
        }

        public void LoadShapes(string path)
        {
            list.shapeList.Clear();
            list.shapesCollection.Clear();
            canv.Children.Clear();

            var shapeLoader = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateLoader();

            if (shapeLoader != null)
            {
                var newList = new ObservableCollection<ShapeEntity>(shapeLoader.Load(path));
                ShapeCreator creator = new ShapeCreator(this);
                foreach (var shape in newList)
                {
                    creator.Load(shape, list, canv);
                }
            }
        }
        public void SaveShapes(string path, string parametr)
        {
            if (parametr != "png")
            {
                var shapeSaver = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateSaver();

                if (shapeSaver != null)
                {
                    shapeSaver.Save(ShapeList, path);
                }
            }
            else
            {
                var shapeSaver = new PNGSaver();
                shapeSaver.Save(canv, path);
            }
        }

        public void AddShape()
        {
            ShapeCreator creator = new ShapeCreator(this);
            creator.Create(SelectedShapeIndex, list, canv);
        }
        public void DeleteShape(ShapeEntity item)
        {
            list.DeleteItem(item, canv);
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

        public ShapeEntity SelectedEntity 
        { 
            get =>selectedEntity;
            set 
            {
                this.RaiseAndSetIfChanged(ref selectedEntity, value); 
            }
        }
        public string OpenFileName { get; set; }
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

        public string shapeName, shapeStartPoint, shapeEndPoint, shapePoints, shapeCommandPath, shapeStrokeColor = colors.ElementAt(0), shapeFillColor = colors.ElementAt(0);
        public int shapeHeight, shapeWidth, shapeStrokeThickness = 1;
        public string ShapeName { get => shapeName; set=>this.RaiseAndSetIfChanged(ref shapeName, value); }
        public string? ShapeStartPoint { get => shapeStartPoint; set => this.RaiseAndSetIfChanged(ref shapeStartPoint, value); }
        public string? ShapeEndPoint { get => shapeEndPoint; set => this.RaiseAndSetIfChanged(ref shapeEndPoint, value); }
        public string? ShapePoints { get => shapePoints; set => this.RaiseAndSetIfChanged(ref shapePoints, value); }
        public int ShapeWidth { get => shapeWidth; set => this.RaiseAndSetIfChanged(ref shapeWidth, value); }
        public int ShapeHeight { get => shapeHeight; set => this.RaiseAndSetIfChanged(ref shapeHeight, value); }
        public string ShapeStrokeColor { get => shapeStrokeColor; set => this.RaiseAndSetIfChanged(ref shapeStrokeColor, value); }
        public string ShapeFillColor { get => shapeFillColor; set => this.RaiseAndSetIfChanged(ref shapeFillColor, value); }
        public int ShapeStrokeThickness { get => shapeStrokeThickness; set => this.RaiseAndSetIfChanged(ref shapeStrokeThickness, value); }
        public string? ShapeCommandPath { get => shapeCommandPath; set => this.RaiseAndSetIfChanged(ref shapeCommandPath, value); }
        public ReactiveCommand<string, Unit> ImportButton { get; }
        public ReactiveCommand<string, Unit> ExportButton { get; }

        public ReactiveCommand<Unit, Unit> AddButton { get; }
        public ReactiveCommand<Unit, Unit> ClearButton { get; }
        public ReactiveCommand<ShapeEntity, Unit> DeleteButton { get; }
    }
}