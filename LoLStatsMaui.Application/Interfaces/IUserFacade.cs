using Domain.Models.Enities.UserEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface IUserFacade
    {
        Task LoginAsync(string username, string password);
        Task CreateUserAsync(string username, string password);
        Task LinkLolAccountAsync(string puuid);
        Task UnlinkLolAccountAsync(string puuid);
        bool GetFollowInfo(string puuid);
        Task HandleFollow(string puuid);
        
        void Logout();
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
    }
}
