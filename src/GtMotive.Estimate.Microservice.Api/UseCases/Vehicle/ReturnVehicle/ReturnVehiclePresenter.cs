using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ReturnVehicle
{
    public class ReturnVehiclePresenter : IWebApiPresenter, IReturnVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; } = new NoContentResult();

        public void StandardHandle(ReturnVehicleOutput output)
        {
            ActionResult = new OkObjectResult(output);
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }

        public void AlreadyReturned(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }

        public void ValidationError(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }
    }
}
