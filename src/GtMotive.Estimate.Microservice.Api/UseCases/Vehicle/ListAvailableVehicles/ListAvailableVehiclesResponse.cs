using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles.Models;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles
{
    /// <summary>
    /// Response model for listing available vehicles.
    /// </summary>
    public sealed class ListAvailableVehiclesResponse(IEnumerable<VehicleItemModel> vehicles)
    {
        /// <summary>
        /// Gets the collection of available vehicles.
        /// </summary>
        [Required]
        public IEnumerable<VehicleItemModel> Vehicles { get; } = vehicles;
    }
}
