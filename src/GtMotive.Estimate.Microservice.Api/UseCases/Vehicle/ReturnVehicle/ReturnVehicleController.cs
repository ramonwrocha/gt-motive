using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ReturnVehicle
{
    [ApiController]
    [Route("api/rentals/return")]
    public class ReturnVehicleController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleRequest request)
        {
            var command = mapper.Map<ReturnVehicleCommand>(request);
            var presenter = await mediator.Send(command);
            return presenter.ActionResult;
        }
    }
}
