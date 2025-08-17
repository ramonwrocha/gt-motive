using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles.Entities
{
    /// <summary>
    /// Represents a vehicle entity in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
    public sealed class Vehicle(string id, int manufactureYear)
    {
        private const int MaxManufactureYearDifference = 5;

        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        public string Id { get; private set; } = id;

        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        public int ManufactureYear { get; private set; } = manufactureYear;

        /// <summary>
        /// Gets the status of the vehicle.
        /// </summary>
        public VehicleStatus Status { get; private set; } = VehicleStatus.Available;

        /// <summary>
        /// Gets the creation date and time of the vehicle.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets a value indicating whether the vehicle is available for rent.
        /// </summary>
        public bool IsAvailable => Status == VehicleStatus.Available;

        /// <summary>
        /// Creates a new <see cref="Vehicle"/> instance with the specified brand, model, manufacture year, and license plate.
        /// </summary>
        /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
        /// <returns>A new <see cref="Vehicle"/> instance.</returns>
        public static Vehicle Create(int manufactureYear)
        {
            ValidateManufactureYear(manufactureYear);

            return new Vehicle(Guid.NewGuid().ToString(), manufactureYear);
        }

        /// <summary>
        /// Creates a new <see cref="Vehicle"/> instance from persistence data.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
        /// <param name="status">The status of the vehicle as a string.</param>
        /// <param name="createdAt">The creation date and time of the vehicle.</param>
        /// <returns>A <see cref="Vehicle"/> instance with the specified persisted values.</returns>
        public static Vehicle CreateFromPersistence(
            string id,
            int manufactureYear,
            string status,
            DateTime createdAt)
        {
            var vehicle = new Vehicle(id, manufactureYear)
            {
                CreatedAt = createdAt,
                Status = new VehicleStatus(status ?? VehicleStatus.Available.Value)
            };

            return vehicle;
        }

        /// <summary>
        /// Marks the vehicle as rented if it is currently available.
        /// </summary>
        public void Rent()
        {
            if (Status != VehicleStatus.Available)
            {
                throw new InvalidOperationException("Vehicle is not available for rent");
            }

            Status = VehicleStatus.Rented;
        }

        /// <summary>
        /// Marks the vehicle as available if it is currently rented.
        /// </summary>
        public void Return()
        {
            if (Status != VehicleStatus.Rented)
            {
                throw new InvalidOperationException("Vehicle is not rented");
            }

            Status = VehicleStatus.Available;
        }

        private static void ValidateManufactureYear(int manufactureYear)
        {
            var currentYear = DateTime.UtcNow.Year;

            if (manufactureYear < currentYear - MaxManufactureYearDifference)
            {
                throw new DomainException($"Vehicle manufacture year cannot be older than {currentYear - MaxManufactureYearDifference}");
            }

            if (manufactureYear > currentYear)
            {
                throw new DomainException("Vehicle manufacture year cannot be in the future");
            }
        }
    }
}
