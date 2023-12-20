using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.BaseEntity
{
    public class Misurazione : EntitaBase
    {
        public int SensoreId { get; set; }
        public double Valore { get; set; }
        public DateTime DataOra { get; set; } = DateTime.Now;
    }
}
