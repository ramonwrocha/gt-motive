namespace GtMotive.Estimate.Microservice.Domain.Vehicles.ValueObjects
{
    /// <summary>
    /// Represents the status of a vehicle.
    /// </summary>
    public readonly record struct VehicleStatus
    {
        private const string AvailableValue = "Available";
        private const string RentedValue = "Rented";
        private const string MaintenanceValue = "Maintenance";

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleStatus"/> class.
        /// </summary>
        /// <param name="value">The status value of the vehicle.</param>
        public VehicleStatus(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the status value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets a <see cref="VehicleStatus"/> representing an available vehicle.
        /// </summary>
        public static VehicleStatus Available => new(AvailableValue);

        /// <summary>
        /// Gets a <see cref="VehicleStatus"/> representing a rented vehicle.
        /// </summary>
        public static VehicleStatus Rented => new(RentedValue);

        /// <summary>
        /// Gets a <see cref="VehicleStatus"/> representing a vehicle under maintenance.
        /// </summary>
        public static VehicleStatus Maintenance => new(MaintenanceValue);

        /// <summary>
        /// Returns the string representation of the vehicle status.
        /// </summary>
        /// <returns>The status value as a string.</returns>
        public override string ToString() => Value;
    }
}
