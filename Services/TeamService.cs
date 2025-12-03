using FootballAPI.Models;
using FootballAPI.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FootballAPI.Services
{
    public class TeamService
    {
        private readonly IMongoCollection<Team> _teamsCollection;

        public TeamService(IOptions<MongoDbSettings> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.Value.ConnectionString);
            var database = client.GetDatabase(mongoConfig.Value.DatabaseName);
            _teamsCollection =
                database.GetCollection<Team>(mongoConfig.Value.TeamsCollectionName);
        }

        public async Task<List<Team>> GetAsync() =>
            await _teamsCollection.Find(_ => true).ToListAsync();

        public async Task<Team?> GetAsync(string id) =>
            await _teamsCollection.Find(t => t.Id == id).FirstOrDefaultAsync();

        public async Task<Team> CreateAsync(Team team)
        {
            await _teamsCollection.InsertOneAsync(team);
            return team;
        }

        public async Task UpdateAsync(string id, Team updatedTeam) =>
            await _teamsCollection.ReplaceOneAsync(t => t.Id == id, updatedTeam);

        public async Task RemoveAsync(string id) =>
            await _teamsCollection.DeleteOneAsync(t => t.Id == id);
    }
}
