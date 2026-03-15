using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface IAccountFacade
    {
        string? GetUsername();
        Task<List<SummonerOverview>> GetSummonerOverviewsAsync();
        Task<List<SummonerOverview>> GetFollowersSummonerOverviewsAsync();
        Task UnfollowAccountAsync(string puuid);
        Task UnlinkLolAccountAsync(string puuid);

        Task<LolAccountMetaData> GetLolAccountMetaDataAsync(string lolName);
        Task<int> GetRandomProfileImageIdAsync(LolAccountMetaData accountMetaData);
        Task<bool> VerifyAccountAsync(LolAccountMetaData accountMetaData, int profileIconId);
    }
}
