using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle
{
    public sealed class CreateVehicleMapper : Profile
    {
        public CreateVehicleMapper()
        {
            CreateMap<CreateVehicleRequest, CreateVehicleCommand>();
        }
    }
}
