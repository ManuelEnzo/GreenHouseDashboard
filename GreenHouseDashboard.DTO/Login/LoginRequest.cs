using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.Login
{
    /// <summary>
    /// Utilizzata per effettuare autenticazione
    /// </summary>
    public class LoginRequest : RequestBase
    {
        public string Password { get; set; }
    }
}
