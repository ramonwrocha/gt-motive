using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles.Models
{
    /// <summary>
    /// Vehicle item view model for API response.
    /// </summary>
    public sealed class VehicleItemModel(string vehicleId, string brand, string model, int manufactureYear, string licensePlate, string status)
    {
        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        [Required]
        public string VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        [Required]
        public string Brand { get; } = brand;

        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        [Required]
        public string Model { get; } = model;

        /// <summary>
        /// Gets the manufacture year of the vehicle.
        /// </summary>
        [Required]
        public int ManufactureYear { get; } = manufactureYear;

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        [Required]
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the current status of the vehicle.
        /// </summary>
        [Required]
        public string Status { get; } = status;
    }
}
