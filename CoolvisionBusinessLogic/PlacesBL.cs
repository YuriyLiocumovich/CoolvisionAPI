
using CoolvisionAPI.CoolvisionDAL;
using CoolvisionAPI.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionBusinessLogic
{
    public class PlacesBL
    {
        public async Task<List<string>> GetRelevantPlacesAsync()
        {
            return await new PlacesDAL().GetRelevantPlacesAsync();
        }

        public async Task<Place> GetPlaceAsync(string placeName, string originPlace = null, string currency = null, string locale = null)
        {
            PlacesResponse plasesResp = await GetPlacesAsync(placeName, originPlace, currency, locale);
            Place place = plasesResp?.Places.Find(e => e.CountryName.ToUpper().Equals(placeName.ToUpper()) ||
                                                        e.CountryId.ToUpper().Substring(0, 2).Equals(placeName.ToUpper()));
            return place;
        }
        
        private async Task<PlacesResponse> GetPlacesAsync(string place, string originplace = null, string currency = null, string locale = null)
        {
            if (originplace == null)
                originplace = "IL";
            if (currency == null)
                currency = "USD";
            if (locale == null)
                locale = "en-US";

            var returnValue = await new PlacesDAL().GetPlacesAsync(BuildUri(originplace, currency, locale, place),
                "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            return returnValue;
        }

        private string BuildUri(string originplace, string currency, string locale, string place)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/autosuggest/v1.0/{0}/{1}/{2}/?query={3}",
                originplace, currency, locale, place);
            return sb.ToString();
        }
    }
}