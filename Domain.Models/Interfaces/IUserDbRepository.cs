using Domain.Models.Enities.UserEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Interfaces
{
    public interface IUserDbRepository
    {
        public Task CreateUserAsync(User user);
        Task<bool> UsernameExistsAsync(string username);
        Task<User?> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(User user);
        Task<bool> IsLolAccountLinkedAsync(string puuid);
    }
}
