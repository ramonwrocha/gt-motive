using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle
{
    /// <summary>
    /// Handles the command to return a vehicle, executing the use case and returning the appropriate presenter.
    /// </summary>
    public class ReturnVehicleHandler(
        IReturnVehicleUseCase useCase,
        IReturnVehicleOutputPort outputPort) : IRequestHandler<ReturnVehicleCommand, IWebApiPresenter>
    {
        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ReturnVehicleInput(request.PersonName);
            await useCase.Execute(input);
            return (IWebApiPresenter)outputPort;
        }
    }
}
