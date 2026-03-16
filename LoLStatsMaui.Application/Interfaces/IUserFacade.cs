using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Enities.UserEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface IUserFacade
    {
        Task LoginAsync(string username, string password);
        Task CreateUserAsync(string username, string password);
        Task<bool> VerifyAccountAsync(LolAccountMetaData accountMetaData, int profileIconId);
        Task<List<SummonerOverview>> GetLinkedSummonerOverviewsAsync();
        Task<List<SummonerOverview>> GetFollowersSummonerOverviewsAsync();

        Task LinkLolAccountAsync(string puuid);
        Task UnlinkLolAccountAsync(string puuid);
        bool GetFollowInfo(string puuid);
        Task HandleFollow(string puuid);
        List<string> GetSearchHistory();
        Task AddSearchedLolName(string lolName);


        void Logout();
        string? GetUsername();
        bool IsLoggedIn { get; }
    }
}
