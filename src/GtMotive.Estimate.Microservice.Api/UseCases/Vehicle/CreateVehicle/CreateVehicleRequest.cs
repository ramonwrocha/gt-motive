using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    public sealed class CreateVehicleRequest
    {
        [Required]
        [JsonRequired]
        public int ManufactureYear { get; set; }
    }
}
