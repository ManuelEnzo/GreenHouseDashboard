using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GreenHouseDashboard.ViewModels.ViewModelBase;

namespace GreenHouseDashboard.Interfaces
{
    public interface ILoginService : INotifyPropertyChanged
    {
        public CurrentLoginProfile CurrentLogin { get; set; }
    }
    public class LoginService : ILoginService
    {
        private CurrentLoginProfile _currentLogin;

        public CurrentLoginProfile CurrentLogin
        {
            get => _currentLogin;
            set
            {
                if (_currentLogin == null)
                {
                    if (_currentLogin != value)
                    {
                        _currentLogin = value;
                        OnPropertyChanged(nameof(CurrentLogin));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
