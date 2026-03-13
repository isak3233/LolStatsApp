using Domain.Models.Enities.Requests;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Facade
{
    public class LinkLolAccountFacade : ILinkLolAccountFacade
    {
        private IAccountService _accountService;
        private ISummonerService _summonerService;
        private IUserService _userService;
        public LinkLolAccountFacade(IAccountService accountService, ISummonerService summonerService, IUserService userService)
        {
            _accountService = accountService;
            _summonerService = summonerService;
            _userService = userService;
        }

        public async Task<LolAccountMetaData> GetLolAccountMetaDataAsync(string lolName)
        {
            return await _accountService.GetLolAccountMetaData(lolName);
        }
        public async Task<int> GetRandomProfileImageIdAsync(LolAccountMetaData accountMetaData)
        {
            var summoner = await _summonerService.GetSummonerDto(accountMetaData);
            var random = new Random();
            while(true)
            {
                var randomIconId = RiotMapper.StartingIconIds[random.Next(RiotMapper.StartingIconIds.Length)];
                if (summoner.profileIconId != randomIconId)
                {
                    return randomIconId;
                } 
            }
            
        }
        public async Task<bool> VerifyAccountAsync(LolAccountMetaData accountMetaData, int profileIconId)
        {
            var summoner = await _summonerService.GetSummonerDto(accountMetaData);
            if(summoner.profileIconId == profileIconId)
            {
                await _userService.LinkLolAccountAsync(accountMetaData.Puuid);
                return true;
            } else
            {
                return false;
            }
        }
    }
}
