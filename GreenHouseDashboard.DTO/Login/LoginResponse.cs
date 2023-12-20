using GreenHouseDashboard.DTO.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.Login
{
    /// <summary>
    /// In risposta ho JwtToken, LoggedIn = true e l'intero oggetto Utente
    /// </summary>
    public class LoginResponse : ResponseBase
    {
        public string Token { get; set; }
        public bool LoggedIn { get; set; }
        public Utente Utente { get; set; }
    }
}
