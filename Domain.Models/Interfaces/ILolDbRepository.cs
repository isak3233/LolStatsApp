using Domain.Models.Enities.LolEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Interfaces
{
    public interface ILolDbRepository
    {
        Task UpsertSummonerOverviewAsync(SummonerOverview summonerOverview);
        Task<SummonerOverview?> GetSummonerOverviewAsync(string puuid);
    }
}
