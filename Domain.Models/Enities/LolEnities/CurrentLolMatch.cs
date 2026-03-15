using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities.LolEnities
{
    public class CurrentLolMatch
    {
        public string GameType { get; set; }
        public long GameStartTime { get; set; }
        public long GameLength { get; set; }
        public string GameMode { get; set; }
        public List<CurrentLolMatchPlayer> Players { get; set; }
    }
}
