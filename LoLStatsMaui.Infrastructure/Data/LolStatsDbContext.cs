using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Enities.UserEnities;
using MongoDB.Driver;

public class LolStatsDbContext
{
    private static LolStatsDbContext? _instance;
    private readonly IMongoDatabase _database;

    private LolStatsDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public static LolStatsDbContext GetInstance(string connectionString, string databaseName)
    {
        if (_instance == null)
        {
            _instance = new LolStatsDbContext(connectionString, databaseName);
        }
        return _instance;
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<SummonerOverview> SummonerOverviews => _database.GetCollection<SummonerOverview>("SummonerOverviews");
    public IMongoCollection<LolAccountMetaData> LolAccountsMetaData => _database.GetCollection<LolAccountMetaData>("LolAccountsMetaData");
    public IMongoCollection<LolMatch> LolMatches => _database.GetCollection<LolMatch>("LolMatches");
}