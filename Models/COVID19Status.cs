
using System;

namespace CoolvisionAPI.Models
{
    public class COVID19Status
    {
        public COVID19Status() { }

        public string continent { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public Case cases { get; set; }
        public COVID19Base deaths { get; set; }
        public COVID19Base tests { get; set; }
        public string day { get; set; }
        public DateTime time { get; set; }
    }
}