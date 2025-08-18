using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Rentals.Entities;
using GtMotive.Estimate.Microservice.Domain.Rentals.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle to a person.
    /// </summary>
    public class RentVehicleUseCase(
        IRentalRepository rentalRepository,
        IVehicleRepository vehicleRepository,
        IRentVehicleOutputPort outputPort) : IRentVehicleUseCase
    {
        /// <summary>
        /// Executes the vehicle rental process for the specified input.
        /// </summary>
        /// <param name="input">The input containing person and vehicle identifiers.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var hasActiveRental = await rentalRepository.HasActiveRental(input.PersonName);
            if (hasActiveRental)
            {
                outputPort.AlreadyHasActiveRental("La persona ya tiene un vehículo alquilado.");
                return;
            }

            var vehicle = await vehicleRepository.GetByIdAsync(input.VehicleId);
            if (vehicle is null or { IsAvailable: false })
            {
                outputPort.NotFoundHandle("Vehículo no disponible.");
                return;
            }

            vehicle.Rent();
            await vehicleRepository.UpdateAsync(vehicle);

            var request = Rental.Create(vehicleId: input.VehicleId, personName: input.PersonName);
            var rental = await rentalRepository.CreateRental(request);

            outputPort.StandardHandle(response: BuildOutput(rental));
        }

        private static RentVehicleOutput BuildOutput(Rental rental)
        {
            return new RentVehicleOutput(
                rental.Id,
                rental.VehicleId,
                rental.PersonName,
                rental.StartDate);
        }
    }
}
