using Avalonia;
using Avalonia.Threading;
using GreenHouseDashboard.Models;
using GreenHouseDashboard.Views;
using ReactiveUI;
using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;
using ImageHelper = GreenHouseDashboard.Models.ImageHelper;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Threading.Tasks;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Reactive;
using GreenHouseDashboard.DTO.Misurazioni;
using GreenHouseDashboard.DTO.Login;
using GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces;
using System.Diagnostics;
using System.Net.Http;
using GreenHouseDashboard.Interfaces;

namespace GreenHouseDashboard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IAsyncInitialization
    {

        /// <summary>
        /// All'Load call API asincrone
        /// </summary>
        public MainWindowViewModel()
        {
            IsBusy = true;
            Initialization =  OnVMLoadAsync();
            IsBusy = false;
        }

        public Task Initialization { get; private set; }


        #region --------------------- Property
        private ObservableCollection<double> _listItemTemperatura;

        public ObservableCollection<double> ListItemTemperatura
        {
            get { return _listItemTemperatura; }
            set { this.RaiseAndSetIfChanged(ref _listItemTemperatura, value); }
        }


        #endregion

        #region --------------------- ManageVM
        public async Task OnVMLoadAsync()
        {
            try
            {

                //Centralizzo il metodo inserendo uno switch case sui sensori
                ListItemTemperatura = new ObservableCollection<double>();
                var x = await CallAPISensorAsync();





            }
            catch (Exception)
            {

                throw new Exception("Errore in fase di caricamento Dati Sensori");
            }
        }

        private async Task<MisurazioniResponse> CallAPISensorAsync()
        {
            try
            {
                

                var url = $"{IpService}/Misurazioni/GetMisurazioni";


                IRequestHttpService requestHttp = new HttpRequestService();
                var response = await requestHttp.SendRequestAsync<ObservableCollection<MisurazioniResponse>>(url, HttpMethod.Get, authToken: JwtToken);

                if (response != null)
                {
                    await Task.Delay(2000);
                }
                else
                {
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion






    }
}
