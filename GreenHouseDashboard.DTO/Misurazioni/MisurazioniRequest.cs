using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.Misurazioni
{
    /// <summary>
    /// Per richiedere una misurazione ho bisogno dell'id del sensore
    /// </summary>
    public class MisurazioniRequest : RequestBase
    {
        public int SensorId { get; set; }
    }
}
