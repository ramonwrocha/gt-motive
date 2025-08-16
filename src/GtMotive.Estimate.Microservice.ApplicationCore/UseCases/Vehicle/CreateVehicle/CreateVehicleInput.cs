namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Input data required to create a new vehicle.
    /// </summary>
    /// <param name="brand">The brand of the vehicle.</param>
    /// <param name="model">The model of the vehicle.</param>
    /// <param name="manufactureYear">The manufacture year of the vehicle.</param>
    /// <param name="licensePlate">The license plate of the vehicle.</param>
    public class CreateVehicleInput(string brand, string model, int manufactureYear, string licensePlate) : IUseCaseInput
    {
        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        public string Brand { get; } = brand;

        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        public string Model { get; } = model;

        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        public int ManufactureYear { get; } = manufactureYear;

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;
    }
}
