using Newtonsoft.Json;

namespace COVID19Stats.Models
{

    public class CountryStatistics : BaseStatistics
    {
        public string Country { get; set; }

        public CountryInfo CountryInfo { get; set; }
    }
}
