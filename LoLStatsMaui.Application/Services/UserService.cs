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

        public UserService(IUserDbRepository userDbRepository)
        {
            _userDbRepository = userDbRepository;
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
    }
}
