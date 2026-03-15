using Domain.Models.Enities.Dto;
using Domain.Models.Enities.LolEnities;
using Domain.Models.Entities.Dto;
using Domain.Models.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.Infrastructure.Constants
{
    public static class RiotMapper
    {
        public static readonly Dictionary<string, string> QueueTypeMap = new()
        {
            { "RANKED_SOLO_5x5", "Ranked Solo/Duo" },
            { "RANKED_FLEX_SR", "Ranked Flex" }
        };
        public static readonly Dictionary<int, string> QueueIdMap = new()
        {
            //https://static.developer.riotgames.com/docs/lol/queues.json INFO
            { 420, "Ranked Solo/Duo" },
            { 440, "Ranked Flex" },
            { 2400, "ARAM: Mayhem" },
            { 100, "ARAM" },
            { 450, "ARAM" },
            { 2300, "BRAWL" },
            { 1700, "ARENA"},
            { 1710, "ARENA"},
            { 870, "Co-op vs Ai"},
            { 880, "Co-op vs Ai"},
            { 890, "Co-op vs Ai"},
            { 900,  "ARURF"},
            { 700,  "CLASH"},
            { 720, "CLASH" },
            { 490, "Normal Quickplay" },
            { 480, "Swiftplay" },
            { 430, "Blind Pick" },
            { 400, "Normal"},
            
        };

        public static readonly Dictionary<string, string> RegionMap = new()
        {
                { "euw1", "EUW" },
                { "eun1", "EUNE" },
                { "na1", "NA" },
                { "kr", "KR" },
                { "br1", "BR" },
                { "la1", "LAN" },
                { "la2", "LAS" },
                { "oc1", "OCE" },
                { "tr1", "TR" },
                { "ru", "RU" },
                { "jp1", "JP" }
        };
        public static readonly Dictionary<string, string> RegionToRoutingMap = new()
        {
            { "na1", "americas" },
            { "br1", "americas" },
            { "la1", "americas" },
            { "la2", "americas" },
            { "kr", "asia" },
            { "jp1", "asia" },
            { "eun1", "europe" },
            { "euw1", "europe" },
            { "me1", "europe" },
            { "tr1", "europe" },
            { "ru", "europe" },
            { "oc1", "sea" },
            { "sg2", "sea" },
            { "tw2", "sea" },
            { "vn2", "sea" }
        };
        private static readonly Dictionary<long, string> ChampionIdToName = new()
        {
            { 266, "Aatrox" },
            { 103, "Ahri" },
            { 84, "Akali" },
            { 166, "Akshan" },
            { 12, "Alistar" },
            { 799, "Ambessa" },
            { 32, "Amumu" },
            { 34, "Anivia" },
            { 1, "Annie" },
            { 523, "Aphelios" },
            { 22, "Ashe" },
            { 136, "AurelionSol" },
            { 893, "Aurora" },
            { 268, "Azir" },
            { 432, "Bard" },
            { 200, "Belveth" },
            { 53, "Blitzcrank" },
            { 63, "Brand" },
            { 201, "Braum" },
            { 233, "Briar" },
            { 51, "Caitlyn" },
            { 164, "Camille" },
            { 69, "Cassiopeia" },
            { 31, "Chogath" },
            { 42, "Corki" },
            { 122, "Darius" },
            { 131, "Diana" },
            { 119, "Draven" },
            { 36, "DrMundo" },
            { 245, "Ekko" },
            { 60, "Elise" },
            { 28, "Evelynn" },
            { 81, "Ezreal" },
            { 9, "Fiddlesticks" },
            { 114, "Fiora" },
            { 105, "Fizz" },
            { 3, "Galio" },
            { 41, "Gangplank" },
            { 86, "Garen" },
            { 150, "Gnar" },
            { 79, "Gragas" },
            { 104, "Graves" },
            { 887, "Gwen" },
            { 120, "Hecarim" },
            { 74, "Heimerdinger" },
            { 910, "Hwei" },
            { 420, "Illaoi" },
            { 39, "Irelia" },
            { 427, "Ivern" },
            { 40, "Janna" },
            { 59, "JarvanIV" },
            { 24, "Jax" },
            { 126, "Jayce" },
            { 202, "Jhin" },
            { 222, "Jinx" },
            { 145, "Kaisa" },
            { 429, "Kalista" },
            { 43, "Karma" },
            { 30, "Karthus" },
            { 38, "Kassadin" },
            { 55, "Katarina" },
            { 10, "Kayle" },
            { 141, "Kayn" },
            { 85, "Kennen" },
            { 121, "Khazix" },
            { 203, "Kindred" },
            { 240, "Kled" },
            { 96, "KogMaw" },
            { 897, "KSante" },
            { 7, "Leblanc" },
            { 64, "LeeSin" },
            { 89, "Leona" },
            { 876, "Lillia" },
            { 127, "Lissandra" },
            { 236, "Lucian" },
            { 117, "Lulu" },
            { 99, "Lux" },
            { 54, "Malphite" },
            { 90, "Malzahar" },
            { 57, "Maokai" },
            { 11, "MasterYi" },
            { 800, "Mel" },
            { 902, "Milio" },
            { 21, "MissFortune" },
            { 62, "MonkeyKing" },
            { 82, "Mordekaiser" },
            { 25, "Morgana" },
            { 950, "Naafiri" },
            { 267, "Nami" },
            { 75, "Nasus" },
            { 111, "Nautilus" },
            { 518, "Neeko" },
            { 76, "Nidalee" },
            { 895, "Nilah" },
            { 56, "Nocturne" },
            { 20, "Nunu" },
            { 2, "Olaf" },
            { 61, "Orianna" },
            { 516, "Ornn" },
            { 80, "Pantheon" },
            { 78, "Poppy" },
            { 555, "Pyke" },
            { 246, "Qiyana" },
            { 133, "Quinn" },
            { 497, "Rakan" },
            { 33, "Rammus" },
            { 421, "RekSai" },
            { 526, "Rell" },
            { 888, "Renata" },
            { 58, "Renekton" },
            { 107, "Rengar" },
            { 92, "Riven" },
            { 68, "Rumble" },
            { 13, "Ryze" },
            { 360, "Samira" },
            { 113, "Sejuani" },
            { 235, "Senna" },
            { 147, "Seraphine" },
            { 875, "Sett" },
            { 35, "Shaco" },
            { 98, "Shen" },
            { 102, "Shyvana" },
            { 27, "Singed" },
            { 14, "Sion" },
            { 15, "Sivir" },
            { 72, "Skarner" },
            { 901, "Smolder" },
            { 37, "Sona" },
            { 16, "Soraka" },
            { 50, "Swain" },
            { 517, "Sylas" },
            { 134, "Syndra" },
            { 223, "TahmKench" },
            { 163, "Taliyah" },
            { 91, "Talon" },
            { 44, "Taric" },
            { 17, "Teemo" },
            { 412, "Thresh" },
            { 18, "Tristana" },
            { 48, "Trundle" },
            { 23, "Tryndamere" },
            { 4, "TwistedFate" },
            { 29, "Twitch" },
            { 77, "Udyr" },
            { 6, "Urgot" },
            { 110, "Varus" },
            { 67, "Vayne" },
            { 45, "Veigar" },
            { 161, "Velkoz" },
            { 711, "Vex" },
            { 254, "Vi" },
            { 234, "Viego" },
            { 112, "Viktor" },
            { 8, "Vladimir" },
            { 106, "Volibear" },
            { 19, "Warwick" },
            { 498, "Xayah" },
            { 101, "Xerath" },
            { 5, "XinZhao" },
            { 157, "Yasuo" },
            { 777, "Yone" },
            { 83, "Yorick" },
            { 804, "Yunara" },
            { 350, "Yuumi" },
            { 904, "Zaahen" },
            { 154, "Zac" },
            { 238, "Zed" },
            { 221, "Zeri" },
            { 115, "Ziggs" },
            { 26, "Zilean" },
            { 142, "Zoe" },
            { 143, "Zyra" },
        };
        public static string GetChampionName(long championId)
        {
            return ChampionIdToName.TryGetValue(championId, out var name) ? name : "Unknown";
        }
        public static readonly int[] StartingIconIds = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
        public static string GetRouting(string region)
        {
            return RegionToRoutingMap.TryGetValue(region.ToLower(), out var routing) ? routing : "europe";
        }
        public static string GetRegion(string region)
        {
            return RegionMap.TryGetValue(region.ToLower(), out var readable) ? readable : region.ToUpper();
        }
        public static string GetQueueType(string queueType)
        {
            return QueueTypeMap.TryGetValue(queueType, out var readable) ? readable : queueType;
        }
        public static string GetQueueType(int queueId)
        {
            return QueueIdMap.TryGetValue(queueId, out var readable) ? readable : "Gamemode";
        }
        public static List<RankEntry> GetRankEntries(List<RankEntryDto> rankEntriesDto)
        {
            List<RankEntry> rankEntries = new List<RankEntry>
            {
                new RankEntry { Tier = "Unranked", QueueType = QueueTypeMap["RANKED_SOLO_5x5"], ImageString = "RankIcons/Rank=Unranked.png" },
                new RankEntry { Tier = "Unranked", QueueType = QueueTypeMap["RANKED_FLEX_SR"], ImageString = "RankIcons/Rank=Unranked.png" }
            };

            foreach (var rankEntryDto in rankEntriesDto)
            {
                int index = rankEntryDto.queueType == "RANKED_SOLO_5x5" ? 0 :
                            rankEntryDto.queueType == "RANKED_FLEX_SR" ? 1 : -1;

                if (index == -1) continue;

                int winRate = (int)Math.Round((double)rankEntryDto.wins / (rankEntryDto.wins + rankEntryDto.losses) * 100);
                string[] noRankTiers = { "MASTER", "GRANDMASTER", "CHALLENGER" };
                string rank = noRankTiers.Contains(rankEntryDto.tier) ? "" : rankEntryDto.rank;

                rankEntries[index] = new RankEntry
                {
                    QueueType = GetQueueType(rankEntryDto.queueType),
                    Tier = rankEntryDto.tier,
                    Rank = rank,
                    LeaguePoints = rankEntryDto.leaguePoints,
                    Wins = rankEntryDto.wins,
                    Losses = rankEntryDto.losses,
                    WinRate = winRate,
                    ImageString = $"RankIcons/Rank={rankEntryDto.tier}.png"
                };
            }
            return rankEntries;
        }
        public static CurrentLolMatch Map(CurrentGameInfoDto dto, string puuid)
        {
            return new CurrentLolMatch
            {
                GameType = dto.GameType,
                GameStartTime = dto.GameStartTime,
                GameLength = dto.GameLength,
                GameMode = GetQueueType((int)dto.GameQueueConfigId),
                Players = dto.Participants.Select(p => MapPlayer(p, puuid)).ToList()
            };
        }
        public static CurrentLolMatchPlayer MapPlayer(CurrentGameParticipantDto dto, string puuid)
        {
            return new CurrentLolMatchPlayer
            {
                IsTargetPlayer = dto.Puuid == puuid,
                ChampionName = GetChampionName(dto.ChampionId),
                ProfileIconId = dto.ProfileIconId,
                TeamId = dto.TeamId,
                Puuid = puuid,
                RiotId = dto.RiotId,
            };
        }
        public static LolMatch Map(LolMatchDto dto, string puuid)
        {
            var targetPlayer = MapPlayer(dto.Info.Participants.First(p => p.Puuid == puuid));
            return new LolMatch
            {
                TargetPlayer = targetPlayer,
                Players = dto.Info.Participants.Select(p => MapPlayer(p, targetPlayer)).ToList(),
                QueueType = GetQueueType(dto.Info.QueueId),
                GameDuration = (int)(dto.Info.GameDuration / 60),
            };
        }
        
        private static LolMatchPlayer MapPlayer(ParticipantDto participant, LolMatchPlayer? targetPlayer = null)
        {
            return new LolMatchPlayer
            {
                IsTargetPlayer = targetPlayer?.GameName == participant.RiotIdGameName,
                GameName = participant.RiotIdGameName,
                TagLine = participant.RiotIdTagline,
                Win = participant.Win,
                ChampLevel = participant.ChampLevel,
                Assists = participant.Assists,
                ChampionId = participant.ChampionId,
                ChampionName = participant.ChampionName,
                ChampionTransform = participant.ChampionTransform,
                Deaths = participant.Deaths,
                Item0 = participant.Item0,
                Item1 = participant.Item1,
                Item2 = participant.Item2,
                Item3 = participant.Item3,
                Item4 = participant.Item4,
                Item5 = participant.Item5,
                Item6 = participant.Item6,
                Kills = participant.Kills,
                TeamId = participant.TeamId,
                TeamPosition = participant.TeamPosition
            };
        }


    }
}
