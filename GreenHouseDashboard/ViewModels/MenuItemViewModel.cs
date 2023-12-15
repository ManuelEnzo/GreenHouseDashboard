using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenHouseDashboard.ViewModels
{
    public class MenuItemViewModel : ViewModelBase
    {
        public string Name { get; }
        public ICommand Command { get; }
        public Bitmap IconSource { get; }

        public MenuItemViewModel(string name, ICommand command, Bitmap iconSource)
        {
            Name = name;
            Command = command;
            IconSource = iconSource;
        }
    }
}
