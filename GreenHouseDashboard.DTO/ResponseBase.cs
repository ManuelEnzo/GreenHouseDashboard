using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO
{
    /// <summary>
    /// Risposta base dopo richiesta API
    /// </summary>
    public class ResponseBase
    {
        public ResponseBase()
        {
            Success = true;
            Message = string.Empty;
            HasError = false;
        }
        public bool Success { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
    }
}
