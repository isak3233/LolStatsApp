using Domain.Models.Enities.UserEnities;
using LoLStatsMaui.Application.Interfaces;
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

        public UserFacade(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        public User? CurrentUser => _currentUserService.CurrentUser;
        public bool IsLoggedIn => _currentUserService.IsLoggedIn;

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
        public async Task LinkLolAccountAsync(string puuid)
        {
            await _userService.LinkLolAccountAsync(puuid);
        }
        public async Task UnlinkLolAccountAsync(string puuid)
        {
            await _userService.UnlinkLolAccountAsync(puuid);
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
