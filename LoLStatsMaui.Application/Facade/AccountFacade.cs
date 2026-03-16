using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Facade
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IAccountService _accountService;
        private readonly ISummonerService _summonerService;
        private readonly IUserFacade _userFacade;
        private readonly IMatchService _matchService;

        public AccountFacade(IAccountService accountService, ISummonerService summonerService, IUserFacade userFacade, IMatchService matchService)
        {
            _accountService = accountService;
            _summonerService = summonerService;
            _userFacade = userFacade;
            _matchService = matchService;
        }

        public string? GetUsername() => _userFacade.CurrentUser?.Username;

        public async Task<List<SummonerOverview>> GetSummonerOverviewsAsync()
        {
            if (_userFacade.CurrentUser == null) return new List<SummonerOverview>();
            var tasks = _userFacade.CurrentUser.LinkedLolAccounts.Select(GetSummonerOverview).ToList();
            return (await Task.WhenAll(tasks)).ToList();
        }

        public async Task<List<SummonerOverview>> GetFollowersSummonerOverviewsAsync()
        {
            if (_userFacade.CurrentUser == null) return new List<SummonerOverview>();
            var tasks = _userFacade.CurrentUser.FollowedAccounts.Select(GetSummonerOverview).ToList();
            return (await Task.WhenAll(tasks)).ToList();
        }

        public async Task UnlinkLolAccountAsync(string puuid) => await _userFacade.UnlinkLolAccountAsync(puuid);

        public async Task UnfollowAccountAsync(string puuid) => await _userFacade.HandleFollow(puuid);

        public async Task<LolAccountMetaData> GetLolAccountMetaDataAsync(string lolName) => await _accountService.GetLolAccountMetaData(lolName);

        public async Task<int> GetRandomProfileImageIdAsync(LolAccountMetaData accountMetaData)
        {
            var summoner = await _summonerService.GetSummonerDto(accountMetaData);
            var random = new Random();
            while (true)
            {
                var randomIconId = RiotMapper.StartingIconIds[random.Next(RiotMapper.StartingIconIds.Length)];
                if (summoner.profileIconId != randomIconId)
                    return randomIconId;
            }
        }

        public async Task<bool> VerifyAccountAsync(LolAccountMetaData accountMetaData, int profileIconId)
        {
            var summoner = await _summonerService.GetSummonerDto(accountMetaData);
            if (summoner.profileIconId == profileIconId)
            {
                await _userFacade.LinkLolAccountAsync(accountMetaData.Puuid);
                return true;
            }
            return false;
        }

        private async Task<SummonerOverview> GetSummonerOverview(string puuid)
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
