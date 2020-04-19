using BlazorState.Redux.Interfaces;
using COVID19Stats.Models;

namespace COVID19Stats.State.Actions
{
    public class UpdateGlobalData : IAction
    {
        public UpdateGlobalData(GlobalStatistics data)
        {
            Data = data;
        }

        public GlobalStatistics Data { get; }
    }
}
