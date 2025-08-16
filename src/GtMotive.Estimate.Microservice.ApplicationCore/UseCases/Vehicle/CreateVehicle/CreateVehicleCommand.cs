using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Command to create a new vehicle with the specified brand, model, manufacture year, and license plate.
    /// </summary>
    public sealed record CreateVehicleCommand(
        string Brand,
        string Model,
        int ManufactureYear,
        string LicensePlate) : IRequest
    {
        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        public string Brand { get; init; } = Brand;

        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        public string Model { get; init; } = Model;

        /// <summary>
        /// Gets the year the vehicle was manufactured.
        /// </summary>
        public int ManufactureYear { get; init; } = ManufactureYear;

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; init; } = LicensePlate;
    }
}
