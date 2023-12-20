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
                _isBusy = value;
                this.RaiseAndSetIfChanged(ref _isBusy, value);
            }
        }

        #endregion
    }
}