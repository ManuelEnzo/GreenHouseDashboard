using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GreenHouseDashboard.DI;
using GreenHouseDashboard.Interfaces;
using GreenHouseDashboard.ViewModels;
using GreenHouseDashboard.Views;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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

            var loginService = new LoginService();  // Crea un'istanza del tuo servizio
            var misurService = new MisurazioniService();
            ServiceContainer.RegisterService<ILoginService>(loginService);
            ServiceContainer.RegisterService<IMisurazioniService>(misurService);

            AvaloniaXamlLoader.Load(this);
            LiveCharts.Configure(config =>
            config.AddDarkTheme());
        }

        public override void OnFrameworkInitializationCompleted()
        {
            ILoginService loginService = new LoginService();
            IMisurazioniService misurazioniService = new MisurazioniService();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(loginService, misurazioniService),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}