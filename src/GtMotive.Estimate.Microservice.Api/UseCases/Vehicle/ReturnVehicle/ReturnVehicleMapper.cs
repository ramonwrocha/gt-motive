using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ReturnVehicle
{
    public class ReturnVehicleMapper : Profile
    {
        public ReturnVehicleMapper()
        {
            CreateMap<ReturnVehicleRequest, ReturnVehicleCommand>();
        }
    }
}
