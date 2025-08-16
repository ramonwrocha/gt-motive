using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces
{
    /// <summary>
    /// Provides methods to manage and query vehicles in the repository.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(Vehicle vehicle);

        /// <summary>
        /// Gets all available vehicles from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of available vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetAvailableVehicles();
    }
}
