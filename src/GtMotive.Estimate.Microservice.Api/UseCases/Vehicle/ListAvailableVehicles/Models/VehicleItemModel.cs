using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles.Models
{
    /// <summary>
    /// Vehicle item view model for API response.
    /// </summary>
    public sealed class VehicleItemModel(string vehicleId, int manufactureYear, string status)
    {
        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        [Required]
        public string VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        [Required]
        public int ManufactureYear { get; } = manufactureYear;

        /// <summary>
        /// Gets the current status of the vehicle.
        /// </summary>
        [Required]
        public string Status { get; } = status;
    }
}
