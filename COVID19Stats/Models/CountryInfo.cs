using System.Text.Json.Serialization;

namespace COVID19Stats.Models
{
    public class CountryInfo
    {
        [JsonPropertyName("_id")]
        public int? Id { get; set; }

        public string ISO2 { get; set; }

        public string ISO3 { get; set; }

        public float Lat { get; set; }

        public float Lon { get; set; }

        public string Flag { get; set; }
    }
}
