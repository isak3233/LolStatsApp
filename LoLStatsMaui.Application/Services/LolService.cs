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

        public LolService(ILolRepository lolRepository)
        {
            _lolRepository = lolRepository;
        }
        public async Task<SummonerOverview> GetSummonerOverviewAsync(string gameName, string tagLine)
        {

            var account = await _lolRepository.GetLolAccount(gameName, tagLine);
            var accountRegion = await _lolRepository.GetAccountRegion(account.puuid);

            var summonerDataTask = _lolRepository.GetSummoner(account.puuid, accountRegion.region);
            var rankEntriesTask = _lolRepository.GetRankEntries(account.puuid, accountRegion.region);

            await Task.WhenAll(summonerDataTask, rankEntriesTask);

            var summonerData = await summonerDataTask;
            var rankEntries = await rankEntriesTask;
            return new SummonerOverview
            {
                Uuid = account.puuid,
                SummonerName = account.gameName,
                TagLine = account.tagLine,
                Level = summonerData.summonerLevel,
                ProfileIconId = summonerData.profileIconId,
                Region = RiotMapper.GetRegion(accountRegion.region),
                RawRegion = accountRegion.region,
                RankEntries = RiotMapper.GetRankEntries(rankEntries),

            };
        }
        public async Task<List<LolMatch>> GetLolMatchesAsync(MatchQueryRequest request)
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
