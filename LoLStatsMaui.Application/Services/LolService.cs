using Domain.Models.Enities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities;
using Domain.Models.Entities.Requests;
using Domain.Models.EntitiesDto;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class LolService : ILolService
    {
        private readonly ILolRepository _lolRepository;

        public async Task<LolProfileData> GetLolProfileAsync(string lolName)
        {
            var lolAccountMetaData = await GetLolAccountMetaData(lolName);
            var summonerOverviewTask = GetSummonerOverviewAsync(lolAccountMetaData);
            var matchesTask = GetLolMatchesAsync(new MatchQueryRequest
            {
                Uuid = lolAccountMetaData.Puuid,
                Region = lolAccountMetaData.Region,
                Count = 5,
            });
            return new LolProfileData
            {
                SummonerOverview = await summonerOverviewTask,
                Matches = await matchesTask
            };
        }
        public LolService(ILolRepository lolRepository)
        {
            _lolRepository = lolRepository;
        }
        private async Task<LolAccountMetaData> GetLolAccountMetaData(string lolName)
        {
            string[] splitName = lolName.Split('#');
            string gameName = splitName[0];
            string tagLine = splitName[1];
            var account = await _lolRepository.GetLolAccount(gameName, tagLine);
            var accountRegion = await _lolRepository.GetAccountRegion(account.puuid);
            return new LolAccountMetaData
            {
                Puuid = account.puuid,
                Region = accountRegion.region,
                GameName = account.gameName,
                TagLine = account.tagLine
            };
        }
        private async Task<SummonerOverview> GetSummonerOverviewAsync(LolAccountMetaData accountMetaData)
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
                ProfileIconId = summonerData.profileIconId,
                Region = RiotMapper.GetRegion(accountMetaData.Region),
                RawRegion = accountMetaData.Region,
                RankEntries = RiotMapper.GetRankEntries(rankEntries),

            };
        }
        private async Task<List<LolMatch>> GetLolMatchesAsync(MatchQueryRequest request)
        {
            var routing = RiotMapper.GetRouting(request.Region);
            request.Region = routing;
            var matchIds = await _lolRepository.GetLolMatchesId(request);


            List<Task<LolMatchDto>> matchDtoTasks = new List<Task<LolMatchDto>>();
            var route = RiotMapper.GetRouting(request.Region);
            foreach (var matchId in matchIds)
            {
                matchDtoTasks.Add(_lolRepository.GetLolMatch(matchId, route));
            }

            LolMatchDto[] matchesDto = await Task.WhenAll(matchDtoTasks);

            List<LolMatch> matches = new List<LolMatch>();
            foreach (var match in matchesDto)
            {
                matches.Add(RiotMapper.Map(match, request.Uuid));
            }
            return matches;
        }
    }
}
