namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    public class CreateVehicleResponse
    {
        public string VehicleId { get; set; } = string.Empty;

        public int ManufactureYear { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
