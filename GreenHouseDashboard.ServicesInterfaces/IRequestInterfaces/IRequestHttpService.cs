using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.ServicesInterfaces.IRequestInterfaces
{
    public interface IRequestHttpService
    {
        Task<T> SendRequestAsync<T>(string url, HttpMethod method, object data = null, string authToken = null);
    }

    public class HttpRequestService : IRequestHttpService
    {
        private readonly HttpClient httpClient;

        public HttpRequestService()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<T> SendRequestAsync<T>(string url, HttpMethod method, object data = null, string authToken = null)
        {
            try
            {
                HttpResponseMessage response;

                if (method == HttpMethod.Get)
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    }

                    response = await httpClient.SendAsync(request);
                }
                else if (method == HttpMethod.Post)
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    }

                    if (data != null)
                    {
                        var jsonContents = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        request.Content = jsonContents;
                    }

                    response = await httpClient.SendAsync(request);
                }
                else
                {
                    throw new NotSupportedException($"Unsupported HTTP method: {method}");
                }

                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }

                string jsonContent = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Errore nella richiesta HTTP: {ex.Message}");
                return default;
            }
        }



    }
}

