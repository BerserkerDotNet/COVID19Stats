using BlazorStorage.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace COVID19Stats.Services
{
    /// <summary>
    /// Client for reading data from https://covidtracking.com/api. Has data for US.
    /// </summary>
    public class CovidTrackingClient
    {
        private readonly HttpClient _client;
        private readonly ILocalStorage _storage;

        public CovidTrackingClient(HttpClient client, ILocalStorage storage)
        {
            _client = client;
            _storage = storage;
        }

        public Task<IEnumerable<CovidTrackingCurrent>> GetStates()
        {
            return GetAllCsv<CovidTrackingCurrent>("states_current");
        }
        
        public async Task<Dictionary<string, IEnumerable<CovidTrackingTimeline>>> GetStatesTimeline()
        {
            var timeline = await GetAllCsv<CovidTrackingTimeline>("states_daily_4pm_et");
            return timeline.GroupBy(c => c.State).ToDictionary(c => c.Key, c => c.AsEnumerable());
        }

        public async Task<IEnumerable<CovidWorldCurrent>> GetWorld()
        {
            var item = await _storage.GetItem<DataWrapper<CovidWorldCurrent>>(nameof(GetWorld));
            if (item == null)
            {
                var url = "https://covid2019-api.herokuapp.com/v2/current";
                item = await _client.GetJsonAsync<DataWrapper<CovidWorldCurrent>>(url);
                await _storage.SetItem(nameof(GetWorld), item);
            }

            return item.Data;
        }

        public async Task<IEnumerable<CovidWorldTimeline>> GetWorldConfirmedTimeline()
        {
            var item = await _storage.GetItem<DataWrapper<CovidWorldTimeline>>(nameof(GetWorldConfirmedTimeline));
            if (item == null)
            {
                var confirmedUrl = "https://covid2019-api.herokuapp.com/v2/timeseries/confirmed";
                item = await _client.GetJsonAsync<DataWrapper<CovidWorldTimeline>>(confirmedUrl);

                await _storage.SetItem(nameof(GetWorldConfirmedTimeline), item);
            }

            return item.Data;
        }

        public async Task<IEnumerable<CovidWorldTimeline>> GetWorldDeathsTimeline()
        {
            var item = await _storage.GetItem<DataWrapper<CovidWorldTimeline>>(nameof(GetWorldDeathsTimeline));
            if (item == null)
            {
                var deathsUrl = "https://covid2019-api.herokuapp.com/v2/timeseries/deaths";
                item = await _client.GetJsonAsync<DataWrapper<CovidWorldTimeline>>(deathsUrl);

                await _storage.SetItem(nameof(GetWorldDeathsTimeline), item);
            }

            return item.Data;
        }

        public async Task<IEnumerable<GlobalWorldTimeline>> GetWorldTimeline()
        {
            var item = await _storage.GetItem<DataWrapper<Dictionary<string, GlobalWorldTimelineValue>>>(nameof(GetWorldTimeline));
            if (item == null)
            {
                var deathsUrl = "https://covid2019-api.herokuapp.com/v2/timeseries/global";
                item = await _client.GetJsonAsync<DataWrapper<Dictionary<string, GlobalWorldTimelineValue>>>(deathsUrl);

                await _storage.SetItem(nameof(GetWorldTimeline), item);
            }

            return item.Data.Select(c => new GlobalWorldTimeline
            {
                Date = DateTime.Parse(c.Keys.First()),
                Confirmed = c.Values.First().Confirmed,
                Deaths = c.Values.First().Deaths,
            }).ToArray();
        }

        private async Task<IEnumerable<T>> GetAllCsv<T>(string file)
        {
            using var stream = await _client.GetStreamAsync($"/data/{file}.csv");
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
            return csv.GetRecords<T>().ToArray();
        }
    }

    public class GlobalWorldTimeline
    {
        public DateTime Date { get; set; }

        public int Confirmed { get; set; }

        public int Deaths { get; set; }
    }


    public class GlobalWorldTimelineValue
    {
        public int Confirmed { get; set; }

        public int Deaths { get; set; }
    }


    public class CovidWorldTimeline
    {
        [JsonPropertyName("Country/Region")]
        public string Country { get; set; }

        [JsonPropertyName("Province/State")]
        public string Province { get; set; }

        public IEnumerable<TimeLine> TimeSeries { get; set; }
    }

    public class TimeLine
    {
        public string Date { get; set; }

        public int Value { get; set; }

        public DateTime When => DateTime.Parse(Date);
    }

    public class DataWrapper<T>
    {
        public IEnumerable<T> Data { get; set; }

        [JsonPropertyName("dt")]
        public string DateTime { get; set; }
    }

    public class CovidWorldCurrent
    {
        public string Location { get; set; }

        public int Confirmed { get; set; }

        public int Deaths { get; set; }

        public int Recovered { get; set; }

        public int Active { get; set; }
    }

    public class CovidTrackingCurrent
    {
        public string State { get; set; }

        public string StateName => StateHelper.GetState(State);

        public float? Positive { get; set; }

        public float? Negative { get; set; }

        public float? Pending { get; set; }

        public float? Hospitalized { get; set; }

        public float? Death { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateChecked { get; set; }

        public float? Total { get; set; }

        public string Grade { get; set; }
    }

    public class CovidTrackingTimeline
    {
        public string Date { get; set; }

        public string State { get; set; }

        public string StateName => StateHelper.GetState(State);

        public float? Positive { get; set; }

        public float? Negative { get; set; }

        public float? Pending { get; set; }

        public float? Hospitalized { get; set; }

        public float? Death { get; set; }

        public float Total { get; set; }

        public DateTime DateChecked { get; set; }
    }
}
