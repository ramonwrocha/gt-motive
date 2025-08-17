using System;
using System.Collections.Generic;
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
        /// <param name="personId">The identifier of the person.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the person has an active rental; otherwise, false.</returns>
        Task<bool> HasActiveRental(string personId);

        /// <summary>
        /// Creates a new rental for the specified person and vehicle.
        /// </summary>
        /// <param name="rental">The rental entity to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="Rental"/>.</returns>
        Task<Rental> CreateRental(Rental rental);

        /// <summary>
        /// Gets the rental by its identifier.
        /// </summary>
        /// <param name="rentalId">The identifier of the rental.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Rental"/> if found; otherwise, null.</returns>
        Task<Rental> GetById(string rentalId);

        /// <summary>
        /// Gets all active rentals.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of active <see cref="Rental"/> entities.</returns>
        Task<IEnumerable<Rental>> GetActiveRentals();

        /// <summary>
        /// Ends the rental with the specified identifier and sets the end date.
        /// </summary>
        /// <param name="rentalId">The identifier of the rental.</param>
        /// <param name="endDate">The date the rental ended.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task EndRental(string rentalId, DateTime endDate);
    }
}
