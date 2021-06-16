
namespace CoolvisionAPI.Models
{
    public class Case: COVID19Base
    {
        public Case() : base() { }

        public int active { get; set; }
        public int critical { get; set; }
        public int recovered { get; set; }
        
    }
}