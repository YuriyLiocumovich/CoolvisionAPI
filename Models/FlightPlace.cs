
namespace CoolvisionAPI.Models
{
    public class FlightPlace : PlaceBase
    {
        public FlightPlace() : base() { }

        public string Name { get; set; }
        public string Type { get; set; }
        public int PlaceId{ get; set; }
        public string IataCode { get; set; }
        public string SkyscannerCode { get; set; }
        public string CityName { get; set; }
    }
}