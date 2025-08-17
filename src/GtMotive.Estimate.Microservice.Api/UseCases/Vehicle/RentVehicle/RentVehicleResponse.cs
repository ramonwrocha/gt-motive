using System;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.RentVehicle
{
    public class RentVehicleResponse
    {
        public string RentalId { get; set; }

        public string VehicleId { get; set; }

        public string PersonId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
