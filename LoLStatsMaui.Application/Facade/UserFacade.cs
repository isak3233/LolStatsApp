using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Enities.UserEnities;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Application.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoLStatsMaui.Application.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISummonerService _summonerService;
        private readonly ILolFacade _lolFacade;

        public UserFacade(IUserService userService, ICurrentUserService currentUserService, ISummonerService summonerService, ILolFacade lolFacade)
        {
            _userService = userService;
            _currentUserService = currentUserService;
            _summonerService = summonerService;
            _lolFacade = lolFacade;
        }


        public bool IsLoggedIn => _currentUserService.IsLoggedIn;
        public string? GetUsername() => _currentUserService.CurrentUser?.Username;

        public async Task LoginAsync(string username, string password)
        {
            var user = await _userService.LoginAsync(username, password);
            _currentUserService.SetUser(user);
        }

        public async Task CreateUserAsync(string username, string password)
        {
            var user = await _userService.CreateUserAsync(username, password);
            _currentUserService.SetUser(user);
        }
        public async Task<bool> VerifyAccountAsync(LolAccountMetaData accountMetaData, int profileIconId)
        {
            var summoner = await _summonerService.GetSummonerDto(accountMetaData);
            if (summoner.profileIconId == profileIconId)
            {
                await LinkLolAccountAsync(accountMetaData.Puuid);
                return true;
            }
            return false;
        }
        public async Task LinkLolAccountAsync(string puuid)
        {
            await _userService.LinkLolAccountAsync(puuid);
        }
        public async Task UnlinkLolAccountAsync(string puuid)
        {
            await _userService.UnlinkLolAccountAsync(puuid);
        }
        public async Task<List<SummonerOverview>> GetLinkedSummonerOverviewsAsync()
        {
            if (_currentUserService.CurrentUser == null) return new List<SummonerOverview>();
            var tasks = _currentUserService.CurrentUser.LinkedLolAccounts.Select(_lolFacade.GetSummonerOverview).ToList();
            return (await Task.WhenAll(tasks)).ToList();
        }

        public async Task<List<SummonerOverview>> GetFollowersSummonerOverviewsAsync()
        {
            if (_currentUserService.CurrentUser == null) return new List<SummonerOverview>();
            var tasks = _currentUserService.CurrentUser.FollowedAccounts.Select(_lolFacade.GetSummonerOverview).ToList();
            return (await Task.WhenAll(tasks)).ToList();
        }
        public bool GetFollowInfo(string puuid)
        {
            if(_currentUserService.CurrentUser == null) return false;
            return _currentUserService.CurrentUser.FollowedAccounts.Contains(puuid);
        }
        public async Task HandleFollow(string puuid)
        {
            if (_currentUserService.CurrentUser == null) return;
            var user = _currentUserService.CurrentUser;
            if(user.FollowedAccounts.Contains(puuid))
            {
                await _userService.RemoveFollowedAccountAsync(puuid);
            }
            else
            {
                await _userService.AddFollowedAccountAsync(puuid);
            }

        }
        public List<string> GetSearchHistory() => _currentUserService.CurrentUser.SearchHistory;
        public async Task AddSearchedLolName(string lolName) => await _userService.AddSearchedLolName(lolName);
        public void Logout()
        {
            _currentUserService.Logout();
        }
    }
}
