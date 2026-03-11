using Domain.Models.Entities;
using Domain.Models.Entities.Dto;
using Domain.Models.Entities.Requests;
using Domain.Models.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Text;
namespace Domain.Models.Interfaces
{
    public interface ILolApiRepository
    {
        Task<LolAccountDto> GetLolAccount(string gameName, string tagLine);
        Task<AccountRegionDto> GetAccountRegion(string puuid);
        Task<SummonerDto> GetSummoner(string puuid, string region);
        Task<List<RankEntryDto>> GetRankEntries(string puuid, string region);
        Task<List<string>> GetLolMatchesId(MatchQueryRequest request);
        Task<LolMatchDto> GetLolMatch(string matchId, string region);

    }
}
