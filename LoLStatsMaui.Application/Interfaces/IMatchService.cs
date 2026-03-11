using Domain.Models.Enities.LolEnities;
using Domain.Models.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface IMatchService
    {
        Task<List<LolMatch>> GetLolMatchesAsync(MatchQueryRequest request);
    }
}
