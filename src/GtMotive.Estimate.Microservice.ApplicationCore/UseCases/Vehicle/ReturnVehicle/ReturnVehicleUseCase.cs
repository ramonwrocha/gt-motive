using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Rentals.Entities;
using GtMotive.Estimate.Microservice.Domain.Rentals.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
    /// </summary>
    /// <param name="rentalRepository">The rental repository.</param>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="outputPort">The output port for handling responses.</param>
    public class ReturnVehicleUseCase(
        IRentalRepository rentalRepository,
        IVehicleRepository vehicleRepository,
        IReturnVehicleOutputPort outputPort) : IReturnVehicleUseCase
    {
        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var rental = await rentalRepository.GetPendingReturn(personName: input.PersonName);
            if (rental is null)
            {
                outputPort.NotFoundHandle("Alquiler no encontrado.");
                return;
            }

            if (!rental.IsActive)
            {
                outputPort.AlreadyReturned("El vehículo ya fue devuelto.");
                return;
            }

            rental.End();
            await rentalRepository.Update(rental);

            var vehicle = await vehicleRepository.GetById(rental.VehicleId);
            vehicle?.Return();

            if (vehicle is not null)
            {
                await vehicleRepository.Update(vehicle);
            }

            outputPort.StandardHandle(BuildOutput(rental));
        }

        private static ReturnVehicleOutput BuildOutput(Rental rental)
        {
            return new ReturnVehicleOutput(
                rental.Id,
                rental.VehicleId,
                rental.PersonName,
                rental.StartDate,
                rental.EndDate!.Value);
        }
    }
}
