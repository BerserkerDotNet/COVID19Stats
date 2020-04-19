using COVID19Stats.Models;
using System.Collections.Generic;

namespace COVID19Stats.State
{
    public class CountriesData
    {
        public IEnumerable<CountryStatistics> ByCountry { get; set; }

        public IEnumerable<CountryInfo> Countries { get; set; }
    }
}
