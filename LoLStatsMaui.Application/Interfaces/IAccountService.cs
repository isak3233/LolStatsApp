using Domain.Models.Enities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface IAccountService
    {
        Task<LolAccountMetaData> GetLolAccountMetaData(string lolName);
        Task<LolAccountMetaData> GetLolAccountMetaDataByPuuid(string puuid);
    }
}
