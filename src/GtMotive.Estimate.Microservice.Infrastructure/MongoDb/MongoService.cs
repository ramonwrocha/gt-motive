using System;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public sealed class MongoService : IMongoService
    {
        public MongoService(IOptions<MongoDbSettings> options)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(options.Value.ConnectionString);
            ArgumentException.ThrowIfNullOrWhiteSpace(options.Value.DatabaseName);

            MongoClient = new MongoClient(options.Value.ConnectionString);
            Database = MongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoClient MongoClient { get; }

        public IMongoDatabase Database { get; }
    }
}
