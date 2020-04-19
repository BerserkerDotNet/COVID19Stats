using COVID19Stats.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace COVID19Stats.Services
{
    public class NOVELCovidClient : ICovidClient
    {
        const string ApiEndpoint = "https://corona.lmao.ninja";

        private readonly HttpClient _client;

        public NOVELCovidClient(HttpClient client)
        {
            _client = client;
        }

        public Task<GlobalStatistics> Global()
        {
            return Get<GlobalStatistics>("all");
        }

        public Task<IEnumerable<CountryStatistics>> AllCountries()
        {
            return Get<IEnumerable<CountryStatistics>>("countries?sort=cases");
        }

        public async Task<IEnumerable<CountryInfo>> CountriesList()
        {
            var byCountryInfo = await AllCountries();
            return byCountryInfo.Select(i => i.CountryInfo).ToArray();
        }

        public Task<CountryStatistics> Country(string country)
        {
            return Get<CountryStatistics>($"countries/{country}");
        }

        public Task<CountryTimeLine> Timeline(string country, int lastDays)
        {
            var days = lastDays == -1 ? "all" : lastDays.ToString();
            return Get<CountryTimeLine>($"v2/historical/{country}?lastdays={days}");
        }

        private Task<T> Get<T>(string url)
        {
            return _client.GetJsonAsync<T>($"{ApiEndpoint}/v2/{url}");
        }
    }
}
