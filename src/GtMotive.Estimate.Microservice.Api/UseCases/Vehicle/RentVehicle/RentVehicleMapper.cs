using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.RentVehicle
{
    public sealed class RentVehicleMapper : Profile
    {
        public RentVehicleMapper()
        {
            CreateMap<RentVehicleRequest, RentVehicleCommand>();
        }
    }
}
