using BlazorState.Redux.Blazor;
using BlazorState.Redux.Interfaces;
using ChartJs.Blazor.ChartJS.BarChart;
using ChartJs.Blazor.ChartJS.BarChart.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Wrappers;
using ChartJs.Blazor.Util;
using COVID19Stats.State;
using COVID19Stats.State.Actions.Async;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Stats.Components
{
    public class WorldByCountryChartConnected
    {
        private BarConfig _barChartConfig = new BarConfig
        {
            Options = new BarOptions
            {
                Legend = new Legend
                {
                    Display = false
                },
                Responsive = true,
                MaintainAspectRatio = false,
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
                },
                Scales = new BarScales
                {
                    XAxes = new List<CartesianAxis>
                    {
                        new BarCategoryAxis
                        {
                            BarThickness = BarThickness.Flex,
                            Stacked = false,
                        }
                    }
                }
            }
        };

        public static RenderFragment Get()
        {
            var c = new WorldByCountryChartConnected();
            return ComponentConnector.Connect<WorldByCountryChart, AppState, WorldByCountryChartProps>(c.MapStateToProps, c.MapDispatchToProps, c.Init);
        }

        private async Task Init(IStore<AppState> store)
        {
            await store.Dispatch<FetchCountriesData>();
        }

        private void MapStateToProps(AppState state, WorldByCountryChartProps props)
        {
            _barChartConfig.Data.Datasets.Clear();
            _barChartConfig.Data.Labels.Clear();

            props.ChartConfig = _barChartConfig;

            var stats = state?.CountriesData;
            if (stats == null)
            {
                return;
            }

            var data = stats.ByCountry.OrderByDescending(c => c.Cases).Take(20).ToArray();
            _barChartConfig.Data.Labels.AddRange(data.Select(c => c.Country));

            var activeDataSet = new BarDataset<Int32Wrapper>
            {
                Label = "Active",
                BackgroundColor = "#3f51b5",
                BorderColor = ColorUtil.ColorString(0, 0, 0),
                BorderWidth = 1
            };

            activeDataSet.AddRange(data.Select(c => c.Active).Wrap());
            _barChartConfig.Data.Datasets.Add(activeDataSet);

            var recoveredDataSet = new BarDataset<Int32Wrapper>
            {
                Label = "Recovered",
                BackgroundColor = "#008C00",
                BorderColor = ColorUtil.ColorString(0, 0, 0),
                BorderWidth = 1
            };

            recoveredDataSet.AddRange(data.Select(c => c.Recovered).Wrap());
            _barChartConfig.Data.Datasets.Add(recoveredDataSet);

            var deathDataSet = new BarDataset<Int32Wrapper>
            {
                Label = "Deaths",
                BackgroundColor = "#e53935",
                BorderColor = ColorUtil.ColorString(0, 0, 0),
                BorderWidth = 1
            };

            deathDataSet.AddRange(data.Select(c => c.Deaths).Wrap());
            _barChartConfig.Data.Datasets.Add(deathDataSet);
        }

        private void MapDispatchToProps(IStore<AppState> store, WorldByCountryChartProps props)
        {
        }
    }

    public class WorldByCountryChartProps
    {
        public BarConfig ChartConfig { get; set; }
    }
}
