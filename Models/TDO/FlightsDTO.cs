
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolvisionAPI.Models.TDO
{
    public class FlightsDTO
    {
        public FlightsDTO() { }

        public List<COVID19StatusDTO> COVID19Statuses { get; set; }
        public List<RouteDTO> Routes { get; set;}
        public List<Currency> Currencies { get; set; }

        public object ConvertToFlightsDTO(FlightsResponse flRes, COVID19Response covRes)
        {
            try
            {
                if (covRes.response["errors"].GetType().Equals(typeof(JArray)))
                {
                    List<COVID19Error> errorsList = covRes.response["errors"].ToObject<List<COVID19Error>>();
                    if (errorsList?.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        errorsList.ForEach(e => sb.Append(e.search).Append(","));
                        return new ErrorInformation(System.Net.HttpStatusCode.BadRequest, sb.ToString());
                    }
                }
                else
                {
                    COVID19Error errors = covRes.response["errors"].ToObject<COVID19Error>();
                    if (errors != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        return new ErrorInformation(System.Net.HttpStatusCode.BadRequest, errors.search);
                    }
                }
            }
            catch
            {
                string errors = covRes.response["errors"].ToObject<string>();
                if (errors.Length > 0)
                    return new ErrorInformation(System.Net.HttpStatusCode.BadRequest, errors);
            }

            ///
            ///Build COVID19Status
            ///
            try
            {
                List<COVID19Status> covList = covRes.response["response"].ToObject<List<COVID19Status>>();

                if (covList != null && covList.Count > 0)
                {
                    var lastCovidStatus = covList.OrderByDescending(r => r.time).FirstOrDefault();

                    COVID19Statuses = new List<COVID19StatusDTO>
                    {
                        new COVID19StatusDTO
                        {
                            cases = lastCovidStatus.cases,
                            deaths = lastCovidStatus.deaths,
                            tests = lastCovidStatus.tests
                        }
                    };
                }
            }
            catch
            {
                COVID19Statuses = new List<COVID19StatusDTO>
                {
                     new COVID19StatusDTO
                    {
                        Countries = covRes.response["response"].ToObject<List<string>>()
                    }
                };
            }

            Currencies = flRes.Currencies;

            ///
            ///Remove all routes without price
            ///
            flRes.Routes = flRes.Routes.FindAll(r => r.Price > 0);

            string countryName = flRes.Places.Find(p => p.PlaceId.Equals(flRes.Routes.FirstOrDefault().DestinationId)).CountryName;

            if (COVID19Statuses == null)
                COVID19Statuses = new List<COVID19StatusDTO>();
            if (COVID19Statuses.Count.Equals(0))
                COVID19Statuses.Add(new COVID19StatusDTO { Countries = new List<string> { countryName } });
            if (COVID19Statuses.FirstOrDefault().Countries == null)
                COVID19Statuses.FirstOrDefault().Countries = new List<string> { countryName };

            Routes = new List<RouteDTO>();
            
            ///
            ///Build RouteDTO objects
            ///
            flRes.Routes.ForEach(r => Routes.Add(
            new RouteDTO
            {
                Price = r.Price,
                QuoteDateTime = r.QuoteDateTime,
                DestinationId = r.DestinationId,
                OriginId = r.OriginId,
                Quotes = GetQuotes(r.QuoteIds, flRes),
                COVID19Active = COVID19Statuses.FirstOrDefault().cases == null ? 0 : COVID19Statuses.FirstOrDefault().cases.active
            }));
            return this;
        }

        private List<QuoteDTO> GetQuotes(List<int> quoteIds, FlightsResponse flRes)
        {
            List<QuoteDTO> quotes = (from q in flRes.Quotes
                      where quoteIds.Contains(q.QuoteId)
                      select new QuoteDTO
                      {
                          QuoteId = q.QuoteId,
                          Direct = q.Direct,
                          MinPrice = q.MinPrice,
                          QuoteDateTime = q.QuoteDateTime,
                          Leg = GetLeg(q.OutboundLeg, flRes)
                      }).ToList();
            return quotes;
        }

        private LegDTO GetLeg(Leg leg, FlightsResponse flRes)
        {
            LegDTO retValue = new LegDTO {
                DepartureDate = leg.DepartureDate,
                Destination = GetPlace(leg.DestinationId, flRes.Places),
                Origin = GetPlace(leg.OriginId, flRes.Places),
                Carriers = GetCarriers(leg.CarrierIds, flRes.Carriers)
            };
            return retValue;
        }

        private List<Carrier> GetCarriers(List<int> carrierIds, List<Carrier> carriers)
        {
            List<Carrier> returnValue = (from c in carriers
                    where carrierIds.Contains(c.CarrierId)
                    select c).ToList();
            return returnValue;
        }

        private FlightPlace GetPlace(int placeId, List<FlightPlace> flightPlaces)
        {
            FlightPlace returnValue = (from p in flightPlaces
                    where p.PlaceId.Equals(placeId)
                    select p).FirstOrDefault();
            return returnValue;
        }
    }
}   