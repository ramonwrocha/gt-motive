using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    public sealed class CreateVehicleRequest
    {
        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        [JsonRequired]
        public int ManufactureYear { get; set; }

        [Required]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
