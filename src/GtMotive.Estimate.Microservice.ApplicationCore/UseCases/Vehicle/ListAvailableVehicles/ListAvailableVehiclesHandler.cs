using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles
{
    /// <summary>
    /// Handles the listing of available vehicles by processing the <see cref="ListAvailableVehiclesQuery"/>.
    /// </summary>
    /// <param name="useCase">The use case to execute vehicle listing.</param>
    /// <param name="outputPort">The presenter to handle the output.</param>
    public class ListAvailableVehiclesHandler(
        IListAvailableVehiclesUseCase useCase,
        IListAvailableVehiclesOutputPort outputPort)
        : IRequestHandler<ListAvailableVehiclesQuery, IWebApiPresenter>
    {
        /// <summary>
        /// Handles the specified <see cref="ListAvailableVehiclesQuery"/> to list available vehicles.
        /// </summary>
        /// <param name="request">The query for listing available vehicles.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<IWebApiPresenter> Handle(ListAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ListAvailableVehiclesInput();
            await useCase.Execute(input);
            return (IWebApiPresenter)outputPort;
        }
    }
}
