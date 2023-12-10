using Avalonia;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GreenHouseDashboard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            NowTime = DateTime.Now.ToString();
            Task.Run(() => GetCurrent());
        }


        #region CurrentTime
        public void GetCurrent()
        {
            while (true)
            {
                Dispatcher.UIThread.Invoke(new Action(() =>
                {
                    TimeNowCurrent = DateTime.Now;
                    NowTime = TimeNowCurrent.ToString();
                }));
            }
        }
        #endregion


        public string Greeting => "Benvenuto nella GreenHouseDashboard";


        #region Property

        private DateTime timeNowCurrent;

        public DateTime TimeNowCurrent
        {
            get { return timeNowCurrent; }
            set
            {
                this.RaiseAndSetIfChanged(ref timeNowCurrent, value);
            }
        }

        private string nowTime;

        public string NowTime
        {
            get { return nowTime; }
            set { this.RaiseAndSetIfChanged(ref nowTime, value); }
        }


        #endregion
    }
}