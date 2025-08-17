namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Input data required to create a new vehicle.
    /// </summary>
    /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
    public class CreateVehicleInput(int manufactureYear) : IUseCaseInput
    {
        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        public int ManufactureYear { get; } = manufactureYear;
    }
}
