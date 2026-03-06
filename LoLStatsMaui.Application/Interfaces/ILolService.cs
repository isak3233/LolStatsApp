using Domain.Models.Enities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities;
using Domain.Models.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface ILolService
    {
        Task<LolProfileData> GetLolProfileAsync(string lolName);

    }
}
