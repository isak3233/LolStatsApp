using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Enities.UserEnities
{
    public class User
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> LinkedLolAccounts { get; set; } = new();
        public List<string> FollowedAccounts { get; set; } = new();
    }
}
