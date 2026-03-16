using Domain.Models.Enities.Dto;
using Domain.Models.Enities.LolEnities;
using Domain.Models.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Mappers
{
    public static class MatchMapper
    {
        public static LolMatch Map(LolMatchDto dto, string puuid)
        {
            var targetPlayer = MapPlayer(dto.Info.Participants.First(p => p.Puuid == puuid));
            return new LolMatch
            {
                TargetPlayer = targetPlayer,
                Players = dto.Info.Participants.Select(p => MapPlayer(p, targetPlayer)).ToList(),
                QueueType = QueueMapper.GetQueueType(dto.Info.QueueId),
                GameDuration = dto.Info.GameDuration / 60,
                GameCreation = dto.Info.GameCreation,
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
        public static CurrentLolMatch Map(CurrentGameInfoDto dto, string puuid)
        {
            return new CurrentLolMatch
            {
                GameType = dto.GameType,
                GameStartTime = dto.GameStartTime,
                GameLength = dto.GameLength,
                GameMode = QueueMapper.GetQueueType((int)dto.GameQueueConfigId),
                Players = dto.Participants.Select(p => MapPlayer(p, puuid)).ToList()
            };
        }

        private static CurrentLolMatchPlayer MapPlayer(CurrentGameParticipantDto dto, string puuid)
        {
            return new CurrentLolMatchPlayer
            {
                IsTargetPlayer = dto.Puuid == puuid,
                ChampionName = ChampionMapper.GetChampionName(dto.ChampionId),
                ProfileIconId = dto.ProfileIconId,
                TeamId = dto.TeamId,
                Puuid = puuid,
                RiotId = dto.RiotId,
            };
        }
    }
}
