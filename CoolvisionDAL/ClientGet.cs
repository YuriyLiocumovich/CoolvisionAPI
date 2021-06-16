
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionDAL
{
    public class ClientGet : ClientBase
    {
        public ClientGet(string uri, string host) : base(uri, host) { }

        public async Task<T> GetAsync<T>()
        {
            BaseRequest.Method = HttpMethod.Get;
            return await SendAsync<T>();
        }
    }
}