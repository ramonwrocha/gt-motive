using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateVehicleController(IMediator mediator, CreateVehiclePresenter presenter, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            var command = mapper.Map<CreateVehicleCommand>(request);
            await mediator.Send(command);
            return presenter.ActionResult;
        }
    }
}
