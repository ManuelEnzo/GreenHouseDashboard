using ReactiveUI;

namespace GreenHouseDashboard.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        #region ---------------------------------- Ctor
        public ViewModelBase()
        {
            IsBusy = true;
        }
        #endregion

        #region ---------------------------------- Property
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                this.RaisePropertyChanged(nameof(IsBusy));
            }
        }

        #endregion
    }
}