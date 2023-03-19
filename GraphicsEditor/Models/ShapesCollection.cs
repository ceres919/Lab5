using GraphicsEditor.Models.Shapes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class ShapesCollection
    {
        public ObservableCollection<ShapeEntity> shapeList = new ObservableCollection<ShapeEntity>();
        
        public ShapesCollection() 
        {
            Delete = ReactiveCommand.Create<ShapeEntity>(DeleteItem);
        }
        public void AddItem(ShapeEntity item)
        {
            shapeList.Add(item);
        }
        public void DeleteItem(ShapeEntity item)
        {
            shapeList.Remove(item);
        }
        public ReactiveCommand<ShapeEntity, Unit> Delete { get; }
    }
}
