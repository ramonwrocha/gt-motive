using System;

namespace GtMotive.Estimate.Microservice.Domain.Rentals.Entities
{
    /// <summary>
    /// Represents a rental of a vehicle by a person, including start and optional end dates.
    /// </summary>
    public sealed class Rental(
        string id,
        string vehicleId,
        string personName,
        DateTime startDate)
    {
        /// <summary>
        /// Gets the unique identifier for the rental.
        /// </summary>
        public string Id { get; private set; } = id;

        /// <summary>
        /// Gets the unique identifier for the vehicle being rented.
        /// </summary>
        public string VehicleId { get; private set; } = vehicleId;

        /// <summary>
        /// Gets the unique identifier for the person renting the vehicle.
        /// </summary>
        public string PersonName { get; private set; } = personName;

        /// <summary>
        /// Gets the start date of the rental.
        /// </summary>
        public DateTime StartDate { get; private set; } = startDate;

        /// <summary>
        /// Gets the end date of the rental, if it has ended.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the rental is currently active.
        /// </summary>
        public bool IsActive => EndDate is null;

        /// <summary>
        /// Factory method to create a new Rental.
        /// </summary>
        /// <param name="vehicleId">The brand of the vehicle.</param>
        /// <param name="personName">The model of the vehicle.</param>
        /// <returns>A new <see cref="Rental"/> instance.</returns>
        public static Rental Create(string vehicleId, string personName)
        {
            ValidateCreate(vehicleId, personName);

            return new Rental(Guid.NewGuid().ToString(), vehicleId, personName, DateTime.UtcNow);
        }

        /// <summary>
        /// Factory method to create a Rental from persistence.
        /// </summary>
        /// <param name="id">The unique identifier for the rental.</param>
        /// <param name="vehicleId">The unique identifier for the vehicle being rented.</param>
        /// <param name="personName">The unique identifier for the person renting the vehicle.</param>
        /// <param name="startDate">The start date of the rental.</param>
        /// <param name="endDate">The end date of the rental, if it has ended.</param>
        /// <returns>A <see cref="Rental"/> instance created from persistence.</returns>
        public static Rental CreateFromPersistence(
            string id, string vehicleId, string personName, DateTime startDate, DateTime? endDate)
        {
            var rental = new Rental(id, vehicleId, personName, startDate)
            {
                EndDate = endDate
            };

            return rental;
        }

        /// <summary>
        /// Ends the rental at the specified end date.
        /// </summary>
        public void End()
        {
            EndDate = DateTime.UtcNow;
        }

        private static void ValidateCreate(string vehicleId, string personName)
        {
            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                throw new DomainException("VehicleId is required.");
            }

            if (string.IsNullOrWhiteSpace(personName))
            {
                throw new DomainException("PersonName is required.");
            }
        }
    }
}
