using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles
{
    /// <summary>
    /// Query to list all available vehicles.
    /// </summary>
    public sealed record ListAvailableVehiclesQuery : IRequest<IWebApiPresenter>
    {
    }
}
