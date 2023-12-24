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

namespace GreenHouseDashboard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        /// <summary>
        /// All'Load call API asincrone
        /// </summary>
        public MainWindowViewModel()
        {
            _ = Task.Run(async () =>
            {
                await OnVMLoadAsync();
            });
        }




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

                ListItemTemperatura = InitializedObservabile();



            }
            catch (Exception)
            {

                throw new Exception("Errore in fase di caricamento Dati Sensori");
            }
        }

        private ObservableCollection<double> InitializedObservabile()
        {
            return new ObservableCollection<double>();
        }

        //private async Task<MisurazioniResponse> CallAPISensorAsync(int sensoreId)
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        var url = $"{IpService}/Misurazioni/GetMisurazioniBySensore/1?idSensore={sensoreId}";


        //        IRequestHttpService requestHttp = new HttpRequestService();
        //        var response = await requestHttp.SendRequestAsync<MisurazioniResponse>(url, HttpMethod.Get);

        //        if (response != null)
        //        {
        //            Debug.WriteLine($"Utente Loggato con successo : ------ {response} ------- ");
        //            await Task.Delay(2000);
        //        }
        //        else
        //        {
        //            Debug.WriteLine($"Utente non loggato : ------ {response} ------- ");
        //            await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        //            {
        //                ContentTitle = "Attenzione !",
        //                ContentMessage = "Password/Username non corretta. Ritenta per accedere !",
        //                Icon = Icon.Warning,
        //                ButtonDefinitions = ButtonEnum.Ok
        //            }).ShowWindowAsync();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion






    }
}
