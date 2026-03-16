using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface ILolFacade
    {
        Task<LolProfileData> GetLolProfileAsync(string lolName, bool update = false);
        Task<List<LolMatch>> GetLolMatches(MatchQueryRequest matchRequest);
        Task<int> GetRandomProfileImageIdAsync(LolAccountMetaData accountMetaData);
        Task<SummonerOverview> GetSummonerOverview(string puuid);
        Task<LolAccountMetaData> GetLolAccountMetaDataAsync(string lolName);


    }
}
