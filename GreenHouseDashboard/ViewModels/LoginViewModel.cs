using GreenHouseDashboard.Models;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenHouseDashboard.ViewModels
{
    /// <summary>
    /// Gestisco interazione per Login
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async () => await ExecuteLoginAsync(), CanExecuteLogin);
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
            return !String.IsNullOrEmpty(Password.Trim()) && !String.IsNullOrEmpty(Username.Trim());
        }

        public async Task ExecuteLoginAsync()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
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
                        Username = loginInfo.Username.Trim();
                        Password = loginInfo.Password.Trim();
                    }
                    
                }

            }
            catch (Exception)
            {

                throw new Exception("Errore in fase di deserializzazione Json Login");
            }

        }

        public class LoginInfo
        {
            public string Check { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
