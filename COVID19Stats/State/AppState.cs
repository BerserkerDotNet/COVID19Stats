using COVID19Stats.Models;

namespace COVID19Stats.State
{
    public class AppState
    {
        public GlobalStatistics Global { get; set; }

        public CountriesData CountriesData { get; set; }
    }
}
