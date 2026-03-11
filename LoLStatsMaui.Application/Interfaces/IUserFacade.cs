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
        void Logout();
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
    }
}
