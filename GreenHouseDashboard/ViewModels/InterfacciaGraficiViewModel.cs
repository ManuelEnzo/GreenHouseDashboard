using DynamicData;
using GreenHouseDashboard.DTO.BaseEntity;
using GreenHouseDashboard.DTO.Misurazioni;
using GreenHouseDashboard.Interfaces;
using GreenHouseDashboard.Models;
using GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.ViewModels
{
    public class InterfacciaGraficiViewModel : PageViewModelBase
    {
        private readonly ILoginService _loginService;

        public InterfacciaGraficiViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            CaricaDatiCommand = new RelayCommand(async () => await OnLoadingDataAsync());
            MisurazioniCollection = new ObservableCollection<MisurazioniResponse>();
        }

        public override bool CanNavigateNext { get => true; protected set => throw new NotImplementedException(); }
        public override bool CanNavigatePrevious { get => true; protected set => throw new NotImplementedException(); }

        private ObservableCollection<MisurazioniResponse> _misurazioniCollection;

        public ObservableCollection<MisurazioniResponse> MisurazioniCollection
        {
            get { return _misurazioniCollection; }
            set { this.RaiseAndSetIfChanged(ref _misurazioniCollection, value); }
        }

        private RelayCommand _caricaDatiCommand;

        public RelayCommand CaricaDatiCommand
        {
            get
            {
                return _caricaDatiCommand;
            }
            set { _caricaDatiCommand = value; }
        }

        private LoginInfo _loginInfoUser;

        public LoginInfo LoginInfoUser
        {
            get { return _loginInfoUser; }
            set { this.RaiseAndSetIfChanged(ref _loginInfoUser, value); }
        }


        public async Task OnLoadingDataAsync()
        {
            try
            {
                var url = $"{IpService}/Misurazioni/GetMisurazioni";

                IRequestHttpService requestHttp = new HttpRequestService();
                var response = await requestHttp.SendRequestAsync<ObservableCollection<MisurazioniResponse>>(url, HttpMethod.Get, authToken: UseTokenSecurely(_loginService.CurrentLogin.JwtTokeBearer));

                if (response != null)
                {
                    MisurazioniCollection.AddRange(response);
                }

            }
            catch (Exception e)
            {

                throw e.GetBaseException();
            }
        }

        public class LoginInfo
        {
            public Utente Utente { get; set; }
        }

    }
}
