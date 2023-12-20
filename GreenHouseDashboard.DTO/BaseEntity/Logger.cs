using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.BaseEntity
{
    /// <summary> 24/10/2023
    /// Classe che mappa la tabella logger
    /// Non ha ne request ne response in quanto non è richiamabile
    /// In futuro, posso creare un LoggerRequest così posso inserire messaggi di log
    /// richiamando l'API
    /// </summary>
    public class Logger : EntitaBase
    {
        public string Evento { get; set; }
        public string LogType { get; set; }
        public DateTime DataOra { get; set; } = DateTime.Now;
    }
    public enum LogType
    {
        Log,
        Info,
        Warn,
        Error
    }
}
