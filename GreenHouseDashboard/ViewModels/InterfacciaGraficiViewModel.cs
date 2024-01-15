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
        private readonly IMisurazioniService _misurazioniService;

        public InterfacciaGraficiViewModel(ILoginService loginService, IMisurazioniService misurazioniService)
        {
            _loginService = loginService;
            _misurazioniService = misurazioniService;

            LoginInfoUser = CreateLoginInfo(_loginService.CurrentLogin.User);

            MisurazioniCollection = new ObservableCollection<MisurazioniResponse>();
            var sectionsOuter = 130;
            var sectionsWidth = 20;

            NeedleTemp = OnNewNeedleVisual(45);
            NeedleHum = OnNewNeedleVisual(45);

            IEnumerable<ISeries> sTemp = new ObservableCollection<ISeries>();
            IEnumerable<ISeries> sHum = new ObservableCollection<ISeries>();
            OnInitializeISeries(ref sTemp, new List<double> { 60, 30, 10 }, sectionsOuter, sectionsWidth);
            OnInitializeISeries(ref sHum, new List<double> { 60, 30, 10 }, sectionsOuter, sectionsWidth);

            SeriesTemp = sTemp;
            SeriesHum = sHum;

            VisualElementsTemp = new VisualElement<SkiaSharpDrawingContext>[]
            {
                new AngularTicksVisual
                {
                    LabelsSize = 16,
                    LabelsOuterOffset = 15,
                    OuterOffset = 65,
                    TicksLength = 20
                },
                NeedleTemp
            };

            VisualElementsHum = new VisualElement<SkiaSharpDrawingContext>[]
            {
                new AngularTicksVisual
                {
                    LabelsSize = 16,
                    LabelsOuterOffset = 15,
                    OuterOffset = 65,
                    TicksLength = 20
                },
                NeedleHum
            };

            _ = OnInitializeAsync();

        }
        public IEnumerable<ISeries> SeriesTemp { get; set; }
        public IEnumerable<ISeries> SeriesHum { get; set; }

        public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElementsTemp { get; set; }
        public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElementsHum { get; set; }

        public NeedleVisual NeedleTemp { get; set; }
        public NeedleVisual NeedleHum { get; set; }

        #region ----------- Navigate
        public override bool CanNavigateNext { get => true; protected set => throw new NotImplementedException(); }
        public override bool CanNavigatePrevious { get => true; protected set => throw new NotImplementedException(); }

        private ObservableCollection<MisurazioniResponse> _misurazioniCollection;
        #endregion

        public ObservableCollection<MisurazioniResponse> MisurazioniCollection
        {
            get { return _misurazioniCollection; }
            set { this.RaiseAndSetIfChanged(ref _misurazioniCollection, value); }
        }

        public async Task OnInitializeAsync()
        {
            IsBusy = true;

            var m = await _misurazioniService.GetMisurazioniAsync(IpService, UseTokenSecurely(_loginService.CurrentLogin.JwtTokeBearer));
            MisurazioniCollection.AddRange(m.Take(1000).OrderByDescending((a) => a.Misurazione.DataOra));
            
            NeedleTemp.Value = MisurazioniCollection.OrderByDescending((z) => z.Misurazione.DataOra).Where((a) => a.Misurazione.SensoreId == 1).Select((x) => x.Misurazione.Valore).FirstOrDefault();
            NeedleHum.Value = MisurazioniCollection.OrderByDescending((z) => z.Misurazione.DataOra).Where((a) => a.Misurazione.SensoreId == 2).Select((x) => x.Misurazione.Valore).FirstOrDefault();
            
            IsBusy = false;
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

        private LoginInfo _loginInfoUser;

        public LoginInfo LoginInfoUser
        {
            get { return _loginInfoUser; }
            set { this.RaiseAndSetIfChanged(ref _loginInfoUser, value); }
        }
        #endregion

    }
}
