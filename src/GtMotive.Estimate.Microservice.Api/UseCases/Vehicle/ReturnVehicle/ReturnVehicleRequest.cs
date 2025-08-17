using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ReturnVehicle
{
    public class ReturnVehicleRequest
    {
        [Required]
        public string PersonName { get; set; }
    }
}
