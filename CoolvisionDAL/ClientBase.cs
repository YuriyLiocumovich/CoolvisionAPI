
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionDAL
{
    public class ClientBase
    {
        public HttpClient BaseHttpClient{get;}
        public HttpRequestMessage BaseRequest { get; }

        public ClientBase(string uri, string host)
        {
            BaseHttpClient = new HttpClient();
            BaseRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Headers =
    {
        { "x-rapidapi-key", "366b3fcd41msh0bd96a890de1e6dp11d9b8jsn12ca91fea89e" },
        { "x-rapidapi-host", host },
    },
            };
        }

        public async Task<T> SendAsync<T>()
        { 
            using (var response = await BaseHttpClient.SendAsync(BaseRequest))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsAsync<T>();
                return body;
            }
        }
    }
}