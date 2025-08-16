namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle
{
    /// <summary>
    /// Output port for CreateVehicle use case.
    /// </summary>
    public interface ICreateVehicleOutputPort : IOutputPortStandard<CreateVehicleOutput>
    {
        /// <summary>
        /// Invalid manufacture year.
        /// </summary>
        /// <param name="message">Error message.</param>
        void InvalidManufactureYear(string message);
    }
}
