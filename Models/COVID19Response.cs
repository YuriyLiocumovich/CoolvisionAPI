
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CoolvisionAPI.Models
{
    public class COVID19Response
    {
        public COVID19Response() { }

        public string get { get; set; }
        public Parameter parameters { get; set; }

        public int results { get; set; }
        
        [JsonExtensionData]
        public IDictionary<string, JToken> response { get; set; }
    }
}