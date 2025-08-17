using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle
{
    /// <summary>
    /// Command to request the rental of a vehicle by a person.
    /// </summary>
    /// <param name="PersonId">The identifier of the person renting the vehicle.</param>
    /// <param name="VehicleId">The identifier of the vehicle to be rented.</param>
    public sealed record RentVehicleCommand(
        string PersonId,
        string VehicleId) : IRequest<IWebApiPresenter>;
}
