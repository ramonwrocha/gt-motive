using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public sealed class MongoService : IMongoService
    {
        public MongoService(IOptions<MongoDbSettings> options)
        {
            MongoClient = new MongoClient(options.Value.ConnectionString);
            Database = MongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoClient MongoClient { get; }

        public IMongoDatabase Database { get; }
    }
}
