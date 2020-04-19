using COVID19Stats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COVID19Stats.Services
{
    public interface ICovidClient
    {
        Task<GlobalStatistics> Global();

        Task<IEnumerable<CountryStatistics>> AllCountries();

        Task<IEnumerable<CountryInfo>> CountriesList();

        Task<CountryStatistics> Country(string country);

        Task<CountryTimeLine> Timeline(string country, int lastDays);
    }
}
