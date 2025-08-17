using System;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.RentVehicle
{
    public class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; } = new NoContentResult();

        public void StandardHandle(RentVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new RentVehicleResponse
            {
                RentalId = output.RentalId,
                VehicleId = output.VehicleId,
                PersonName = output.PersonName,
                StartDate = output.StartDate
            };

            ActionResult = new OkObjectResult(response);
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }

        public void AlreadyHasActiveRental(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }

        public void ValidationError(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }
    }
}
