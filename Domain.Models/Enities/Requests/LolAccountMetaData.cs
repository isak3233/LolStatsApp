using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Enities.Requests
{
    public class LolAccountMetaData
    {
        [BsonId]
        public string Puuid { get; set; }
        public string Region { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
    }
}
