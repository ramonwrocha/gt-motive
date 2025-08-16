using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; } = new NoContentResult();

        public void StandardHandle(CreateVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new CreatedResult($"/api/vehicles/{response.VehicleId}", new
            {
                response.VehicleId,
                response.Brand,
                response.Model,
                response.ManufactureYear,
                response.LicensePlate,
                Status = "Available"
            });
        }

        public void InvalidManufactureYear(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }

        public void ValidationError(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }
    }
}
