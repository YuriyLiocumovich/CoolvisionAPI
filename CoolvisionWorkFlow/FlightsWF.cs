
using CoolvisionAPI.CoolvisionBusinessLogic;
using CoolvisionAPI.Models;
using CoolvisionAPI.Models.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionWorkFlow
{
    public class FlightsWF
    {
        public async Task<object> GetFlights(
            string place
            , string originPlace = null
            , string currency = null
            , string locale = null
            , string outboundpartialdate = null
            , string inboundDate = null)
        {
            List<string> plasesList = await new PlacesBL().GetRelevantPlacesAsync();

            plasesList = (from pl in plasesList
                          select pl.ToUpper()).ToList();

            if(!plasesList.Contains(place.ToUpper()))
                return new ErrorInformation(System.Net.HttpStatusCode.BadRequest, "The place you search is not accessible.");

            var flightResp = await SearchFlight(place, originPlace, currency, locale, outboundpartialdate, inboundDate);
            return flightResp;
        }

        public async Task<object> GetAnyFlights(
            string originPlace = null
            , string currency = null
            , string locale = null
            , string outboundpartialdate = null
            , string inboundDate = null)
        {
            List<string> plasesList = await new PlacesBL().GetRelevantPlacesAsync();

            plasesList = (from pl in plasesList
                          select pl.ToUpper()).ToList();

            List<Task<object>> taskList = new List<Task<object>>();

            foreach (string place in plasesList)
            {
                taskList.Add(SearchFlight(place, originPlace, currency, locale, outboundpartialdate, inboundDate));
            }
            await Task.WhenAll(taskList);

            var result = (from task in taskList
                          select task.Result).ToList();

            FlightsDTO flightResponse = new FlightsDTO
            {
                COVID19Statuses = new List<COVID19StatusDTO>(),
                Currencies = new List<Currency>(),
                Routes = new List<RouteDTO>()
            };

            var flightList = result.Where(o => o.GetType().Equals(typeof(FlightsDTO)));

            var covidStatusesList = flightList.Select(o => ((FlightsDTO)o).COVID19Statuses).ToList();
            covidStatusesList.ForEach(l => flightResponse.COVID19Statuses.AddRange(l));

            var currenciesList = flightList.Select(o => (((FlightsDTO)o).Currencies)).ToList();
            currenciesList.ForEach(c => flightResponse.Currencies.AddRange(c));

            var routesList = flightList.Select(o => (((FlightsDTO)o).Routes)).ToList();
            routesList.ForEach(r => flightResponse.Routes.AddRange(r));

            OrderFlightResponse(flightResponse);

            return flightResponse;
        }

        private void OrderFlightResponse(FlightsDTO flightsDTOResponse)
        {
            flightsDTOResponse.COVID19Statuses = flightsDTOResponse.COVID19Statuses.OrderBy(cs => cs.cases?.active).ToList();
            flightsDTOResponse.Routes = flightsDTOResponse.Routes.OrderBy(c => c.COVID19Active).ThenBy(c => c.Price).ToList();
        }

        private async Task<object> SearchFlight(
            string place
            , string originPlace=null
            , string currency = null
            , string locale = null
            , string outboundpartialdate = null
            , string inboundDate = null)
        {
            Place p;

            if (place.Length < 2 || originPlace?.Length < 2 || place.ToUpper().Equals(originPlace?.ToUpper()))
                return new ErrorInformation(System.Net.HttpStatusCode.BadRequest, "The place you search is not accessible.");

            try
            {
                if (originPlace?.Length > 2)
                {
                    p = await new PlacesBL().GetPlaceAsync(originPlace, null, currency, locale);
                    if (p != null)
                    {
                        originPlace = p.CountryId.Substring(0, 2);
                    }
                    p = null;
                }

                p = await new PlacesBL().GetPlaceAsync(place, originPlace, currency, locale);
                if (p != null)
                {
                    COVID19Response covRes = await new COVID19BL().GetCountry(place);

                    var flights = await new FlightsBL().GetFlightsAsync(p.CountryId.Substring(0, 2), originPlace, currency, locale,
                        outboundpartialdate, inboundDate);
                    var returnValue = await Task.Run(() => new FlightsDTO().ConvertToFlightsDTO(flights, covRes));
                    return returnValue;
                }
                return new ErrorInformation(System.Net.HttpStatusCode.InternalServerError, "The place you search is not accessible.");
            }
            catch (HttpRequestException reqEx)
            {
                return new ErrorInformation(System.Net.HttpStatusCode.BadRequest, reqEx.Message);
            }
            catch (Exception ex)
            {
                return new ErrorInformation(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}