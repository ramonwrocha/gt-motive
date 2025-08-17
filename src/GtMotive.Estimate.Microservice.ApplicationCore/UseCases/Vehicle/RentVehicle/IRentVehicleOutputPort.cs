namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.RentVehicle
{
    /// <summary>
    /// Defines the output port for the Rent Vehicle use case.
    /// </summary>
    public interface IRentVehicleOutputPort : IOutputPortStandard<RentVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Notifies that the user already has an active rental and cannot rent another vehicle.
        /// </summary>
        /// <param name="message">The message describing the active rental situation.</param>
        void AlreadyHasActiveRental(string message);
    }
}
