using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Handles the creation of a vehicle by processing the <see cref="CreateVehicleCommand"/>.
    /// </summary>
    /// <param name="useCase">The use case to execute vehicle creation.</param>
    /// <param name="outputPort">The output port to handle the result of the vehicle creation process.</param>
    public class CreateVehicleHandler(
        ICreateVehicleUseCase useCase,
        ICreateVehicleOutputPort outputPort) : IRequestHandler<CreateVehicleCommand, IWebApiPresenter>
    {
        /// <summary>
        /// Handles the specified <see cref="CreateVehicleCommand"/> to create a vehicle.
        /// </summary>
        /// <param name="request">The command containing vehicle creation data.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<IWebApiPresenter> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(
                request.ManufactureYear);

            await useCase.Execute(input);
            return (IWebApiPresenter)outputPort;
        }
    }
}
