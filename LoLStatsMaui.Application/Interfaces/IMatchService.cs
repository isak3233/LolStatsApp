using Domain.Models.Entities;
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
