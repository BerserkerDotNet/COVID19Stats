namespace COVID19Stats.Models
{
    public class CountryTimeLine
    {
        public string Country { get; set; }

        public string[] Provinces { get; set; }

        public Timeline Timeline { get; set; }
    }
}
