using BlazorState.Redux.Blazor;
using BlazorState.Redux.Interfaces;
using COVID19Stats.Models;
using COVID19Stats.State;
using COVID19Stats.State.Actions.Async;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace COVID19Stats.Components
{
    public class WorldStatisticsConnected
    {
        public static RenderFragment Get()
        {
            var c = new WorldStatisticsConnected();
            return ComponentConnector.Connect<WorldStatistics, AppState, WorldStatisticsProps>(c.MapStateToProps, c.MapDispatchToProps, c.Init);
        }

        private async Task Init(IStore<AppState> store)
        {
            await store.Dispatch<FetchGlobalData>();
        }

        private void MapStateToProps(AppState state, WorldStatisticsProps props)
        {
            var stats = state?.Global;
            if (stats is null)
            {
                stats = new GlobalStatistics();
            }

            props.Cases = stats.Cases;
            props.Deaths = stats.Deaths;
            props.Recovered = stats.Recovered;
            props.Active = stats.Active;
            props.Critical = stats.Critical;
            props.Updated = stats.UpdatedDate;
        }

        private void MapDispatchToProps(IStore<AppState> store, WorldStatisticsProps props)
        {
        }
    }

    public class WorldStatisticsProps
    {
        public int Cases { get; set; }

        public int Deaths { get; set; }

        public int Recovered { get; set; }

        public int Active { get; set; }

        public int Critical { get; set; }

        public DateTime Updated { get; set; }
    }
}
