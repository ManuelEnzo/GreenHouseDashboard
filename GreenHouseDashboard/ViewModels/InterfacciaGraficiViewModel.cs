using DynamicData;
using GreenHouseDashboard.DTO.BaseEntity;
using GreenHouseDashboard.DTO.Misurazioni;
using GreenHouseDashboard.Interfaces;
using GreenHouseDashboard.Models;
using GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
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
    /// <summary>
    /// TODO : Valutare la praticità di caricare la collection delle misurazioni in fase di login - questo perchè così passo 
    /// la collection al viewmodel già pronta per i grafici evitando il bottone di caricamento
    /// </summary>
    public class InterfacciaGraficiViewModel : PageViewModelBase
    {
        private readonly ILoginService _loginService;

        public InterfacciaGraficiViewModel(ILoginService loginService)
        {
            _loginService = loginService;

            LoginInfoUser = CreateLoginInfo(_loginService.CurrentLogin.User);

            CaricaDatiCommand = new RelayCommand(async () => await OnLoadingDataAsync());
            MisurazioniCollection = new ObservableCollection<MisurazioniResponse>();
            var sectionsOuter = 130;
            var sectionsWidth = 20;

            Needle = OnNewNeedleVisual(45);

            Series = GaugeGenerator.BuildAngularGaugeSections(
                new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                new GaugeItem(10, s => SetStyle(sectionsOuter, sectionsWidth, s)));

            VisualElements = new VisualElement<SkiaSharpDrawingContext>[]
            {
            new AngularTicksVisual
            {
                LabelsSize = 16,
                LabelsOuterOffset = 15,
                OuterOffset = 65,
                TicksLength = 20
            },
            Needle
            };

        }
        public IEnumerable<ISeries> Series { get; set; }

        public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElements { get; set; }

        public NeedleVisual Needle { get; set; }
        
        public override bool CanNavigateNext { get => true; protected set => throw new NotImplementedException(); }
        public override bool CanNavigatePrevious { get => true; protected set => throw new NotImplementedException(); }

        private ObservableCollection<MisurazioniResponse> _misurazioniCollection;

        public ObservableCollection<MisurazioniResponse> MisurazioniCollection
        {
            get { return _misurazioniCollection; }
            set { this.RaiseAndSetIfChanged(ref _misurazioniCollection, value); }
        }

        private static void SetStyle(
            double sectionsOuter, double sectionsWidth, PieSeries<ObservableValue> series)
        {
            series.OuterRadiusOffset = sectionsOuter;
            series.MaxRadialColumnWidth = sectionsWidth;
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

                var lastC = MisurazioniCollection.LastOrDefault((x) => x.Misurazione.SensoreId == 1);

                Needle.Value = lastC.Misurazione.Valore;
             

            }
            catch (Exception e)
            {

                throw e.GetBaseException();
            }
        }

        #region ---------- LoginInfo
        public class LoginInfo
        {
            public Utente Utente { get; set; }
        }

        public LoginInfo CreateLoginInfo(Utente utente)
        {
            return new LoginInfo { Utente = utente };
        }
        #endregion

    }
}
