using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities
{
    public class LolProfileData
    {
        public SummonerOverview SummonerOverview { get; set; }
        public List<LolMatch> Matches { get; set; }

    }
}
