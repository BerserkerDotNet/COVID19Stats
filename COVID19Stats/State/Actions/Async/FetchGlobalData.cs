using BlazorState.Redux.Interfaces;
using COVID19Stats.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Stats.State.Actions.Async
{
    public class FetchGlobalData : IAsyncAction
    {
        private readonly ICovidClient _client;

        public FetchGlobalData(ICovidClient client)
        {
            _client = client;
        }

        public async Task Execute(IDispatcher dispatcher)
        {
            var data = await _client.Global();
            dispatcher.Dispatch(new UpdateGlobalData(data));
        }
    }
}
