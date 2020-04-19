using BlazorState.Redux.Interfaces;
using COVID19Stats.Services;
using System.Threading.Tasks;

namespace COVID19Stats.State.Actions.Async
{
    public class FetchCountriesData : IAsyncAction
    {
        private readonly ICovidClient _client;

        public FetchCountriesData(ICovidClient client)
        {
            _client = client;
        }

        public async Task Execute(IDispatcher dispatcher)
        {
            var data = await _client.AllCountries();
            dispatcher.Dispatch(new UpdateCountriesData(data));
        }
    }
}
