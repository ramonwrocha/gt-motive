using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Rentals.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Rentals.Interfaces
{
    /// <summary>
    /// Provides methods to manage rental entities, including creation, retrieval, and termination of rentals.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Determines whether the specified person has an active rental.
        /// </summary>
        /// <param name="personName">The identifier of the person.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the person has an active rental; otherwise, false.</returns>
        Task<bool> HasActiveRental(string personName);

        /// <summary>
        /// Creates a new rental for the specified person and vehicle.
        /// </summary>
        /// <param name="rental">The rental entity to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="Rental"/>.</returns>
        Task<Rental> CreateRental(Rental rental);

        /// <summary>
        /// Gets the rental by its identifier.
        /// </summary>
        /// <param name="personName">The identifier of the person.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Rental"/> if found; otherwise, null.</returns>
        Task<Rental> GetPendingReturn(string personName);

        /// <summary>
        /// Updates the specified rental entity in the repository.
        /// </summary>
        /// <param name="rental">The rental entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Update(Rental rental);
    }
}
