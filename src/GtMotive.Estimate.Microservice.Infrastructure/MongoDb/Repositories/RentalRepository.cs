using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Rentals.Entities;
using GtMotive.Estimate.Microservice.Domain.Rentals.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Entities;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private const string CollectionName = "Rentals";
        private readonly IMongoCollection<RentalEntity> _rentalsCollection;
        private readonly IResilienceService _resilienceService;

        public RentalRepository(IMongoService mongoService, IResilienceService resilienceService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _rentalsCollection = mongoService.Database.GetCollection<RentalEntity>(CollectionName);
            _resilienceService = resilienceService;
        }

        public async Task<Rental> CreateRental(Rental rental)
        {
            return await _resilienceService.ExecuteAsync(async () =>
            {
                await _rentalsCollection.InsertOneAsync(MapToEntity(rental));
                return rental;
            });
        }

        public async Task<Rental> GetPendingReturn(string personName)
        {
            return await _resilienceService.ExecuteAsync(async () =>
            {
                var filter = Builders<RentalEntity>.Filter.And(
                    Builders<RentalEntity>.Filter.Eq(r => r.PersonName, personName),
                    Builders<RentalEntity>.Filter.Eq(r => r.EndDate, null));

                var rentalEntity = await _rentalsCollection.Find(filter).FirstOrDefaultAsync();

                return rentalEntity is not null
                    ? MapToDomain(rentalEntity)
                    : null;
            });
        }

        public async Task<bool> HasActiveRental(string personName)
        {
            return await _resilienceService.ExecuteAsync(async () =>
            {
                var filter = Builders<RentalEntity>.Filter.And(
                    Builders<RentalEntity>.Filter.Eq(r => r.PersonName, personName),
                    Builders<RentalEntity>.Filter.Eq(r => r.EndDate, null));

                var count = await _rentalsCollection.CountDocumentsAsync(filter);

                return count > 0;
            });
        }

        public async Task Update(Rental rental)
        {
            await _resilienceService.ExecuteAsync(async () =>
            {
                var entity = MapToEntity(rental);

                var filter = Builders<RentalEntity>.Filter.Eq(r => r.Id, rental.Id);

                var result = await _rentalsCollection.ReplaceOneAsync(filter, entity);

                if (result.ModifiedCount == 0)
                {
                    throw new InvalidOperationException($"Rental with ID {rental.Id} not found.");
                }
            });
        }

        private static RentalEntity MapToEntity(Rental rental)
        {
            return new RentalEntity
            {
                Id = rental.Id,
                EndDate = rental.EndDate,
                PersonName = rental.PersonName,
                StartDate = rental.StartDate,
                VehicleId = rental.VehicleId
            };
        }

        private static Rental MapToDomain(RentalEntity entity)
        {
            return Rental.CreateFromPersistence(
                entity.Id,
                entity.VehicleId,
                entity.PersonName,
                entity.StartDate,
                entity.EndDate);
        }
    }
}
