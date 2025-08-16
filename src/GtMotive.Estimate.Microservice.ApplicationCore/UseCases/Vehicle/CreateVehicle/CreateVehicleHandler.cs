using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Handles the creation of a vehicle by processing the <see cref="CreateVehicleCommand"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The use case to execute vehicle creation.</param>
    public class CreateVehicleHandler(CreateVehicleUseCase useCase) : IRequestHandler<CreateVehicleCommand>
    {
        /// <summary>
        /// Handles the specified <see cref="CreateVehicleCommand"/> to create a vehicle.
        /// </summary>
        /// <param name="request">The command containing vehicle creation data.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(
                request.Brand,
                request.Model,
                request.ManufactureYear,
                request.LicensePlate);

            await useCase.Execute(input);
        }
    }
}
