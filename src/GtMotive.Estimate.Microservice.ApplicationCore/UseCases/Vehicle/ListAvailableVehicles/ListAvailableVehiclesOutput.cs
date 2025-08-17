using System.Collections.Generic;
using VehicleModel = GtMotive.Estimate.Microservice.Domain.Vehicles.Entities.Vehicle;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles
{
    /// <summary>
    /// Output for the use case that lists available vehicles.
    /// </summary>
    /// <param name="vehicles">The collection of available vehicles.</param>
    public class ListAvailableVehiclesOutput(IEnumerable<VehicleModel> vehicles) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the collection of available vehicles.
        /// </summary>
        public IEnumerable<VehicleModel> Vehicles { get; } = vehicles;
    }
}
