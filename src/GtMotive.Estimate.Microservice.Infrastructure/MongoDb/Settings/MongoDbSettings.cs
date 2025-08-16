namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings
{
    public class MongoDbSettings
    {
        public const string SectionName = "MongoDb";

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
