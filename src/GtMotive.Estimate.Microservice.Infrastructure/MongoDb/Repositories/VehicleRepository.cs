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

        public async Task Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var entity = MapToEntity(vehicle);

            await _resilienceService.ExecuteAsync(async () =>
            {
                await _vehiclesCollection.InsertOneAsync(entity);
            });
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehicles()
        {
            var availableStatus = VehicleStatus.Available.ToString();

            var entities = await _resilienceService.ExecuteAsync(async () =>
            {
                return await _vehiclesCollection
                    .Find(x => x.Status.Equals(availableStatus, StringComparison.Ordinal)).ToListAsync();
            });

            return entities.Select(MapToDomain);
        }

        private static VehicleEntity MapToEntity(Vehicle vehicle)
        {
            return new VehicleEntity
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ManufactureYear = vehicle.ManufactureYear,
                LicensePlate = vehicle.LicensePlate,
                Status = vehicle.Status.Value,
                CreatedAt = vehicle.CreatedAt
            };
        }

        private static Vehicle MapToDomain(VehicleEntity entity)
        {
            return Vehicle.CreateFromPersistence(
                id: entity.Id,
                brand: entity.Brand,
                model: entity.Model,
                manufactureYear: entity.ManufactureYear,
                licensePlate: entity.LicensePlate,
                status: entity.Status,
                createdAt: entity.CreatedAt);
        }
    }
}
