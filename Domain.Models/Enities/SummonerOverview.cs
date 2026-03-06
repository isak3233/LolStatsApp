using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class SummonerOverview
    {
        public string Uuid { get; set; }
        public string SummonerName { get; set; }
        public string TagLine { get; set; }
        public string Region { get; set; }
        public string RawRegion { get; set; }
        public long Level { get; set; }
        public int ProfileIconId { get; set; }
        public List<RankEntry> RankEntries { get; set; }
    }
}
