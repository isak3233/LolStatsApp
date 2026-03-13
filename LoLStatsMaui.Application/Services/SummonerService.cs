using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Dto;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly ILolApiRepository _lolRepository;

        public SummonerService(ILolApiRepository lolRepository)
        {
            _lolRepository = lolRepository;
        }
        public async Task<SummonerDto> GetSummonerDto(LolAccountMetaData accountMetaData)
        {
            return await _lolRepository.GetSummoner(accountMetaData.Puuid, accountMetaData.Region);
        }
        public async Task<SummonerOverview> GetSummonerOverviewAsync(LolAccountMetaData accountMetaData)
        {
            var summonerDataTask = _lolRepository.GetSummoner(accountMetaData.Puuid, accountMetaData.Region);
            var rankEntriesTask = _lolRepository.GetRankEntries(accountMetaData.Puuid, accountMetaData.Region);
            var rankEntries = await rankEntriesTask;
            var summonerData = await summonerDataTask;
            return new SummonerOverview
            {
                Uuid = summonerData.puuid,
                SummonerName = accountMetaData.GameName,
                TagLine = accountMetaData.TagLine,
                Level = summonerData.summonerLevel,
                ProfileIconString = $"ProfileIcons/{summonerData.profileIconId}.png",
                Region = RiotMapper.GetRegion(accountMetaData.Region),
                RawRegion = accountMetaData.Region,
                RankEntries = RiotMapper.GetRankEntries(rankEntries),
            };
        }
    }
}
