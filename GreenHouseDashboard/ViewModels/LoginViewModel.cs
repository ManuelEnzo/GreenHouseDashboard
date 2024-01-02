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

namespace GreenHouseDashboard.ViewModels
{
    /// <summary>
    /// Gestisco interazione per Login
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {

        public event EventHandler LoginCompleted;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async () => await ExecuteLoginAsync(this.Username, this.Password), CanExecuteLogin);
            this.Username = string.Empty;
            this.Password = string.Empty;
            ReadAllJsonInformationLogin(ref _username, ref _password);
        }


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
        private Window login;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand;
            }
            set { _loginCommand = value; }
        }


        #endregion

        #region -------------------- CanExecute Execute Login

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
                    await Task.Delay(2000);
                    if (!String.IsNullOrEmpty(response.Token))
                    {
                        JwtToken = response.Token;
                    }

                    LoginCompleted?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// ----------- 20/12/2023 -----------
        /// Per gestire la checkbox di accesso rapido ho bisogno di serializzare in fase di exit un file json 
        /// al momento del login deserializzo il file e setto in automatico username e password
        /// </summary>
        /// <returns></returns>
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

        public class LoginInfo
        {
            public string Check { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private CancellationTokenSource _cts = new CancellationTokenSource();
        public CancellationToken cancellationToken => _cts.Token;
        public void Cancel()
        {
            _cts.Cancel();
        }

    }
}
