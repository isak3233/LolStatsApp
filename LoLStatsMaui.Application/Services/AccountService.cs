using Domain.Models.Enities.Requests;
using Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class AccountService
    {
        private readonly ILolRepository _lolRepository;

        public AccountService(ILolRepository lolRepository)
        {
            _lolRepository = lolRepository;
        }

        public async Task<LolAccountMetaData> GetLolAccountMetaData(string lolName)
        {
            string[] splitName = lolName.Split('#');
            string gameName = splitName[0];
            string tagLine = splitName[1];
            var account = await _lolRepository.GetLolAccount(gameName, tagLine);
            var accountRegion = await _lolRepository.GetAccountRegion(account.puuid);
            return new LolAccountMetaData
            {
                Puuid = account.puuid,
                Region = accountRegion.region,
                GameName = account.gameName,
                TagLine = account.tagLine
            };
        }
    }
}
