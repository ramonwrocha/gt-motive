using System;
using System.Linq;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles.Models;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles
{
    public sealed class ListAvailableVehiclesPresenter : IWebApiPresenter, IListAvailableVehiclesOutputPort
    {
        public IActionResult ActionResult { get; private set; } = new NoContentResult();

        public void StandardHandle(ListAvailableVehiclesOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var apiResponse = new ListAvailableVehiclesResponse(
                response.Vehicles.Select(vehicle => new VehicleItemModel(
                    vehicle.Id,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.ManufactureYear,
                    vehicle.LicensePlate,
                    vehicle.Status.Value)));

            ActionResult = new OkObjectResult(apiResponse);
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }

        public void ValidationError(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }
    }
}
