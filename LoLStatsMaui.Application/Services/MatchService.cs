using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Requests;
using Domain.Models.EntitiesDto;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly ILolApiRepository _lolRepository;

        public MatchService(ILolApiRepository lolRepository)
        {
            _lolRepository = lolRepository;
        }
        public async Task<CurrentLolMatch?> GetCurrentMatch(LolAccountMetaData lolAccountMetaData)
        {
            var matchData = await _lolRepository.GetCurrentMatch(lolAccountMetaData.Puuid, lolAccountMetaData.Region);
            if (matchData == null) return null;
            return RiotMapper.Map(matchData, lolAccountMetaData.Puuid);
        }
        public async Task<List<LolMatch>> GetLolMatchesAsync(MatchQueryRequest request)
        {
            var routing = RiotMapper.GetRouting(request.Region);
            request.Region = routing;
            var matchIds = await _lolRepository.GetLolMatchesId(request);
            var route = RiotMapper.GetRouting(request.Region);
            List<Task<LolMatchDto>> matchDtoTasks = matchIds
                .Select(matchId => _lolRepository.GetLolMatch(matchId, route))
                .ToList();
            LolMatchDto[] matchesDto = await Task.WhenAll(matchDtoTasks);
            return matchesDto.Select(match => RiotMapper.Map(match, request.Uuid)).ToList();
        }
    }
}
