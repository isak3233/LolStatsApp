using Domain.Models.Enities.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Interfaces
{
    public interface ILinkLolAccountFacade
    {
        Task<LolAccountMetaData> GetLolAccountMetaDataAsync(string lolName);
        Task<int> GetRandomProfileImageIdAsync(LolAccountMetaData accountMetaData);
        Task<bool> VerifyAccountAsync(LolAccountMetaData accountMetaData, int profileIconId);
    }
}
