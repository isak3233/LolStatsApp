using Domain.Models.Enities.LolEnities;
using Domain.Models.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

    }
}
