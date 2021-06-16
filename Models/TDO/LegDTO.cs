using System;

using System.Collections.Generic;

namespace CoolvisionAPI.Models.TDO
{
    public class LegDTO
    {
        public LegDTO() { }

        public List<Carrier> Carriers { get; set; }
        //public PlaceDTO Origin { get; set; }
        public FlightPlace Origin { get; set; }
        //public PlaceDTO Destination { get; set; }
        public FlightPlace Destination { get; set; }
        public DateTime DepartureDate { get; set; }

    }
}