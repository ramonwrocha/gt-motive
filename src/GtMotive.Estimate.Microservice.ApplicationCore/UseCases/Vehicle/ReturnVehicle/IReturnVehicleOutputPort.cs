namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ReturnVehicle
{
    /// <summary>
    /// Output port interface for the ReturnVehicle use case.
    /// </summary>
    public interface IReturnVehicleOutputPort : IOutputPortStandard<ReturnVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Handles the scenario when the vehicle has already been returned.
        /// </summary>
        /// <param name="message">A message describing the already returned state.</param>
        void AlreadyReturned(string message);
    }
}
