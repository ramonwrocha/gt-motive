using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleOutput"/> class.
    /// </summary>
    /// <param name="rentalId">The unique identifier of the rental transaction.</param>
    /// <param name="vehicleId">The unique identifier of the returned vehicle.</param>
    /// <param name="personName">The unique identifier of the person who rented the vehicle.</param>
    /// <param name="startDate">The start date of the rental period.</param>
    /// <param name="endDate">The end date of the rental period when the vehicle was returned.</param>
    public class ReturnVehicleOutput(string rentalId, string vehicleId, string personName, DateTime startDate, DateTime endDate) : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets the unique identifier of the rental transaction.
        /// </summary>
        public string RentalId { get; set; } = rentalId;

        /// <summary>
        /// Gets or sets the unique identifier of the returned vehicle.
        /// </summary>
        public string VehicleId { get; set; } = vehicleId;

        /// <summary>
        /// Gets or sets the unique identifier of the person who rented the vehicle.
        /// </summary>
        public string PersonName { get; set; } = personName;

        /// <summary>
        /// Gets or sets the start date of the rental period.
        /// </summary>
        public DateTime StartDate { get; set; } = startDate;

        /// <summary>
        /// Gets or sets the end date of the rental period when the vehicle was returned.
        /// </summary>
        public DateTime EndDate { get; set; } = endDate;
    }
}
