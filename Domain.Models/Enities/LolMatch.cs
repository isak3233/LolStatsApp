using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models.Enities;

namespace Domain.Models.Entities
{
    public class LolMatch
    {
        public LolMatchPlayer TargetPlayer { get; set; }
        public List<LolMatchPlayer> Players { get; set; }
        public List<LolMatchPlayer> Team1Players => Players.Where(p => p.TeamId == 100).ToList();
        public List<LolMatchPlayer> Team2Players => Players.Where(p => p.TeamId == 200).ToList();
        public string QueueType { get; set; }
        public long GameDuration { get; set; }
    }
    

}
