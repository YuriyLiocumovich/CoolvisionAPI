
using Newtonsoft.Json;

namespace CoolvisionAPI.Models
{
    public class COVID19Base
    {
        public COVID19Base() { }

        [JsonProperty("new")]
        public string newCase { get; set; }

        [JsonProperty("1M_pop")]
        public string pop { get; set; }
        public int total { get; set; }
    }
}