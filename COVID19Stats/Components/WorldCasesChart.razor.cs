using BlazorState.Redux.Blazor;
using BlazorState.Redux.Interfaces;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.PieChart;
using COVID19Stats.Models;
using COVID19Stats.State;
using COVID19Stats.State.Actions.Async;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace COVID19Stats.Components
{
    public class WorldCasesChartConnected
    {
        private PieConfig _chartConfig;
        private PieDataset _chartDataSet;

        public WorldCasesChartConnected()
        {
            _chartConfig = new PieConfig
            {
                Options = new PieOptions(true)
                {
                    Legend = new Legend
                    {
                        Display = false
                    },
                    Responsive = true,
                    MaintainAspectRatio = false,
                    CutoutPercentage = 80,
                    Tooltips = new Tooltips
                    {
                        Enabled = true,
                        Mode = InteractionMode.Index,
                        Intersect = false,
                        BorderWidth = 1,
                        BorderColor = "rgba(0, 0, 0, 0.12)",
                        BackgroundColor = "#ffffff",
                        TitleFontColor = "rgba(0, 0, 0, 0.87)",
                        BodyFontColor = "rgba(0, 0, 0, 0.54)",
                        FooterFontColor = "rgba(0, 0, 0, 0.54)"
                    }
                }
            };

            _chartDataSet = new PieDataset
            {
                BackgroundColor = new[] { "#3f51b5", "#e53935", "#008C00" },
                BorderWidth = 8,
                HoverBorderColor = "#ffffff",
                BorderColor = "#ffffff"
            };

            _chartConfig.Data.Labels.AddRange(new[] { "Active", "Deaths", "Recovered" });
            _chartConfig.Data.Datasets.Add(_chartDataSet);
        }

        public static RenderFragment Get()
        {
            var c = new WorldCasesChartConnected();
            return ComponentConnector.Connect<WorldCasesChart, AppState, WorldCasesChartProps>(c.MapStateToProps, c.MapDispatchToProps, c.Init);
        }

        private async Task Init(IStore<AppState> store)
        {
            await store.Dispatch<FetchGlobalData>();
        }

        private void MapStateToProps(AppState state, WorldCasesChartProps props)
        {
            var stats = state?.Global;
            if (stats is null)
            {
                stats = new GlobalStatistics();
            }

            _chartDataSet.Data.Clear();
            _chartDataSet.Data.AddRange(new double[] { stats.Active, stats.Deaths, stats.Recovered });

            var total = stats.Cases;

            props.States = new[]
            {
                (CalculatePercentage(total, stats.Active), "#3f51b5", "Active"),
                (CalculatePercentage(total, stats.Recovered), "#008C00", "Recovered"),
                (CalculatePercentage(total, stats.Deaths), "#e53935", "Deaths")
            };
            props.ChartConfig = _chartConfig;
        }

        private void MapDispatchToProps(IStore<AppState> store, WorldCasesChartProps props)
        {
        }

        private float CalculatePercentage(int total, int value)
        {
            if (total == 0)
            {
                return 0;
            }

            return (float)Math.Round(value / (double)total, 2);
        }
    }

    public class WorldCasesChartProps
    {
        public PieConfig ChartConfig { get; set; }

        public (float percent, string color, string title)[] States { get; set; }
    }
}