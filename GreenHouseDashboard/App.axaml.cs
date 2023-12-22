using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GreenHouseDashboard.ViewModels;
using GreenHouseDashboard.Views;
using System.Threading.Tasks;

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
                var loginView = new LoginView();
                var loginVM = new LoginViewModel();
                loginView.DataContext = loginVM;

                desktop.MainWindow = loginView; desktop.MainWindow.Width = 400; desktop.MainWindow.Height = 500;

                loginVM.LoginCompleted += (sender, args) =>
                {
                    var mainWindow = new MainWindow();
                    var mainVM = new MainWindowViewModel();
                    OnStartUpMainWindow(mainWindow, mainVM, loginView, desktop);
                };

                loginView.Show();
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Centralizzata la possibilità di effettuare un close della View
        /// </summary>
        /// <param name="mainWindow"><see cref="MainWindow"/></param>
        /// <param name="mainWindowViewModel"><see cref="MainWindowViewModel"/></param>
        /// <param name="windowToClose"><see cref="Window"/> Form da Chiudere</param>
        /// <param name="desktop"><see cref="IClassicDesktopStyleApplicationLifetime"/></param>
        private void OnStartUpMainWindow(MainWindow mainWindow, MainWindowViewModel mainWindowViewModel, Window windowToClose, IClassicDesktopStyleApplicationLifetime desktop)
        {
            try
            {
                // Mostra la MainWindow solo dopo il login effettuato

                mainWindow.DataContext = mainWindowViewModel;
                desktop.MainWindow = mainWindow;
                mainWindow.Show();

                // Chiudi la LoginView dopo il login effettuato
                windowToClose.Close();
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}