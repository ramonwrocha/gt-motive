namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Output data for the CreateVehicle use case.
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle.</param>
    /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
    public class CreateVehicleOutput(string vehicleId, int manufactureYear) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        public string VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        public int ManufactureYear { get; } = manufactureYear;
    }
}
