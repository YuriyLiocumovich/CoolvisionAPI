
using CoolvisionAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionDAL
{
    public class PlacesDAL
    {
        public async Task<List<string>> GetRelevantPlacesAsync()
        {
            return await Task.Run(()=> new List<string>() { "France", "United Kingdom", "United States", "Australia" });
        }

        public async Task<PlacesResponse> GetPlacesAsync(string uri, string host)
        {
            ClientGet client = new ClientGet(uri, host);
            var returnValue = await client.GetAsync<PlacesResponse>();
            return returnValue;
        }
    }
}