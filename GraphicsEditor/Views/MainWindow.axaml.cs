using Avalonia.Controls;
using Avalonia.Interactivity;
using GraphicsEditor.Models.LoadAndSave;
using GraphicsEditor.ViewModels;
using ReactiveUI;
using System.Linq;
using System.Reactive;

namespace GraphicsEditor.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this)
            {
                SaverLoaderFactoryCollection = new ISaverLoaderFactory[]
                    {
                        new XMLSaverLoaderFactory(),
                        new JSONSaverLoaderFactory(),
                    },
            };
        }

        public async void OpenFileDialogMenu(string parametr)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            switch (parametr)
            {
                //case "png":
                //    openFileDialog.Filters.Add(
                //        new FileDialogFilter
                //        {
                //            Name = "PNG files",
                //            Extensions = new string[] { "png" }.ToList()
                //        });
                //    break;
                case "xml":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "XML files",
                            Extensions = new string[] { "xml" }.ToList()
                        });
                    break;
                case "json":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
                    break;
            }
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    dataContext.LoadShapes(result[0]);
                }
            }
        }

        public async void SaveFileDialogMenu(string parametr)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            switch (parametr)
            {
                case "png":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "PNG files",
                            Extensions = new string[] { "png" }.ToList()
                        });
                    break;
                case "xml":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "XML files",
                            Extensions = new string[] { "xml" }.ToList()
                        });
                    break;
                case "json":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
                    break;
            }
            string? result = await saveFileDialog.ShowAsync(this);

            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    dataContext.SaveShapes(result, parametr);
                }
            }
        }
    }
}