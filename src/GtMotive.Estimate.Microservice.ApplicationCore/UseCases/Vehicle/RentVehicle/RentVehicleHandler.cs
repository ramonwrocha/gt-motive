using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle
{
    /// <summary>
    /// Handles the RentVehicleCommand and coordinates the vehicle rental process.
    /// </summary>
    public class RentVehicleHandler(
        IRentVehicleUseCase useCase, IRentVehicleOutputPort outputPort) : IRequestHandler<RentVehicleCommand, IWebApiPresenter>
    {
        /// <inheritdoc />
        public async Task<IWebApiPresenter> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new RentVehicleInput(request.PersonName, request.VehicleId);
            await useCase.Execute(input);
            return (IWebApiPresenter)outputPort;
        }
    }
}
