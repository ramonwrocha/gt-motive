using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle
{
    /// <summary>
    /// Represents the output details of a vehicle rental operation.
    /// </summary>
    public class RentVehicleOutput(string rentalId, string vehicleId, string personName, DateTime startDate) : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets the unique identifier of the rental.
        /// </summary>
        public string RentalId { get; set; } = rentalId;

        /// <summary>
        /// Gets or sets the unique identifier of the rented vehicle.
        /// </summary>
        public string VehicleId { get; set; } = vehicleId;

        /// <summary>
        /// Gets or sets the unique identifier of the person renting the vehicle.
        /// </summary>
        public string PersonName { get; set; } = personName;

        /// <summary>
        /// Gets or sets the start date of the rental period.
        /// </summary>
        public DateTime StartDate { get; set; } = startDate;
    }
}
