using Domain.Models.Enities.LolEnities;
using Domain.Models.Entities.Dto;
using Domain.Models.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.Infrastructure.Constants
{
    public static class RiotMapper
    {
        public static readonly Dictionary<string, string> QueueTypeMap = new()
        {
            { "RANKED_SOLO_5x5", "Ranked Solo/Duo" },
            { "RANKED_FLEX_SR", "Ranked Flex" }
        };
        public static readonly Dictionary<int, string> QueueIdMap = new()
        {
            //https://static.developer.riotgames.com/docs/lol/queues.json INFO
            { 420, "Ranked Solo/Duo" },
            { 440, "Ranked Flex" },
            { 2400, "ARAM: Mayhem" },
            { 100, "ARAM" },
            { 450, "ARAM" },
            { 2300, "BRAWL" },
            { 1700, "ARENA"},
            { 1710, "ARENA"},
            { 870, "Co-op vs Ai"},
            { 880, "Co-op vs Ai"},
            { 890, "Co-op vs Ai"},
            { 900,  "ARURF"},
            { 700,  "CLASH"},
            { 720, "CLASH" },
            { 490, "Normal Quickplay" },
            { 480, "Swiftplay" },
            { 430, "Blind Pick" },
            { 400, "Normal"},
            
        };

        public static readonly Dictionary<string, string> RegionMap = new()
        {
                { "euw1", "EUW" },
                { "eun1", "EUNE" },
                { "na1", "NA" },
                { "kr", "KR" },
                { "br1", "BR" },
                { "la1", "LAN" },
                { "la2", "LAS" },
                { "oc1", "OCE" },
                { "tr1", "TR" },
                { "ru", "RU" },
                { "jp1", "JP" }
        };
        public static readonly Dictionary<string, string> RegionToRoutingMap = new()
        {
            { "na1", "americas" },
            { "br1", "americas" },
            { "la1", "americas" },
            { "la2", "americas" },
            { "kr", "asia" },
            { "jp1", "asia" },
            { "eun1", "europe" },
            { "euw1", "europe" },
            { "me1", "europe" },
            { "tr1", "europe" },
            { "ru", "europe" },
            { "oc1", "sea" },
            { "sg2", "sea" },
            { "tw2", "sea" },
            { "vn2", "sea" }
        };
        public static string GetRouting(string region)
        {
            return RegionToRoutingMap.TryGetValue(region.ToLower(), out var routing) ? routing : "europe";
        }
        public static string GetRegion(string region)
        {
            return RegionMap.TryGetValue(region.ToLower(), out var readable) ? readable : region.ToUpper();
        }
        public static string GetQueueType(string queueType)
        {
            return QueueTypeMap.TryGetValue(queueType, out var readable) ? readable : queueType;
        }
        public static string GetQueueType(int queueId)
        {
            return QueueIdMap.TryGetValue(queueId, out var readable) ? readable : "Gamemode";
        }
        public static List<RankEntry> GetRankEntries(List<RankEntryDto> rankEntriesDto)
        {
            List<RankEntry> rankEntries = new List<RankEntry>
            {
                new RankEntry { Tier = "Unranked", QueueType = QueueTypeMap["RANKED_SOLO_5x5"], ImageString = "RankIcons/Rank=Unranked.png" },
                new RankEntry { Tier = "Unranked", QueueType = QueueTypeMap["RANKED_FLEX_SR"], ImageString = "RankIcons/Rank=Unranked.png" }
            };

            foreach (var rankEntryDto in rankEntriesDto)
            {
                int index = rankEntryDto.queueType == "RANKED_SOLO_5x5" ? 0 :
                            rankEntryDto.queueType == "RANKED_FLEX_SR" ? 1 : -1;

                if (index == -1) continue;

                int winRate = (int)Math.Round((double)rankEntryDto.wins / (rankEntryDto.wins + rankEntryDto.losses) * 100);
                string[] noRankTiers = { "MASTER", "GRANDMASTER", "CHALLENGER" };
                string rank = noRankTiers.Contains(rankEntryDto.tier) ? "" : rankEntryDto.rank;

                rankEntries[index] = new RankEntry
                {
                    QueueType = GetQueueType(rankEntryDto.queueType),
                    Tier = rankEntryDto.tier,
                    Rank = rank,
                    LeaguePoints = rankEntryDto.leaguePoints,
                    Wins = rankEntryDto.wins,
                    Losses = rankEntryDto.losses,
                    WinRate = winRate,
                    ImageString = $"RankIcons/Rank={rankEntryDto.tier}.png"
                };
            }
            return rankEntries;
        }
        public static LolMatch Map(LolMatchDto dto, string puuid)
        {
            var targetPlayer = MapPlayer(dto.Info.Participants.First(p => p.Puuid == puuid));
            return new LolMatch
            {
                TargetPlayer = targetPlayer,
                Players = dto.Info.Participants.Select(p => MapPlayer(p, targetPlayer)).ToList(),
                QueueType = GetQueueType(dto.Info.QueueId),
                GameDuration = (int)(dto.Info.GameDuration / 60),
            };
        }
        
        private static LolMatchPlayer MapPlayer(ParticipantDto participant, LolMatchPlayer? targetPlayer = null)
        {
            return new LolMatchPlayer
            {
                IsTargetPlayer = targetPlayer?.GameName == participant.RiotIdGameName,
                GameName = participant.RiotIdGameName,
                TagLine = participant.RiotIdTagline,
                Win = participant.Win,
                ChampLevel = participant.ChampLevel,
                Assists = participant.Assists,
                ChampionId = participant.ChampionId,
                ChampionName = participant.ChampionName,
                ChampionTransform = participant.ChampionTransform,
                Deaths = participant.Deaths,
                Item0 = participant.Item0,
                Item1 = participant.Item1,
                Item2 = participant.Item2,
                Item3 = participant.Item3,
                Item4 = participant.Item4,
                Item5 = participant.Item5,
                Item6 = participant.Item6,
                Kills = participant.Kills,
                TeamId = participant.TeamId,
                TeamPosition = participant.TeamPosition
            };
        }


    }
}
