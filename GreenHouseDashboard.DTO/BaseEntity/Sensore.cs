using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.BaseEntity
{
    public class Sensore : EntitaBase
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public string ImageUrl { get; set; }
    }
}
