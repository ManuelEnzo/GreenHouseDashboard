using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DTO
{
    public class RequestBase : IRequestBase
    {
        public RequestBase() { }

        public RequestBase(IRequestBase reqBase)
        {
            this.NomeUtente = reqBase.NomeUtente;
        }

        public string NomeUtente { get; set; }

    }
}
