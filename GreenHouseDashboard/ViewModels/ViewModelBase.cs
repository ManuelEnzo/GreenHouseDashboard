using ReactiveUI;

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


        #endregion

        public static string IpService { get; set; } = "http://greenhouseme.ddns.net:5000";
    }
}