using Domain.Models.Enities.Dto;
using Domain.Models.Enities.LolEnities;
using Domain.Models.Entities.Dto;
using Domain.Models.EntitiesDto;
using LoLStatsMaui.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.Infrastructure.Constants
{

    public static class RiotMapper
    {
        // Region
        public static readonly Dictionary<string, string> RegionMap = RegionMapper.RegionMap;
        public static readonly Dictionary<string, string> RegionToRoutingMap = RegionMapper.RegionToRoutingMap;
        public static string GetRouting(string region) => RegionMapper.GetRouting(region);
        public static string GetRegion(string region) => RegionMapper.GetRegion(region);

        // Queue
        public static readonly Dictionary<string, string> QueueTypeMap = QueueMapper.QueueTypeMap;
        public static readonly Dictionary<int, string> QueueIdMap = QueueMapper.QueueIdMap;
        public static string GetQueueType(string queueType) => QueueMapper.GetQueueType(queueType);
        public static string GetQueueType(int queueId) => QueueMapper.GetQueueType(queueId);
        public static int? GetQueueId(string queueType) => QueueMapper.GetQueueId(queueType);

        // Champion
        public static string GetChampionName(long championId) => ChampionMapper.GetChampionName(championId);

        // ProfileIcon
        public static readonly int[] StartingIconIds = ProfileIconMapper.StartingIconIds;

        // Rank
        public static List<RankEntry> GetRankEntries(List<RankEntryDto> rankEntriesDto) => RankMapper.GetRankEntries(rankEntriesDto);

        // Match
        public static LolMatch Map(LolMatchDto dto, string puuid) => MatchMapper.Map(dto, puuid);
        public static CurrentLolMatch Map(CurrentGameInfoDto dto, string puuid) => MatchMapper.Map(dto, puuid);
    }



}
