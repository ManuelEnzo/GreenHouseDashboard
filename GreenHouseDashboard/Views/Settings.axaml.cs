using Avalonia.Controls;
using GreenHouseDashboard.ViewModels;

namespace GreenHouseDashboard.Views
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();

            var VM = new SettingsViewModel();
            this.DataContext = VM;
        }
    }
}
