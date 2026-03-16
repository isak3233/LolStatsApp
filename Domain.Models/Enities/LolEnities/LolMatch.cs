using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities.LolEnities
{
    public class LolMatch
    {
        public LolMatchPlayer TargetPlayer { get; set; }
        public List<LolMatchPlayer> Players { get; set; }
        public List<LolMatchPlayer> Team1Players => Players.Where(p => p.TeamId == 100).ToList();
        public List<LolMatchPlayer> Team2Players => Players.Where(p => p.TeamId == 200).ToList();
        public string QueueType { get; set; }
        public long GameDuration { get; set; }
        public long GameCreation { get; set; }
        public string GameCreationString
        {
            get
            {
                var matchTime = DateTimeOffset.FromUnixTimeMilliseconds(GameCreation).LocalDateTime;
                var diff = DateTime.Now - matchTime;

                if (diff.TotalMinutes < 60)
                    return diff.TotalMinutes < 2 ? "1 minut sedan" : $"{(int)diff.TotalMinutes} minuter sedan";
                if (diff.TotalHours < 24)
                    return diff.TotalHours < 2 ? "1 timme sedan" : $"{(int)diff.TotalHours} timmar sedan";
                if (diff.TotalDays < 30)
                    return diff.TotalDays < 2 ? "1 dag sedan" : $"{(int)diff.TotalDays} dagar sedan";

                int months = (int)(diff.TotalDays / 30);
                return months < 2 ? "1 månad sedan" : $"{months} månader sedan";
            }
        }
    }
    

}
