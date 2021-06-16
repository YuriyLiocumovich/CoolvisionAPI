
using System.Collections.Generic;

namespace CoolvisionAPI.Models.TDO
{
    public class COVID19StatusDTO
    {
        public COVID19StatusDTO() { }
        public Case cases { get; set; }
        public COVID19Base deaths { get; set; }
        public COVID19Base tests { get; set; }
        public List<string> Countries { get; set; }
    }
}