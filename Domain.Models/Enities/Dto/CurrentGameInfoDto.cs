using Domain.Models.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Models.Enities.Dto
{
    public class CurrentGameInfoDto
    {
        [JsonPropertyName("gameId")]
        public long GameId { get; set; }

        [JsonPropertyName("gameType")]
        public string GameType { get; set; }

        [JsonPropertyName("gameStartTime")]
        public long GameStartTime { get; set; }

        [JsonPropertyName("mapId")]
        public long MapId { get; set; }

        [JsonPropertyName("gameLength")]
        public long GameLength { get; set; }

        [JsonPropertyName("platformId")]
        public string PlatformId { get; set; }

        [JsonPropertyName("gameMode")]
        public string GameMode { get; set; }

        [JsonPropertyName("bannedChampions")]
        public List<BannedChampionDto> BannedChampions { get; set; }

        [JsonPropertyName("gameQueueConfigId")]
        public long GameQueueConfigId { get; set; }

        [JsonPropertyName("observers")]
        public ObserverDto Observers { get; set; }

        [JsonPropertyName("participants")]
        public List<CurrentGameParticipantDto> Participants { get; set; }
    }

    public class BannedChampionDto
    {
        [JsonPropertyName("pickTurn")]
        public int PickTurn { get; set; }

        [JsonPropertyName("championId")]
        public long ChampionId { get; set; }

        [JsonPropertyName("teamId")]
        public long TeamId { get; set; }
    }

    public class ObserverDto
    {
        [JsonPropertyName("encryptionKey")]
        public string EncryptionKey { get; set; }
    }

    public class CurrentGameParticipantDto
    {
        [JsonPropertyName("championId")]
        public long ChampionId { get; set; }

        [JsonPropertyName("perks")]
        public PerksDto Perks { get; set; }

        [JsonPropertyName("profileIconId")]
        public long ProfileIconId { get; set; }

        [JsonPropertyName("bot")]
        public bool Bot { get; set; }

        [JsonPropertyName("teamId")]
        public long TeamId { get; set; }

        [JsonPropertyName("puuid")]
        public string? Puuid { get; set; }
        [JsonPropertyName("riotId")]
        public string RiotId { get; set; }

        [JsonPropertyName("spell1Id")]
        public long Spell1Id { get; set; }

        [JsonPropertyName("spell2Id")]
        public long Spell2Id { get; set; }

        [JsonPropertyName("gameCustomizationObjects")]
        public List<GameCustomizationObjectDto> GameCustomizationObjects { get; set; }
    }

    public class LiveGamePerksDto
    {
        [JsonPropertyName("perkIds")]
        public List<long> PerkIds { get; set; }

        [JsonPropertyName("perkStyle")]
        public long PerkStyle { get; set; }

        [JsonPropertyName("perkSubStyle")]
        public long PerkSubStyle { get; set; }
    }

    public class GameCustomizationObjectDto
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
