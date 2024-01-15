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
using DynamicData;
using System.Windows.Input;
using System.Linq;

namespace GreenHouseDashboard.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private readonly IMisurazioniService _misurazioniService;

        public MainWindowViewModel(ILoginService loginService, IMisurazioniService misurazioniService)
        {
            _loginService = loginService;
            this._misurazioniService = misurazioniService;
            Pages = new ObservableCollection<Func<PageViewModelBase>>
            {
                () => new LoginViewModel(_loginService),
                () => new InterfacciaGraficiViewModel(_loginService, _misurazioniService)
            };

            _CurrentPage = Pages[0]();

            var canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
            var canNavPrev = this.WhenAnyValue(x => x.CurrentPage.CanNavigatePrevious);

            NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
            NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
        }

        private readonly ObservableCollection<Func<PageViewModelBase>> Pages;

        private PageViewModelBase _CurrentPage;

        public PageViewModelBase CurrentPage
        {
            get => _CurrentPage;
            private set => this.RaiseAndSetIfChanged(ref _CurrentPage, value);
        }

        public ReactiveCommand<Unit, Unit> NavigateNextCommand { get; }

        private void NavigateNext()
        {
            int index = Pages
                .Select((page, pageIndex) => new { Page = page, Index = pageIndex })
                .FirstOrDefault(x => x.Page().GetType() == CurrentPage.GetType())
                ?.Index ?? -1;

            if (index < 0) { return ; }
            index++;
            if (index < Pages.Count)
                CurrentPage = Pages[index]();
        }

        public ReactiveCommand<Unit, Unit> NavigatePreviousCommand { get; }

        private void NavigatePrevious()
        {
            int index = Pages
                .Select((page, pageIndex) => new { Page = page, Index = pageIndex })
                .FirstOrDefault(x => x.Page().GetType() == CurrentPage.GetType())
                ?.Index ?? -1;
            if (index < 0) { return; }
            index--;
            if (index >= 0)
                CurrentPage = Pages[index]();
        }
    }
    }
