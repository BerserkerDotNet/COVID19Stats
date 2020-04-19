using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using COVID19Stats.Services;
using BlazorStorage.Extensions;
using BlazorState.Redux.Extensions;
using COVID19Stats.State;
using Skclusive.Material.Layout;
using Skclusive.Material.Component;
using System.Net.Http;
using System;
using COVID19Stats.State.Actions.Async;
using COVID19Stats.State.Reducers;
using COVID19Stats.Models;
using BlazorState.Redux.Storage;

namespace COVID19Stats
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<CovidTrackingClient>();
            builder.Services.AddSingleton<InMemoryCache>();
            builder.Services.AddSingleton<ICovidClient, NOVELCovidClient>();

            builder.Services.AddStorage();
            builder.Services.AddReduxStore<AppState>(cfg =>
            {
                cfg.UseLocalStorage();
                cfg.RegisterActionsFromAssemblyContaining<FetchGlobalData>();
                cfg.Map<GlobalDataReducer, GlobalStatistics>(s => s.Global);
                cfg.Map<CountriesDataReducer, CountriesData>(s => s.CountriesData);

            });

            builder.Services.TryAddLayoutServices
            (
                new LayoutConfigBuilder()
                    .WithIsServer(false)
                    .WithIsPreRendering(false)
                    .WithResponsive(true)
                    .Build()
            );

            await builder.Build().RunAsync();
        }
    }
}
