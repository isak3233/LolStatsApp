using Domain.Models.Enities;
using Domain.Models.Entities;
using Domain.Models.Entities.Requests;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Application.Services;
using System;
using System.Collections.Generic;
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

        public async Task<LolProfileData> GetLolProfileAsync(string lolName)
        {
            var metaData = await _accountService.GetLolAccountMetaData(lolName);
            var summonerTask = _summonerService.GetSummonerOverviewAsync(metaData);
            
            return new LolProfileData
            {
                SummonerOverview = await summonerTask,
                Matches = new List<LolMatch>()
            };
        }
        public async Task<List<LolMatch>> GetLolMatches(MatchQueryRequest matchRequest)
        {
            var matches = await _matchService.GetLolMatchesAsync(matchRequest);
            return matches;
        }
    }
}
