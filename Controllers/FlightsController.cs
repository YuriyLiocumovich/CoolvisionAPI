
using CoolvisionAPI.CoolvisionWorkFlow;
using CoolvisionAPI.Models;
using CoolvisionAPI.Models.TDO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CoolvisionAPI.Controllers
{
    [RoutePrefix("flights")]
    public class FlightsController : ApiController
    {
        /// <summary>
        /// Get flights for specific country
        /// </summary>
        /// <param name="place">Dectination country</param>
        /// <param name="originPlace">Your current country</param>
        /// <param name="currency">Currency you want the prices in</param>
        /// <param name="locale"></param>
        /// <param name="outboundpartialdate">The outbound date. Format “yyyy-mm-dd”, “yyyy-mm” or “anytime”</param>
        /// <param name="inboundDate">The return date. Format “yyyy-mm-dd”, “yyyy-mm” or “anytime”</param>
        /// <returns>Returns list of flights or Error</returns>
        [HttpGet, Route("")]
        [ResponseType(typeof(FlightsDTO))]
        public async Task<HttpResponseMessage> Get(
            string place
            , string originPlace = null
            , string currency = null
            , string locale = null
            , string outboundpartialdate = null
            , string inboundDate = null)
        {
            var returnValue = await new FlightsWF().GetFlights(place, originPlace, currency, locale, outboundpartialdate, inboundDate);
            if (returnValue is ErrorInformation)
                return Request.CreateResponse(((ErrorInformation)returnValue).HttpStatus, returnValue);
            return Request.CreateResponse(returnValue);
        }

        /// <summary>
        /// Get flights for allowed Countries list. Allowed countries: France, United Kingdom, United States, Australia
        /// </summary>
        /// <param name="originPlace">Your current country</param>
        /// <param name="currency">urrency you want the prices in</param>
        /// <param name="locale"></param>
        /// <param name="outboundpartialdate">The outbound date. Format “yyyy-mm-dd”, “yyyy-mm” or “anytime”</param>
        /// <param name="inboundDate">The return date. Format “yyyy-mm-dd”, “yyyy-mm” or “anytime”</param>
        /// <returns>Returns list of flights or Error</returns>
        [HttpGet, Route("any")]
        [ResponseType(typeof(FlightsDTO))]
        public async Task<HttpResponseMessage> GetAny(
            string originPlace = null
            , string currency = null
            , string locale = null
            , string outboundpartialdate = null
            , string inboundDate = null)
        {
            var returnValue = await new FlightsWF().GetAnyFlights(originPlace, currency, locale, outboundpartialdate, inboundDate);
            if (returnValue is ErrorInformation)
                return Request.CreateResponse(((ErrorInformation)returnValue).HttpStatus, returnValue);
            return Request.CreateResponse(returnValue);
        }
    }
}
