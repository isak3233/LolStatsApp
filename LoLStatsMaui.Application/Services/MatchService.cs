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
        private readonly ILolDbRepository _lolDbRepository;

        public MatchService(ILolApiRepository lolApiRepository, ILolDbRepository lolDbRepository)
        {
            _lolApiRepository = lolApiRepository;
            _lolDbRepository = lolDbRepository;
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

            var dbMatchesDto = await _lolDbRepository.GetLolMatches(matchIds);

            //Lägger in targetPlayerInfo
            var dbMatches = dbMatchesDto.Select(m => MatchMapper.MapDbMatch(m, request.Uuid)).ToList();

            var dbMatchIds = dbMatches.Select(m => m.MatchId).ToList();
            var missingMatchIds = matchIds.Where(id => !dbMatchIds.Contains(id)).ToList();

            var apiMatchDtoTasks = missingMatchIds.Select(id => _lolApiRepository.GetLolMatch(id, routing)).ToList();
            var apiMatchesDto = await Task.WhenAll(apiMatchDtoTasks);

            var apiMatches = apiMatchesDto.Select(m => MatchMapper.Map(m, request.Uuid)).ToList();

            _ = _lolDbRepository.UpsertLolMatchesAsync(apiMatches);

            //Sätter ihop listorna och soreterar dom efter ids
            var allMatches = apiMatches.Concat(dbMatches)
                .OrderByDescending(m => long.Parse(m.MatchId.Split('_')[1]))
                .ToList();

            return allMatches;



        }
    }
}
