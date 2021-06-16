
using CoolvisionAPI.CoolvisionDAL;
using CoolvisionAPI.Models;
using System.Text;
using System.Threading.Tasks;

namespace CoolvisionAPI.CoolvisionBusinessLogic
{
    public class COVID19BL
    {
        public async Task<COVID19Response> GetCountry(string place)
        {
            COVID19DAL dal = new COVID19DAL();
            var returnValue = await dal.GetCOVID19Async(BuildUri(place),
                "covid-193.p.rapidapi.com");
            return returnValue;
        }

        private string BuildUri(string place)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("https://covid-193.p.rapidapi.com/countries?search={0}", place);
            return sb.ToString();
        }
    }
}