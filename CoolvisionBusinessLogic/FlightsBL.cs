
using CoolvisionAPI.CoolvisionDAL;
using CoolvisionAPI.Models;
using System.Text;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionBusinessLogic
{
    public class FlightsBL
    {
        public async Task<FlightsResponse> GetFlightsAsync(
            string destination
            , string originPlace = null
            , string currency = null
            , string locale = null
            , string outboundpartialdate = null
            , string inboundDate = null
            )
        {
            if (originPlace == null)
                originPlace = "IL";
            if (currency == null)
                currency = "USD";
            if (locale == null)
                locale = "en-US";
            if (outboundpartialdate == null)
                outboundpartialdate = "anytime";
            if (inboundDate == null)
                inboundDate = "anytime";

            FlightsDAL dal = new FlightsDAL();
            var returnValue = await dal.GetFlightsAsync(BuildUri(originPlace, currency, locale, destination, outboundpartialdate, inboundDate),
                "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            return returnValue;
        }

        private string BuildUri(
            string originPlace
            , string currency
            , string locale
            , string destination
            , string outboundpartialdate = null
            , string inboundDate = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/browseroutes/v1.0/{0}/{1}/{2}/{3}/{4}/{5}?inboundpartialdate={6}",
                originPlace, currency, locale, originPlace, destination, outboundpartialdate, inboundDate);
            return sb.ToString();
        }
    }
}