using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GreenHouseDashboard.ViewModels;
using GreenHouseDashboard.Views;

namespace GreenHouseDashboard
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //desktop.MainWindow = new MainWindow
                //{
                //    DataContext = new MainWindowViewModel(),
                //};
                desktop.MainWindow = new LoginView
                {
                    DataContext = new LoginViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}