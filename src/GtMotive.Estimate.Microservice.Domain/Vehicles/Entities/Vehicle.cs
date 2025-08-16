using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles.Entities
{
    /// <summary>
    /// Represents a vehicle entity in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <param name="brand">The brand of the vehicle.</param>
    /// <param name="model">The model of the vehicle.</param>
    /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
    /// <param name="licensePlate">The license plate of the vehicle.</param>
    public sealed class Vehicle(string id, string brand, string model, int manufactureYear, string licensePlate)
    {
        private const int MaxManufactureYearDifference = 5;

        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        public string Id { get; private set; } = id;

        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        public string Brand { get; private set; } = brand;

        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        public string Model { get; private set; } = model;

        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        public int ManufactureYear { get; private set; } = manufactureYear;

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; private set; } = licensePlate;

        /// <summary>
        /// Gets the status of the vehicle.
        /// </summary>
        public VehicleStatus Status { get; private set; } = VehicleStatus.Available;

        /// <summary>
        /// Gets the creation date and time of the vehicle.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Creates a new <see cref="Vehicle"/> instance with the specified brand, model, manufacture year, and license plate.
        /// </summary>
        /// <param name="brand">The brand of the vehicle.</param>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
        /// <param name="licensePlate">The license plate of the vehicle.</param>
        /// <returns>A new <see cref="Vehicle"/> instance.</returns>
        public static Vehicle Create(string brand, string model, int manufactureYear, string licensePlate)
        {
            ValidateManufactureYear(manufactureYear);
            ValidateBrand(brand);
            ValidateModel(model);
            ValidateLicensePlate(licensePlate);

            return new Vehicle(Guid.NewGuid().ToString(), brand, model, manufactureYear, licensePlate);
        }

        /// <summary>
        /// Creates a new <see cref="Vehicle"/> instance from persistence data.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <param name="brand">The brand of the vehicle.</param>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
        /// <param name="licensePlate">The license plate of the vehicle.</param>
        /// <param name="status">The status of the vehicle as a string.</param>
        /// <param name="createdAt">The creation date and time of the vehicle.</param>
        /// <returns>A <see cref="Vehicle"/> instance with the specified persisted values.</returns>
        public static Vehicle CreateFromPersistence(
            string id,
            string brand,
            string model,
            int manufactureYear,
            string licensePlate,
            string status,
            DateTime createdAt)
        {
            var vehicle = new Vehicle(id, brand, model, manufactureYear, licensePlate)
            {
                Id = id,
                CreatedAt = createdAt,
                Status = Enum.TryParse<VehicleStatus>(status, out var parsedStatus) ? parsedStatus : VehicleStatus.Available
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

        private static void ValidateBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new DomainException("Vehicle brand is required");
            }
        }

        private static void ValidateModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new DomainException("Vehicle model is required");
            }
        }

        private static void ValidateLicensePlate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new DomainException("Vehicle license plate is required");
            }
        }
    }
}
