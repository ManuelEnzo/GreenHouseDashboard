using Avalonia;
using Avalonia.Threading;
using GreenHouseDashboard.Models;
using GreenHouseDashboard.Views;
using ReactiveUI;
using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;
using ImageHelper = GreenHouseDashboard.Models.ImageHelper;

namespace GreenHouseDashboard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly DispatcherTimer timer;

        public MainWindowViewModel()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            StartTimerDispatcher(ref timer);

            MenuItems.Add(AddItemsIntoMenu("Dashboard", new Settings(), "greenhouse34.png"));

        }

        #region --------------------- Property

        private string nowTime;

        public string NowTime
        {
            get { return nowTime; }
            set { this.RaiseAndSetIfChanged(ref nowTime, value); }
        }

        public string Greeting => "Benvenuto nella GreenHouseDashboard";

        public ObservableCollection<MenuItemViewModel> MenuItems { get; } = new ObservableCollection<MenuItemViewModel>();

        private Control _selectedMenuContent;

        public Control SelectedMenuContent
        {
            get { return _selectedMenuContent; }
            set => this.RaiseAndSetIfChanged(ref _selectedMenuContent, value);
        }

        #endregion

        #region --------------------- ManageVM
        private void ShowMenuContent(Control content)
        {
            SelectedMenuContent = content;
        }

        private void StartTimerDispatcher(ref DispatcherTimer timer)
        {
            timer.Tick += (sender, args) => NowTime = DateTime.Now.ToString("T");
            timer.Start();
        }

        #endregion

        #region --------------------- Gestione Menù
        /// <summary>
        /// Centralizzato Add Item nel menù della dashboard
        /// </summary>
        /// <param name="name">Nome della voce aggiunta</param>
        /// <param name="control"><see cref="Control"/></param>
        /// <param name="iconResources">Path Icona</param>
        /// <returns></returns>
        private MenuItemViewModel AddItemsIntoMenu(string name, Control control, string iconResources)
        {
            return new MenuItemViewModel(name, new RelayCommand(() => ShowMenuContent(control)), ImageHelper.LoadFromResource(new(string.Concat("avares://GreenHouseDashboard/Assets/",iconResources))));
        }
        #endregion


    }
}
