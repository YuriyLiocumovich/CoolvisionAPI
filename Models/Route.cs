
using System;
using System.Collections.Generic;

namespace CoolvisionAPI.Models
{
    public class Route
    {
        public Route() { }

        public int? Price { get; set; }
        public DateTime? QuoteDateTime { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public List<int> QuoteIds { get; set; }
    }
}