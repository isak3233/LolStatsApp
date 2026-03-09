using CommunityToolkit.Maui;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Facade;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Application.Services;
using LoLStatsMaui.Infrastructure.Repositories;
using LoLStatsMaui.ViewModels;
using LoLStatsMaui.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LoLStatsMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
            builder.Configuration.AddUserSecrets<App>();
#endif
            builder.Services.AddHttpClient<ILolRepository, LolApiRepository>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["RiotApi:BaseUrl"]);
                client.DefaultRequestHeaders.Add("X-Riot-Token", builder.Configuration["RiotApi:ApiKey"]);
            });
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<SummonerService>();
            builder.Services.AddScoped<MatchService>();
            builder.Services.AddScoped<ILolFacade, LolFacade>();
            
            builder.Services.AddTransient<LolAccountOverViewModel>();
            builder.Services.AddTransient<LolAccountOverviewPage>();
            var app = builder.Build();

            return app;
        }
    }
}
