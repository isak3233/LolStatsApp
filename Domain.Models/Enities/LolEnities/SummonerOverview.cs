using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities.LolEnities
{
    public class SummonerOverview
    {
        [BsonId]
        public string Uuid { get; set; }
        public string SummonerName { get; set; }
        public string TagLine { get; set; }
        public string Region { get; set; }
        public string RawRegion { get; set; }
        public long Level { get; set; }
        public string ProfileIconString { get; set; }
        public List<RankEntry> RankEntries { get; set; }
        public bool IsInGame => CurrentLolMatch != null;
        [BsonIgnoreIfNull]
        public CurrentLolMatch? CurrentLolMatch { get; set; }
    }
}
