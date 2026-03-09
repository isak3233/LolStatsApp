
using Domain.Models.Entities;
using Domain.Models.Entities.Dto;
using Domain.Models.Entities.Requests;
using Domain.Models.EntitiesDto;
using Domain.Models.Exceptions;
using Domain.Models.Interfaces;
using LoLStatsMaui.Domain.Exceptions;
using System.Diagnostics;
using System.Security.Principal;
using System.Text.Json;
using System.Text.RegularExpressions;



namespace LoLStatsMaui.Infrastructure.Repositories
{
    public class LolApiRepository : ILolRepository
    {
        private readonly HttpClient _httpClient;

        public LolApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LolAccountDto> GetLolAccount(string gameName, string tagLine)
        {
            return await GetAsync<LolAccountDto>($"riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}");

        }
        public async Task<AccountRegionDto> GetAccountRegion(string puuid)
        {
            return await GetAsync<AccountRegionDto>($"riot/account/v1/region/by-game/lol/by-puuid/{puuid}");
        }
        public async Task<SummonerDto> GetSummoner(string puuid, string region)
        {
            return await GetAsync<SummonerDto>($"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}");
        }
        public async Task<List<RankEntryDto>> GetRankEntries(string puuid, string region)
        {
            return await GetAsync<List<RankEntryDto>>($"https://{region}.api.riotgames.com/lol/league/v4/entries/by-puuid/{puuid}");
        }
        public async Task<List<string>> GetLolMatchesId(MatchQueryRequest request)
        {
            string? startTime = request.StartTime == null ? "" : "startTime=" + request.StartTime.ToString() + "&";
            string? endTime = request.EndTime == null ? "" : "endTime=" + request.EndTime.ToString() + "&";
            string? queue = request.Queue == null ? "" : "queue=" + request.Queue.ToString() + "&";
            string? type = request.Type == null ? "" : "type=" + request.Type.ToString() + "&";
            string? start = request.Start == null ? "" : "start=" + request.Start.ToString() + "&";
            string count = "count=" + request.Count.ToString();

            string url = $"https://{request.Region}.api.riotgames.com/lol/match/v5/matches/by-puuid/{request.Uuid}/ids?{startTime}{endTime}{queue}{type}{start}{count}";
            return await GetAsync<List<string>>(url);
        }
        public async Task<LolMatchDto> GetLolMatch(string matchId, string region)
        {
            return await GetAsync<LolMatchDto>($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId}");
        }
      


        private async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            int statusCode = (int)response.StatusCode;
            if (statusCode == 404) throw new NotFoundException(json);
            if (statusCode == 401) throw new UnauthorizedException(json);
            if (statusCode == 403) throw new UnauthorizedException(json);
            if (statusCode == 429) throw new RateLimitException(json);
            if (statusCode >= 500) throw new ServerException(json);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<T>(json);

        }
    }
}
