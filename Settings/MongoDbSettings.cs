namespace FootballAPI.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string TeamsCollectionName { get; set; } = null!;
    }
}
