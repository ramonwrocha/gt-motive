using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task EndRental(string rentalId, DateTime endDate)
        {
            await _resilienceService.ExecuteAsync(async () =>
            {
                var filter = Builders<RentalEntity>.Filter.Eq(r => r.Id, rentalId);

                var update = Builders<RentalEntity>.Update.Set(r => r.EndDate, endDate);

                var result = await _rentalsCollection.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                {
                    throw new InvalidOperationException($"Rental with ID {rentalId} not found or already ended.");
                }
            });
        }

        public async Task<IEnumerable<Rental>> GetActiveRentals()
        {
            return await _resilienceService.ExecuteAsync(async () =>
            {
                var filter = Builders<RentalEntity>.Filter.Eq(r => r.EndDate, null);

                var rentals = await _rentalsCollection.Find(filter).ToListAsync();

                return rentals.Select(MapToDomain);
            });
        }

        public async Task<Rental> GetById(string rentalId)
        {
            return await _resilienceService.ExecuteAsync(async () =>
            {
                var filter = Builders<RentalEntity>.Filter.Eq(r => r.Id, rentalId);

                var rentalEntity = await _rentalsCollection.Find(filter).FirstOrDefaultAsync();

                return rentalEntity is not null
                    ? MapToDomain(rentalEntity)
                    : null;
            });
        }

        public async Task<bool> HasActiveRental(string personId)
        {
            return await _resilienceService.ExecuteAsync(async () =>
            {
                var filter = Builders<RentalEntity>.Filter.And(
                    Builders<RentalEntity>.Filter.Eq(r => r.PersonId, personId),
                    Builders<RentalEntity>.Filter.Eq(r => r.EndDate, null));

                var count = await _rentalsCollection.CountDocumentsAsync(filter);

                return count > 0;
            });
        }

        private static RentalEntity MapToEntity(Rental rental)
        {
            return new RentalEntity
            {
                Id = rental.Id,
                EndDate = rental.EndDate,
                PersonId = rental.PersonId,
                StartDate = rental.StartDate,
                VehicleId = rental.VehicleId
            };
        }

        private static Rental MapToDomain(RentalEntity entity)
        {
            return Rental.CreateFromPersistence(
                entity.Id,
                entity.VehicleId,
                entity.PersonId,
                entity.StartDate,
                entity.EndDate);
        }
    }
}
