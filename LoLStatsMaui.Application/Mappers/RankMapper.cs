using Domain.Models.Enities.LolEnities;
using Domain.Models.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Mappers
{
    public static class RankMapper
    {
        public static List<RankEntry> GetRankEntries(List<RankEntryDto> rankEntriesDto)
        {
            List<RankEntry> rankEntries = new()
        {
            new RankEntry { Tier = "Unranked", QueueType = QueueMapper.QueueTypeMap["RANKED_SOLO_5x5"], ImageString = "RankIcons/Rank=Unranked.png" },
            new RankEntry { Tier = "Unranked", QueueType = QueueMapper.QueueTypeMap["RANKED_FLEX_SR"], ImageString = "RankIcons/Rank=Unranked.png" }
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
                    QueueType = QueueMapper.GetQueueType(rankEntryDto.queueType),
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
    }
}
