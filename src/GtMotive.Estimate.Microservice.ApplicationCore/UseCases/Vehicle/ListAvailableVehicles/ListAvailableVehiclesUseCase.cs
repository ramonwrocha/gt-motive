using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles
{
    /// <summary>
    /// Lists all available vehicles from the fleet.
    /// </summary>
    /// <param name="outputPort">Output port for results.</param>
    /// <param name="vehicleRepository">Repository for vehicle operations.</param>
    public sealed class ListAvailableVehiclesUseCase(
        IListAvailableVehiclesOutputPort outputPort,
        IVehicleRepository vehicleRepository) : IListAvailableVehiclesUseCase
    {
        /// <summary>
        /// Executes the list available vehicles use case.
        /// </summary>
        /// <param name="input">The input data (empty for this query).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Execute(ListAvailableVehiclesInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var availableVehicles = await vehicleRepository.GetAvailableVehiclesAsync();

            if (!availableVehicles.Any())
            {
                outputPort.NotFoundHandle("No available vehicles found in the fleet.");
                return;
            }

            outputPort.StandardHandle(response: new ListAvailableVehiclesOutput(availableVehicles));
        }
    }
}
