using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities.LolEnities
{
    public class CurrentLolMatchPlayer
    {
        public bool IsTargetPlayer { get; set; }
        public string ChampionName { get; set; }
        public long ProfileIconId { get; set; }
        public long TeamId { get; set; }
        public string? Puuid { get; set; }
        public string? RiotId { get; set; }
    }
}
