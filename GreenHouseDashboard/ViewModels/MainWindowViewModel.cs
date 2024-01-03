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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reactive.Concurrency;
using System.Threading;

namespace GreenHouseDashboard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IAsyncInitialization
    {

        /// <summary>
        /// All'Load call API asincrone
        /// </summary>
        public MainWindowViewModel()
        {
          
            new Action(async () =>
            {
                IsBusy = true;
                var Data = await OnVMLoadAsync();
                IsBusy = false;
            }).Invoke();
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
        public async Task<ObservableCollection<MisurazioniResponse>> OnVMLoadAsync()
        {
            try
            {
                return await CallAPISensorAsync();
            }
            catch (Exception e)
            {

                throw new Exception("Errore in fase di caricamento Dati Sensori", e.GetBaseException());
            }
        }

        private async Task<ObservableCollection<MisurazioniResponse>> CallAPISensorAsync()
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
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion






    }
}
