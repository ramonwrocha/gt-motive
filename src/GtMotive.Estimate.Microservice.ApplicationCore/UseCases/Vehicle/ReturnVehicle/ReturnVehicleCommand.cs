using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle
{
    /// <summary>
    /// Command to return a vehicle for a given rental.
    /// </summary>
    /// <param name="PersonName">The unique identifier of the person.</param>
    public sealed record ReturnVehicleCommand(string PersonName) : IRequest<IWebApiPresenter>
    {
    }
}
