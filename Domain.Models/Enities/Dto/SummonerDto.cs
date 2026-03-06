using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities.Dto
{
    public class SummonerDto
    {
        public int profileIconId { get; set; }
        public long revisionDate { get; set; }
        public string puuid { get; set; }
        public long summonerLevel { get; set; }
    }
}
