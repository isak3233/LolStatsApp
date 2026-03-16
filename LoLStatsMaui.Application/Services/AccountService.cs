using Domain.Models.Enities.Requests;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILolApiRepository _lolApiRepository;
        private readonly ILolDbRepository _lolDbRepository;

        public AccountService(ILolApiRepository lolApiRepository, ILolDbRepository lolDbRepository)
        {
            _lolApiRepository = lolApiRepository;
            _lolDbRepository = lolDbRepository;
        }

        public async Task<LolAccountMetaData> GetLolAccountMetaData(string lolName, bool updateData = false)
        {
            if(string.IsNullOrEmpty(lolName) || !(lolName.Count(c => c == '#') == 1) || lolName.Split('#')[0].Length < 4 || lolName.Split('#')[1].Length < 3)
            {
                throw new ArgumentException();
            }
            string[] splitName = lolName.Split('#');
            string gameName = splitName[0];
            string tagLine = splitName[1];

            if(!updateData)
            {
                var maybeAccountData = await _lolDbRepository.GetLolAccountMetaDataAsync(lolName);
                if (maybeAccountData != null) return maybeAccountData;
            }

            var account = await _lolApiRepository.GetLolAccount(gameName, tagLine);
            var accountRegion = await _lolApiRepository.GetAccountRegion(account.puuid);

            var lolAccountMetaData = new LolAccountMetaData
            {
                Puuid = account.puuid,
                Region = accountRegion.region,
                GameName = account.gameName,
                TagLine = account.tagLine
            };
            _ = _lolDbRepository.UpsertLolAccountMetaDataAsync(lolAccountMetaData);
            return lolAccountMetaData;
        }
        public async Task<LolAccountMetaData> GetLolAccountMetaDataByPuuid(string puuid, bool updateData = false)
        {

            if (!updateData)
            {
                var maybeAccountData = await _lolDbRepository.GetLolAccountMetaDataByPuuidAsync(puuid);
                if (maybeAccountData != null) return maybeAccountData;
            }
            var accountTask = _lolApiRepository.GetLolAccount(puuid);
            var accountRegionTask = _lolApiRepository.GetAccountRegion(puuid);
            var account = await accountTask;
            var accountRegion = await accountRegionTask;

            var lolAccountMetaData = new LolAccountMetaData
            {
                Puuid = account.puuid,
                Region = accountRegion.region,
                GameName = account.gameName,
                TagLine = account.tagLine
            };

            _ = _lolDbRepository.UpsertLolAccountMetaDataAsync(lolAccountMetaData);

            return lolAccountMetaData;
        }
    }
}
