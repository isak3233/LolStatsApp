using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.Infrastructure.Repositories
{
    public class LolDbRepository : ILolDbRepository
    {
        private readonly LolStatsDbContext _context;

        public LolDbRepository(LolStatsDbContext context)
        {
            _context = context;
        }

        public async Task UpsertSummonerOverviewAsync(SummonerOverview summonerOverview)
        {
            var filter = Builders<SummonerOverview>.Filter.Eq(s => s.Uuid, summonerOverview.Uuid);
            await _context.SummonerOverviews.ReplaceOneAsync(filter, summonerOverview, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<SummonerOverview?> GetSummonerOverviewAsync(string puuid)
        {
            var filter = Builders<SummonerOverview>.Filter.Eq(s => s.Uuid, puuid);
            var result = await _context.SummonerOverviews.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task UpsertLolAccountMetaDataAsync(LolAccountMetaData lolAccountMetaData)
        {
            var filter = Builders<LolAccountMetaData>.Filter.Eq(s => s.Puuid, lolAccountMetaData.Puuid);
            await _context.LolAccountsMetaData.ReplaceOneAsync(filter, lolAccountMetaData, new ReplaceOptions { IsUpsert = true });
        }
        public async Task<LolAccountMetaData?> GetLolAccountMetaDataAsync(string lolName)
        {
            string[] splitName = lolName.Split('#');
            string gameName = splitName[0];
            string tagLine = splitName[1];

            var filter = Builders<LolAccountMetaData>.Filter.And(
                Builders<LolAccountMetaData>.Filter.Regex(s => s.GameName, new BsonRegularExpression($"^{gameName}$", "i")),
                Builders<LolAccountMetaData>.Filter.Regex(s => s.TagLine, new BsonRegularExpression($"^{tagLine}$", "i"))
            );
            var result = await _context.LolAccountsMetaData.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<LolAccountMetaData?> GetLolAccountMetaDataByPuuidAsync(string puuid)
        {
            var filter = Builders<LolAccountMetaData>.Filter.Eq(s => s.Puuid, puuid);
            var result = await _context.LolAccountsMetaData.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<List<LolMatch>> GetLolMatches(List<string> matchIds)
        {
            var filter = Builders<LolMatch>.Filter.In(s => s.MatchId, matchIds);
            var result = await _context.LolMatches.FindAsync(filter);
            return await result.ToListAsync();
        }
        public async Task UpsertLolMatchesAsync(List<LolMatch> lolMatches)
        {
            var tasks = lolMatches.Select(match =>
            {
                var filter = Builders<LolMatch>.Filter.Eq(m => m.MatchId, match.MatchId);
                return _context.LolMatches.ReplaceOneAsync(filter, match, new ReplaceOptions { IsUpsert = true });
            });
            await Task.WhenAll(tasks);
        }


    }
}
