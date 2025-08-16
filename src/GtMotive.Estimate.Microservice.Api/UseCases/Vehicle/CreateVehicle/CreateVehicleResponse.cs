namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    public class CreateVehicleResponse
    {
        public string VehicleId { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int ManufactureYear { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
