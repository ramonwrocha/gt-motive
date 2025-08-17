using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Command to create a new vehicle with the specified manufacture year and license plate.
    /// </summary>
    public sealed record CreateVehicleCommand(int ManufactureYear) : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets the year the vehicle was manufactured.
        /// </summary>
        public int ManufactureYear { get; init; } = ManufactureYear;
    }
}
