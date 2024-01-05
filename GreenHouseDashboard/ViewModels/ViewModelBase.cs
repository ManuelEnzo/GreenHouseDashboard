using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Security;

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
        public static SecureString JwtToken { get; set; }

        private bool _isVisibleNextBack;

        public bool IsVisibleNextBack
        {
            get { return _isVisibleNextBack; }
            set { this.RaiseAndSetIfChanged(ref _isVisibleNextBack, value); }
        }
        #endregion

        public static string UseTokenSecurely(SecureString jwtToken)
        {
            // Esempio di utilizzo del token da SecureString
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(jwtToken);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }

    }
}