using BlazorState.Redux.Interfaces;
using COVID19Stats.Models;
using COVID19Stats.State.Actions;

namespace COVID19Stats.State.Reducers
{
    public class GlobalDataReducer : IReducer<GlobalStatistics>
    {
        public GlobalStatistics Reduce(GlobalStatistics state, IAction action)
        {
            switch (action)
            {
                case UpdateGlobalData a:
                    return a.Data;
                default:
                    return state;
            }
        }
    }
}
