using CoolvisionAPI.Models;

using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionDAL
{
    public class COVID19DAL
    {
        public async Task<COVID19Response> GetCOVID19Async(string uri, string host)
        {
            ClientGet client = new ClientGet(uri, host);
            var returnValue = await client.GetAsync<COVID19Response>();
            return returnValue;
        }
    }
}