using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<ICreateVehicleOutputPort>(provider => provider.GetRequiredService<CreateVehiclePresenter>());

            services.AddScoped<ListAvailableVehiclesPresenter>();
            services.AddScoped<IListAvailableVehiclesOutputPort>(provider => provider.GetRequiredService<ListAvailableVehiclesPresenter>());

            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<IRentVehicleOutputPort>(provider => provider.GetRequiredService<RentVehiclePresenter>());

            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<IReturnVehicleOutputPort>(provider => provider.GetRequiredService<ReturnVehiclePresenter>());

            return services;
        }
    }
}
