using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Dto;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Constants;
using LoLStatsMaui.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly ILolApiRepository _lolApiRepository;
        private readonly ILolDbRepository _lolDbRepository;

        public SummonerService(ILolApiRepository lolApiRepository, ILolDbRepository lolDbRepository)
        {
            _lolApiRepository = lolApiRepository;
            _lolDbRepository = lolDbRepository;
        }
        public async Task<SummonerDto> GetSummonerDto(LolAccountMetaData accountMetaData)
        {
            return await _lolApiRepository.GetSummoner(accountMetaData.Puuid, accountMetaData.Region);
        }
        public async Task<SummonerOverview> GetSummonerOverviewAsync(LolAccountMetaData accountMetaData, bool updateSummonerOverview = false)
        {
            if(updateSummonerOverview)
            {
                var updatedSummonerOverview = await GetUpdatedSummonerOverview(accountMetaData);
                return updatedSummonerOverview;
            } else
            {
                var maybeSummonerOverview = await _lolDbRepository.GetSummonerOverviewAsync(accountMetaData.Puuid);
                if (maybeSummonerOverview != null) return maybeSummonerOverview;

                var updatedSummonerOverview = await GetUpdatedSummonerOverview(accountMetaData);
                return updatedSummonerOverview;
            }
            
        }

        private async Task<SummonerOverview> GetUpdatedSummonerOverview(LolAccountMetaData accountMetaData)
        {
            var summonerDataTask = _lolApiRepository.GetSummoner(accountMetaData.Puuid, accountMetaData.Region);
            var rankEntriesTask = _lolApiRepository.GetRankEntries(accountMetaData.Puuid, accountMetaData.Region);
            var rankEntries = await rankEntriesTask;
            var summonerData = await summonerDataTask;

            var summonerOverview = new SummonerOverview
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
            _ = _lolDbRepository.UpsertSummonerOverviewAsync(summonerOverview);
            return summonerOverview;
        }
       
    }
}
