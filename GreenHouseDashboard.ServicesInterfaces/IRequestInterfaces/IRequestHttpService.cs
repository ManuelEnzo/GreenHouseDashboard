using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces
{
    public interface IRequestHttpService
    {
        Task<T> SendRequestAsync<T>(string url, HttpMethod method, object data = null);
    }

    public class HttpRequestService : IRequestHttpService
    {
        private readonly HttpClient httpClient;

        public HttpRequestService()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<T> SendRequestAsync<T>(string url, HttpMethod method, object data = null)
        {
            try
            {
                HttpResponseMessage response;

                if (method == HttpMethod.Get)
                {
                    response = await httpClient.GetAsync(url);
                }
                else if (method == HttpMethod.Post)
                {
                    var jc = data != null ? new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json") : null;
                    response = await httpClient.PostAsync(url, jc);
                }
                else
                {
                    throw new NotSupportedException($"Unsupported HTTP method: {method}");
                }

                if (!response.IsSuccessStatusCode)
                {
                    return default; // o gestisci l'errore in un altro modo a seconda delle tue esigenze
                }

                string jsonContent = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonContent);
            }
            catch (HttpRequestException ex)
            {
                // Gestisci errori di rete, timeout, ecc.
                Console.WriteLine($"Errore nella richiesta HTTP: {ex.Message}");
                return default; // o gestisci l'errore in un altro modo a seconda delle tue esigenze
            }
        }

    }
}

