namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle
{
    /// <summary>
    /// Represents the input data required to return a rented vehicle.
    /// </summary>
    public class ReturnVehicleInput(string personName) : IUseCaseInput
    {
        /// <summary>
        /// Gets the identifier of the person returning the vehicle.
        /// </summary>
        public string PersonName { get; init; } = personName;
    }
}
