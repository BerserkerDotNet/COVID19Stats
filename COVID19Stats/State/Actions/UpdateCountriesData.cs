using BlazorState.Redux.Interfaces;
using COVID19Stats.Models;
using System.Collections.Generic;

namespace COVID19Stats.State.Actions
{
    public class UpdateCountriesData : IAction
    {
        public UpdateCountriesData(IEnumerable<CountryStatistics> data)
        {
            Data = data;
        }

        public IEnumerable<CountryStatistics> Data { get; }
    }
}
