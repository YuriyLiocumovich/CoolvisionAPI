
using CoolvisionAPI.Models;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionDAL
{
    public class FlightsDAL
    {
        public async Task<FlightsResponse> GetFlightsAsync(string uri, string host)
        {
            ClientGet client = new ClientGet(uri, host);
            var returnValue = await client.GetAsync<FlightsResponse>();
            return returnValue;
        }
    }
}