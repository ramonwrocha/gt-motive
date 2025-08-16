using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Domain.Common.Interfaces
{
    /// <summary>
    /// Provides a builder for configuring infrastructure services.
    /// </summary>
    public interface IInfrastructureBuilder
    {
        /// <summary>
        /// Gets the collection of service descriptors for dependency injection.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
