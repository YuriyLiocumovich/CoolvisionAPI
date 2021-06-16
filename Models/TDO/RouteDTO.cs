
using System;
using System.Collections.Generic;

namespace CoolvisionAPI.Models.TDO
{
    public class RouteDTO
    {
        public RouteDTO() { }
        public int? Price { get; set; }
        public DateTime? QuoteDateTime { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public List<QuoteDTO> Quotes { get;set;}
        public int COVID19Active { get; set; }
    }
}