using Avalonia.Interactivity;
using ReactiveUI;
using System;

namespace GreenHouseDashboard.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        #region ---------------------------------- Ctor
        public ViewModelBase()
        {
            
        }
        #endregion

        #region ---------------------------------- Property
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isBusy, value);
            }
        }
        public static string IpService { get; set; } = "http://greenhouseme.ddns.net:5000";
        public static string JwtToken { get; set; }
        #endregion

    }
}