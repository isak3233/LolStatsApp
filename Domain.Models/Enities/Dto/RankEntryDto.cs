using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities.Dto
{
    public class RankEntryDto
    {
        public string queueType { get; set; }
        public string tier { get; set; }
        public string rank { get; set; }

        public int leaguePoints { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
    }
}
