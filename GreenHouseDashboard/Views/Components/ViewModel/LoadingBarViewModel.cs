using GreenHouseDashboard.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.Views.Components.ViewModel
{
    public class LoadingBarViewModel : ViewModelBase
    {

        #region ----------------------------Ctor
        public LoadingBarViewModel()
        {
            LoadSpinnerValuesAsync();
        }
        #endregion
     
        private async void LoadSpinnerValuesAsync()
        {
            IsBusy = true;
            MySpinnerValue = 20;

            await Task.Delay(2000);

            MySpinnerValue = 100;
            await Task.Delay(600);
            IsBusy = false;
        }


        #region ---------------------------- Property
        private int _mySpinnerValue;

        public int MySpinnerValue
        {
            get { return _mySpinnerValue; }
            set
            {
                if (_mySpinnerValue != value)
                {
                    _mySpinnerValue = value;
                    this.RaisePropertyChanged(nameof(MySpinnerValue));
                }
            }
        }
        #endregion

        #region ---------------------------- Esempio Task Asyncrono Property
        //public Task<int> MySpinnerValueAsync => GetTextAsync();


        //public async Task<int> GetTextAsync()
        //{
        //    try
        //    {
        //        await Task.Delay(1000); 
        //        return 100;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}
        #endregion
    }
}
