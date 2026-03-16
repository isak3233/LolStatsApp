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
            builder.Services.AddHttpClient<ILolApiRepository, LolApiRepository>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["RiotApi:BaseUrl"]);
                client.DefaultRequestHeaders.Add("X-Riot-Token", builder.Configuration["RiotApi:ApiKey"]);
            });
            builder.Services.AddScoped<IUserDbRepository, UserDbRepository>();
            builder.Services.AddScoped<ILolDbRepository, LolDbRepository>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ISummonerService, SummonerService>();
            builder.Services.AddScoped<IMatchService, MatchService>();
            //Kör facade här eftersom Lolapi:en är väldigt splitrad (flera api enpoints och objekt skickas tillbaka) och facade ser till att våran Ui har vi så få metoder som möjligt och så enkla ihopslagna objekt då LolApi:en ger tillbaka väldigt mycket onödiga information
            builder.Services.AddScoped<ILolFacade, LolFacade>();
            //Använder singelton här eftersom vi vill bara att man ska kunna vara inloggad som en användare
            //Ser till också att när vi har loggat in så är vi inloggade på alla sidor
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IUserService, UserService>();

            //Kör facade här så att vi sätter ihop UserService och CurrentUserService så att våran ui kan bara kalla på (exempel) CreateUser och sen tar våran facade hand om att skapa kontot och sedan logga in användaren. 
            builder.Services.AddScoped<IUserFacade, UserFacade>();

            // Samma anleding som innan. Göra så att våran ui kan använda en service
            builder.Services.AddScoped<IAccountFacade, AccountFacade>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LolAccountOverViewModel>();
            builder.Services.AddTransient<LolAccountOverviewPage>();
            builder.Services.AddTransient<CreateUserPage>();
            builder.Services.AddTransient<CreateUserViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LinkAccountPage>();
            builder.Services.AddTransient<LinkAccountViewModel>();
            builder.Services.AddTransient<MyAccountPage>();
            builder.Services.AddTransient<MyAccountViewModel>();
            builder.Services.AddTransient<LiveGamePage>();
            builder.Services.AddTransient<LiveGameViewModel>();


            var connectionString = builder.Configuration["MongoDB:ConnectionString"];
            var databaseName = builder.Configuration["MongoDB:DatabaseName"];
            //Använder singelton här för att se till att applicationen bara har en anslutning till database (Bättre prestanda då flera anslutningar kan försämra prestandan)
            //Ser till att vi har EN Database State, Ser till att vi inte försöker skriva till ett värde två gånger eftersom det kan leda till att vi förlorar data
            //Exempel. Om vi läser ett värdet som heter count som är 100 två gånger samtidigt i våran kod och vi ska plusa på 1 så kommer vi skriva 101 och inte 102 eftersom vi har läst in värdet 100. Detta brukar kallas oftast för race condtition
            builder.Services.AddSingleton(LolStatsDbContext.GetInstance(connectionString, databaseName));
            


            var app = builder.Build();

            return app;
        }
    }
}
