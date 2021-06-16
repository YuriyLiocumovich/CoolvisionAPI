
using System.Collections.Generic;
namespace CoolvisionAPI.Models
{
    public class FlightsResponse
    {
        public FlightsResponse() { }

        public List<Quote> Quotes { get; set; }
        public List<Carrier> Carriers { get; set; }
        public List<FlightPlace> Places { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Route> Routes { get; set; }
    }
}