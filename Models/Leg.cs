
using System;
using System.Collections.Generic;

namespace CoolvisionAPI.Models
{
    public class Leg
    {
        public Leg() { }

        public List<int> CarrierIds { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}