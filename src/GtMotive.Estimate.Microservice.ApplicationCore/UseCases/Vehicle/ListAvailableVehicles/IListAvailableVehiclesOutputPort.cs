namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles
{
    /// <summary>
    /// Output port for ListAvailableVehicles use case.
    /// </summary>
    public interface IListAvailableVehiclesOutputPort : IOutputPortStandard<ListAvailableVehiclesOutput>, IOutputPortNotFound
    {
    }
}
