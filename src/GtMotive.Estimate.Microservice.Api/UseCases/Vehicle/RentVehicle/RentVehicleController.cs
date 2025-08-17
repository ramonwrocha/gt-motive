using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.RentVehicle
{
    [ApiController]
    [Route("api/rentals/rent")]
    public class RentVehicleController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] RentVehicleRequest request)
        {
            var command = mapper.Map<RentVehicleCommand>(request);
            var presenter = await mediator.Send(command);
            return presenter.ActionResult;
        }
    }
}
