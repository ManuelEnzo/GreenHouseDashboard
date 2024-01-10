using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using GreenHouseDashboard.DTO.Login;
using GreenHouseDashboard.Models;
using GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces;
using Microsoft.VisualBasic;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using GreenHouseDashboard.Views;
using Avalonia.Threading;
using System.Security;
using GreenHouseDashboard.DTO.BaseEntity;
using GreenHouseDashboard.Interfaces;

namespace GreenHouseDashboard.ViewModels
{
    /// <summary>
    /// Gestisco interazione per Login
    /// </summary>
    public class LoginViewModel : PageViewModelBase
    {
        private  ILoginService _loginService;

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            LoginCommand = new RelayCommand(async () => await ExecuteLoginAsync(this.Username, this.Password), CanExecuteLogin);
            ReadAllJsonInformationLogin(ref _username, ref _password);
            this.WhenAnyValue(x => x._loginService.CurrentLogin)
                                .Subscribe(_ => UpdateCanNavigateNext());
        }


        #region -------------------- ExecuteNavigation

        public override bool CanNavigatePrevious
        {
            get => false;
            protected set => throw new NotSupportedException();
        }

        private bool _CanNavigateNext;

        public override bool CanNavigateNext
        {
            get { return _CanNavigateNext; }
            protected set { this.RaiseAndSetIfChanged(ref _CanNavigateNext, value); }
        }
        #endregion

        #region -------------------- Property Login
        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                this.RaiseAndSetIfChanged(ref _username, value);
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                this.RaiseAndSetIfChanged(ref _password, value);
                _loginCommand.RaiseCanExecuteChanged();
            }
        }


        #endregion

        #region -------------------- Command Login

        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand;
            }
            set { _loginCommand = value; }
        }


        #endregion

        #region -------------------- CanExecute - Execute Login

        public bool CanExecuteLogin()
        {
            return !System.String.IsNullOrEmpty(Password.Trim()) && !System.String.IsNullOrEmpty(Username.Trim());
        }

        public async Task ExecuteLoginAsync(string username, string passwr)
        {
            try
            {
                IsBusy = true;

                var url = $"{IpService}/Utente/AuthenticateUser";
                var dati = new
                {
                    nomeUtente = username,
                    password = passwr
                };

                IRequestHttpService requestHttp = new HttpRequestService();
                var response = await requestHttp.SendRequestAsync<LoginResponse>(url, HttpMethod.Post, dati);

                if (response != null)
                {
                    Debug.WriteLine($"Utente Loggato con successo : ------ {response} ------- ");
                    await Task.Delay(1000);


                    if (String.IsNullOrEmpty(response.Token))
                    {
                        throw new ArgumentNullException("JwtToken vuoto !");
                    }

                    _loginService.CurrentLogin = CreateLoginProfile(response.Utente, response.Token, true);
                    if(_loginService.CurrentLogin == null)
                    {
                        throw new ArgumentNullException("LoginService non inizializzato vuoto !");
                    }

                }
                else
                {
                    Debug.WriteLine($"Utente non loggato : ------ {response} ------- ");
                    await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
                    {
                        ContentTitle = "Attenzione !",
                        ContentMessage = "Password/Username non corretta. Ritenta per accedere !",
                        Icon = Icon.Warning,
                        ButtonDefinitions = ButtonEnum.Ok
                    }).ShowWindowAsync();
                }

            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Errore durante l'autenticazione: {ex.Message}");
            }
            finally { IsBusy = false; }

        }

        #endregion

        #region -------------------- ObjectInfo
        public class LoginInfo
        {
            public string Check { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
        #endregion


        private void ReadAllJsonInformationLogin(ref string username, ref string password)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.json");

                if (!File.Exists(filePath))
                {
                    return;
                }

                string jsonContent = File.ReadAllText(filePath);

                // Deserializza il JSON in un oggetto C#
                LoginInfo loginInfo = JsonConvert.DeserializeObject<LoginInfo>(jsonContent);

                if (loginInfo != null)
                {
                    if (loginInfo.Check == "1")
                    {
                        username = loginInfo.Username.Trim();
                        password = loginInfo.Password.Trim();
                    }

                }

            }
            catch (System.Exception)
            {

                throw new System.Exception("Errore in fase di deserializzazione Json Login");
            }

        }

        private void UpdateCanNavigateNext()
        {
            if (_loginService.CurrentLogin != null)
                CanNavigateNext = _loginService.CurrentLogin.IsLoggedIn;
        }



    }
}
