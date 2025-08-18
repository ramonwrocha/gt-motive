using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Entities;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Entities;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private const string CollectionName = "Vehicles";
        private readonly IMongoCollection<VehicleEntity> _vehiclesCollection;
        private readonly IResilienceService _resilienceService;

        public VehicleRepository(IMongoService mongoService, IResilienceService resilienceService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _vehiclesCollection = mongoService.Database.GetCollection<VehicleEntity>(CollectionName);
            _resilienceService = resilienceService;
        }

        public async Task<Vehicle> GetByIdAsync(string vehicleId)
        {
            ArgumentNullException.ThrowIfNull(vehicleId);

            var entity = await _resilienceService.ExecuteAsync(async () =>
            {
                return await _vehiclesCollection
                    .Find(x => x.Id == vehicleId)
                    .FirstOrDefaultAsync();
            });

            return entity is not null
                ? MapToDomain(entity)
                : null;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var entity = MapToEntity(vehicle);

            await _resilienceService.ExecuteAsync(async () =>
            {
                await _vehiclesCollection.InsertOneAsync(entity);
            });
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
        {
            var availableStatus = VehicleStatus.Available.ToString();

            var entities = await _resilienceService.ExecuteAsync(async () =>
            {
                return await _vehiclesCollection
                    .Find(x => x.Status.Equals(availableStatus, StringComparison.Ordinal)).ToListAsync();
            });

            return entities.Select(MapToDomain);
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            await _resilienceService.ExecuteAsync(async () =>
            {
                var entity = MapToEntity(vehicle);

                var filter = Builders<VehicleEntity>.Filter.Eq(x => x.Id, entity.Id);

                var result = await _vehiclesCollection.ReplaceOneAsync(filter, entity);

                if (result.ModifiedCount == 0)
                {
                    throw new InvalidOperationException($"Vehicle with ID {entity.Id} not found.");
                }
            });
        }

        private static VehicleEntity MapToEntity(Vehicle vehicle)
        {
            return new VehicleEntity
            {
                Id = vehicle.Id,
                ManufactureYear = vehicle.ManufactureYear,
                Status = vehicle.Status.Value,
                CreatedAt = vehicle.CreatedAt
            };
        }

        private static Vehicle MapToDomain(VehicleEntity entity)
        {
            return Vehicle.CreateFromPersistence(
                id: entity.Id,
                manufactureYear: entity.ManufactureYear,
                status: entity.Status,
                createdAt: entity.CreatedAt);
        }
    }
}
