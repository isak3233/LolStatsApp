using Domain.Models.Enities.UserEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> LoginAsync(string username, string password);
        Task<User> CreateUserAsync(string username, string password);
        Task LinkLolAccountAsync(string puuid);
    }
}
