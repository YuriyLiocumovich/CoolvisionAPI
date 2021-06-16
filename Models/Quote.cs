
using System;

namespace CoolvisionAPI.Models
{
    public class Quote
    {
        public Quote() { }

        public int QuoteId { get; set; }
        public int MinPrice { get; set; }
        public bool Direct { get; set; }
        public Leg OutboundLeg { get; set; }
        public DateTime QuoteDateTime { get; set; }
    }
}