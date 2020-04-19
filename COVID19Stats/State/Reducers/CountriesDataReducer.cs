using BlazorState.Redux.Interfaces;
using COVID19Stats.State.Actions;
using System.Linq;

namespace COVID19Stats.State.Reducers
{
    public class CountriesDataReducer : IReducer<CountriesData>
    {
        public CountriesData Reduce(CountriesData state, IAction action)
        {
            switch (action)
            {
                case UpdateCountriesData a:
                    return new CountriesData
                    {
                        ByCountry = a.Data,
                        Countries = a.Data.Select(i => i.CountryInfo).ToArray()
                    };
                default:
                    return state;
            }
        }
    }
}
