using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Requests;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Application.Services;
using LoLStatsMaui.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
namespace LoLStatsMaui.Application.Facade
{
    public class LolFacade : ILolFacade
    {
        private readonly IAccountService _accountService;
        private readonly ISummonerService _summonerService;
        private readonly IMatchService _matchService;

        public LolFacade(IAccountService accountService, ISummonerService summonerService, IMatchService matchService)
        {
            _accountService = accountService;
            _summonerService = summonerService;
            _matchService = matchService;
        }

        public async Task<LolProfileData> GetLolProfileAsync(string lolName, bool update = false)
        {

            var metaData = await _accountService.GetLolAccountMetaData(lolName, update);

            var summonerTask = _summonerService.GetSummonerOverviewAsync(metaData, update);

            var currentMatchTask = _matchService.GetCurrentMatch(metaData);


            var summoner = await summonerTask;
            var currentMatch = await currentMatchTask;
            summoner.CurrentLolMatch = await currentMatchTask;


            return new LolProfileData
            {
                SummonerOverview = summoner,
                Matches = new List<LolMatch>()
            };
        }
        public async Task<List<LolMatch>> GetLolMatches(MatchQueryRequest matchRequest)
        {
            var matches = await _matchService.GetLolMatchesAsync(matchRequest);
            return matches;
        }
        public async Task<int> GetRandomProfileImageIdAsync(LolAccountMetaData accountMetaData)
        {
            var summoner = await _summonerService.GetSummonerDto(accountMetaData);
            var random = new Random();
            while (true)
            {
                var randomIconId = RiotMapper.StartingIconIds[random.Next(RiotMapper.StartingIconIds.Length)];
                if (summoner.profileIconId != randomIconId)
                {
                    return randomIconId;
                }

            }
        }
        public async Task<LolAccountMetaData> GetLolAccountMetaDataAsync(string lolName) => await _accountService.GetLolAccountMetaData(lolName);
        public async Task<SummonerOverview> GetSummonerOverview(string puuid)
        {
            var accountMetaData = await _accountService.GetLolAccountMetaDataByPuuid(puuid);

            var currentMatchTask = _matchService.GetCurrentMatch(accountMetaData);
            var summonerOverviewTask = _summonerService.GetSummonerOverviewAsync(accountMetaData);

            var currentMatch = await currentMatchTask;
            var summonerOverview = await summonerOverviewTask;
            summonerOverview.CurrentLolMatch = currentMatch;
            return summonerOverview;
        }
    }
}
