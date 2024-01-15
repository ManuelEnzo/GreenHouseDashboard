using GreenHouseDashboard.DTO.Misurazioni;
using GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.Interfaces
{
    public interface IMisurazioniService
    {
        public Task<ObservableCollection<MisurazioniResponse>> GetMisurazioniAsync(string ipservice, string token);   
    }

    public class MisurazioniService : IMisurazioniService
    {
        public async Task<ObservableCollection<MisurazioniResponse>> GetMisurazioniAsync(string ipservice, string token)
        {
            try
            {
                var url = $"{ipservice}/Misurazioni/GetMisurazioni";

                IRequestHttpService requestHttp = new HttpRequestService();
                return await requestHttp.SendRequestAsync<ObservableCollection<MisurazioniResponse>>(url, HttpMethod.Get, authToken: token);

                //var lastC = MisurazioniCollection.LastOrDefault((x) => x.Misurazione.SensoreId == 1);

                //Needle.Value = lastC.Misurazione.Valore;


            }
            catch (Exception e)
            {

                throw e.GetBaseException();
            }
        }
    }
}
