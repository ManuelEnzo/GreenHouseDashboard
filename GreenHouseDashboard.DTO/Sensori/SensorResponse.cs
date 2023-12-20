using GreenHouseDashboard.DTO.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO.Sensori
{
    public class SensorResponse : ResponseBase
    {
        public Sensore Sensore { get; set; }
    }
}
