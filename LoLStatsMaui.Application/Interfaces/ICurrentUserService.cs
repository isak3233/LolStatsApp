using Domain.Models.Enities.UserEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface ICurrentUserService
    {
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
        void SetUser(User user);
        void Logout();

    }
}
