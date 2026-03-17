using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities.LolEnities
{
    public class LolMatchPlayer
    {
        [BsonIgnore]
        public bool IsTargetPlayer { get; set; }
        public string Puuid { get; set; }
        public string FullLolName => $"{GameName}#{TagLine}";
        public string GameName { get; set; }
        public string TagLine { get; set; }
        public bool Win { get; set; }
        public int Assists { get; set; }
        public int ChampLevel { get; set; }
        public int ChampionId { get; set; }
        public string ChampionName { get; set; }
        public int ChampionTransform { get; set; } //	This field is currently only utilized for Kayn's transformations. (Legal values: 0 - None, 1 - Slayer, 2 - Assassin)
        public int Deaths { get; set; }
        public int Item0 { get; set; }
        public int Item1 { get; set; }
        public int Item2 { get; set; }
        public int Item3 { get; set; }
        public int Item4 { get; set; }
        public int Item5 { get; set; }
        public int Item6 { get; set; }
        public int Kills { get; set; }


        public int TeamId { get; set; }
        public string TeamPosition { get; set; }
    }
}
