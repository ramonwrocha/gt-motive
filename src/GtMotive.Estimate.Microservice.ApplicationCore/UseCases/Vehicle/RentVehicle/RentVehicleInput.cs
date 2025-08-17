namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle
{
    /// <summary>
    /// Input data required to rent a vehicle.
    /// </summary>
    public class RentVehicleInput(string personName, string vehicleId) : IUseCaseInput
    {
        /// <summary>
        /// Gets the identifier of the person renting the vehicle.
        /// </summary>
        public string PersonName { get; init; } = personName;

        /// <summary>
        /// Gets the identifier of the vehicle to be rented.
        /// </summary>
        public string VehicleId { get; init; } = vehicleId;
    }
}
