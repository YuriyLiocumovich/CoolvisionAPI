using System;

namespace CoolvisionAPI.Models.TDO
{
    public class QuoteDTO
    {
        public QuoteDTO() { }
        public int QuoteId { get; set; }
        public int MinPrice { get; set; }
        public bool Direct { get; set; }
        //public List<LegDTO> Legs { get; set; }
        public LegDTO Leg { get; set; }
        public DateTime QuoteDateTime { get; set; }
    }
}