using Domain.Models.Enities.UserEnities;
using Domain.Models.Exceptions;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDbRepository _userDbRepository;
        private readonly ICurrentUserService _currentUserService;

        public UserService(IUserDbRepository userDbRepository, ICurrentUserService currentUserService)
        {
            _userDbRepository = userDbRepository;
            _currentUserService = currentUserService;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _userDbRepository.GetUserByUsernameAsync(username);
            if (user == null || user.Password != password)
            {
                throw new InvalidCredentialsException("Invalide User Credentials");
            }
            return user;
        }

        public async Task<User> CreateUserAsync(string username, string password)
        {
            if (await _userDbRepository.UsernameExistsAsync(username))
            {
                throw new UsernameExistException("Username already in use");
            }
            var user = new User { Username = username, Password = password };
            await _userDbRepository.CreateUserAsync(user);
            return user;
        }
        public async Task LinkLolAccountAsync(string puuid)
        {
            if (_currentUserService.CurrentUser == null)
            {
                throw new UserNotLoggedInException("User not signed in");
            }
            if (await _userDbRepository.IsLolAccountLinkedAsync(puuid))
            {
                throw new LolAccountAlreadyLinkedException("Lol account already linked");
            }

            var user = _currentUserService.CurrentUser;
            if (user.LinkedLolAccounts.Contains(puuid)) return;
            user.LinkedLolAccounts.Add(puuid);
            await _userDbRepository.UpdateUserAsync(user);
        }
        public async Task AddFollowedAccountAsync(string puuid)
        {
            if (_currentUserService.CurrentUser == null)
            {
                throw new UserNotLoggedInException("User not signed in");
            }
            var user = _currentUserService.CurrentUser;
            if (user.FollowedAccounts.Contains(puuid)) return;
            user.FollowedAccounts.Add(puuid);
            await _userDbRepository.UpdateUserAsync(user);
        }
        public async Task RemoveFollowedAccountAsync(string puuid)
        {
            if (_currentUserService.CurrentUser == null)
            {
                throw new UserNotLoggedInException("User not signed in");
            }
            var user = _currentUserService.CurrentUser;
            if (!user.FollowedAccounts.Contains(puuid)) return;
            user.FollowedAccounts.Remove(puuid);
            await _userDbRepository.UpdateUserAsync(user);
        }
        public async Task UnlinkLolAccountAsync(string puuid)
        {
            if (_currentUserService.CurrentUser == null)
            {
                throw new UserNotLoggedInException("User not signed in");
            }
            var user = _currentUserService.CurrentUser;
            if (!user.LinkedLolAccounts.Contains(puuid)) return;
            user.LinkedLolAccounts.Remove(puuid);
            await _userDbRepository.UpdateUserAsync(user);
        }
    }
}
