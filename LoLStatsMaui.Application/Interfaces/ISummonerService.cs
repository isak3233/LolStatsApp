using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface ISummonerService
    {
        Task<SummonerOverview> GetSummonerOverviewAsync(LolAccountMetaData accountMetaData);
    }
}
