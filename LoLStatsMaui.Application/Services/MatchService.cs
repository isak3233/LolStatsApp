using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Requests;
using Domain.Models.EntitiesDto;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Application.Mappers;
using LoLStatsMaui.Infrastructure.Constants;
using LoLStatsMaui.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly ILolApiRepository _lolApiRepository;

        public MatchService(ILolApiRepository lolApiRepository)
        {
            _lolApiRepository = lolApiRepository;
        }
        public async Task<CurrentLolMatch?> GetCurrentMatch(LolAccountMetaData lolAccountMetaData)
        {
            var matchData = await _lolApiRepository.GetCurrentMatch(lolAccountMetaData.Puuid, lolAccountMetaData.Region);
            if (matchData == null) return null;
            return RiotMapper.Map(matchData, lolAccountMetaData.Puuid);
        }
        public async Task<List<LolMatch>> GetLolMatchesAsync(MatchQueryRequest request)
        {
            
            var routing = RegionMapper.GetRouting(request.Region);
            request = request with { Region = routing };
            var matchIds = await _lolApiRepository.GetLolMatchesId(request);
            var matchDtoTasks = matchIds.Select(id => _lolApiRepository.GetLolMatch(id, routing)).ToList();
            var matchesDto = await Task.WhenAll(matchDtoTasks);
            return matchesDto.Select(m => MatchMapper.Map(m, request.Uuid)).ToList();
            
            
        }
    }
}
