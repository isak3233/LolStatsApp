using Domain.Models.Enities.UserEnities;
using LoLStatsMaui.Application.Interfaces;
using System;
using System.Collections.Generic;
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

        public void Logout()
        {
            _currentUserService.Logout();
        }
    }
}
