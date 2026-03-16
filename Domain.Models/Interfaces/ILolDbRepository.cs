using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Interfaces
{
    public interface ILolDbRepository
    {
        Task UpsertSummonerOverviewAsync(SummonerOverview summonerOverview);
        Task<SummonerOverview?> GetSummonerOverviewAsync(string puuid);
        Task UpsertLolAccountMetaDataAsync(LolAccountMetaData lolAccountMetaData);
        Task<LolAccountMetaData?> GetLolAccountMetaDataAsync(string lolName);
        Task<LolAccountMetaData?> GetLolAccountMetaDataByPuuidAsync(string lolName);
    }
}
