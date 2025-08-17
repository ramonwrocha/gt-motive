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
                brand: input.Brand,
                model: input.Model,
                manufactureYear: input.ManufactureYear,
                licensePlate: input.LicensePlate);

            await vehicleRepository.Add(vehicle);

            outputPort.StandardHandle(response: new CreateVehicleOutput(
                    vehicleId: vehicle.Id,
                    brand: vehicle.Brand,
                    model: vehicle.Model,
                    manufactureYear: vehicle.ManufactureYear,
                    licensePlate: vehicle.LicensePlate));
        }
    }
}
