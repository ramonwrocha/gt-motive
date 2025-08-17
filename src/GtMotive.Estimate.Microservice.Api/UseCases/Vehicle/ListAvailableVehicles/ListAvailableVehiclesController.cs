using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles
{
    [ApiController]
    [Route("api/vehicles/available")]
    public class ListAvailableVehiclesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListAvailableVehicles()
        {
            var vehicles = await mediator.Send(new ListAvailableVehiclesQuery());
            return vehicles.ActionResult;
        }
    }
}
