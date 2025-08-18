using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="outputPort">Output port for results.</param>
    /// <param name="vehicleRepository">Repository for vehicle persistence operations.</param>
    public sealed class CreateVehicleUseCase(
        ICreateVehicleOutputPort outputPort,
        IVehicleRepository vehicleRepository) : ICreateVehicleUseCase
    {
        /// <summary>
        /// Executes the vehicle creation use case with the provided input.
        /// </summary>
        /// <param name="input">The input data required to create a vehicle.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicle = Domain.Vehicles.Entities.Vehicle.Create(
                manufactureYear: input.ManufactureYear);

            await vehicleRepository.AddAsync(vehicle);

            outputPort.StandardHandle(response: BuildOutput(vehicle));
        }

        private static CreateVehicleOutput BuildOutput(Domain.Vehicles.Entities.Vehicle vehicle)
        {
            return new CreateVehicleOutput(
                vehicleId: vehicle.Id,
                manufactureYear: vehicle.ManufactureYear);
        }
    }
}
