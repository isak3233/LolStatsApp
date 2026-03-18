using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Mappers
{
    public static class QueueMapper
    {
        public static readonly Dictionary<string, string> QueueTypeMap = new()
        {
            { "RANKED_SOLO_5x5", "Ranked Solo/Duo" },
            { "RANKED_FLEX_SR", "Ranked Flex" }
        };

        public static readonly Dictionary<int, string> QueueIdMap = new()
        {
            { 420, "Ranked Solo/Duo" },
            { 440, "Ranked Flex" },
            { 2400, "Aram: Mayhem" },
            { 450, "Aram" },
            { 2300, "Brawl" },
            { 1700, "Arena" },
            { 870, "Co-op vs Ai" },
            { 880, "Co-op vs Ai" },
            { 890, "Co-op vs Ai" },
            { 900, "Arurf" },
            { 700, "Clash Sr" },
            { 720, "Clash Aram" },
            { 490, "Normal Quickplay" },
            { 480, "Swiftplay" },
            { 430, "Blind Pick" },
            { 400, "Normal" },
        };

        public static string GetQueueType(string queueType) => QueueTypeMap.TryGetValue(queueType, out var readable) ? readable : queueType;

        public static string GetQueueType(int queueId) => QueueIdMap.TryGetValue(queueId, out var readable) ? readable : "Gamemode";

        public static int? GetQueueId(string queueType)
        {
            var result = QueueIdMap.FirstOrDefault(x => x.Value == queueType);
            return result.Key == 0 ? null : result.Key;
        }
    }
}
