using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GreenHouseDashboard.DI;
using GreenHouseDashboard.Interfaces;
using GreenHouseDashboard.ViewModels;
using GreenHouseDashboard.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace GreenHouseDashboard
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            // Configura i servizi all'interno del metodo Initialize
            var services = new ServiceCollection();
            services.AddSingleton<ILoginService, LoginService>();

            var stateService = new LoginService();  // Crea un'istanza del tuo servizio
            ServiceContainer.RegisterService<ILoginService>(stateService);

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            ILoginService loginService = new LoginService();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(loginService),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}