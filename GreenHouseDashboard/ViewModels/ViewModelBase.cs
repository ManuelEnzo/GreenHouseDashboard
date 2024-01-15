using Avalonia.Interactivity;
using GreenHouseDashboard.DTO.BaseEntity;
using GreenHouseDashboard.DTO.Login;
using ReactiveUI;
using System;
using System.Diagnostics;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using DynamicData;
using System.ComponentModel;
using Tmds.DBus.Protocol;

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

        #region ----------------------- SkiaSharp

        public NeedleVisual OnNewNeedleVisual(int value)
        {
            return new NeedleVisual { Value = value };
        }

        public void OnInitializeISeries(ref IEnumerable<ISeries> Series, List<double> values, int sectionsOuter, int sectionsWidth)
        {
            try
            {
                if (Series == null) { return; }
                if (values == null) { return; }
                if (values.Count == 0) { return; }

                GaugeItem[] i = new GaugeItem[values.Count];

                //TODO : Capire come ciclare  i[0]
                int index = 0;
                foreach(var item in values)
                {
                    i[index] = new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s));
                    index += 1;
                }
                //values.ForEach((x) => i[0] = new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s)));

                
                Series = GaugeGenerator.BuildAngularGaugeSections(i);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void SetStyle(
           double sectionsOuter, double sectionsWidth, PieSeries<ObservableValue> series)
        {
            series.OuterRadiusOffset = sectionsOuter;
            series.MaxRadialColumnWidth = sectionsWidth;
        }

        #endregion





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