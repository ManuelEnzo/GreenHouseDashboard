using Avalonia.Interactivity;
using GreenHouseDashboard.DTO.BaseEntity;
using GreenHouseDashboard.DTO.Login;
using ReactiveUI;
using System;
using System.Diagnostics;
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

        #endregion

        /// <summary>
        /// Metodo per utilizzare il JwtToken
        /// </summary>
        /// <param name="jwtToken">Passato come SecureString</param>
        /// <returns>Il JwtToken convertito in stringa utilizzabile</returns>
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

        /// <summary>
        /// Restituisce un nuovo ogetto <see cref="CurrentLoginProfile"/>
        /// </summary>
        /// <param name="user"><see cref="Utente"/></param>
        /// <param name="jwt">JwtToken <see cref="SecureString"/></param>
        /// <param name="logIn">true</param>
        /// <returns>new <see cref="CurrentLoginProfile"/></returns>
        public CurrentLoginProfile CreateLoginProfile(Utente user, string jwt, bool logIn)
        {
            try
            {
                CurrentLoginProfile currentLoginProfile = new CurrentLoginProfile
                {
                    User = user,
                    IsLoggedIn = logIn,
                    JwtTokeBearer = StoreTokenSecurely(jwt),

                };
                return currentLoginProfile;
            }
            catch (Exception e)
            {

                throw e.GetBaseException();
            }
        }

        /// <summary>
        /// Memorizza sottoforma di <see cref="SecureString"/> il JwtToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns>new <see cref="SecureString"/></returns>
        public SecureString StoreTokenSecurely(string token)
        {
            try
            {
                var jwtToken = new SecureString();
                foreach (char c in token)
                {
                    jwtToken.AppendChar(c);
                }
                return jwtToken;

            }
            catch (Exception e)
            {

                throw e.GetBaseException();
            }
        }

        /// <summary>
        /// Utente Loggato
        /// </summary>
        public class CurrentLoginProfile
        {
            public Utente User { get; set; }
            public SecureString JwtTokeBearer { get; set; }
            public bool IsLoggedIn { get; set; }
            
        }



    }
}