using GreenHouseDashboard.DTO.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.Misurazioni
{
    /// <summary>
    /// In risposta ho l'intera misurazione
    /// </summary>
    public class MisurazioniResponse : ResponseBase
    {
        public Misurazione Misurazione { get; set; }
    }
}
