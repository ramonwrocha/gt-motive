using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public VehicleRepository(IMongoDatabase database)
        {
            ArgumentNullException.ThrowIfNull(database);
            _vehiclesCollection = database.GetCollection<VehicleEntity>(CollectionName);
        }

        public async Task Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var entity = MapToEntity(vehicle);

            await _vehiclesCollection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehicles()
        {
            var availableStatus = VehicleStatus.Available.ToString();

            var entities = await _vehiclesCollection.Find(x => x.Status.Equals(availableStatus, StringComparison.Ordinal)).ToListAsync();

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
