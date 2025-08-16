using System;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Abstraction for implementing resilience strategies.
    /// </summary>
    public interface IResilienceService
    {
        /// <summary>
        /// Executes the specified asynchronous operation with resilience strategies applied and returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the result returned by the operation.</typeparam>
        /// <param name="operation">The asynchronous operation to execute.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result of type <typeparamref name="T"/>.</returns>
        Task<T> ExecuteAsync<T>(Func<Task<T>> operation);

        /// <summary>
        /// Executes the specified asynchronous operation with resilience strategies applied.
        /// </summary>
        /// <param name="operation">The asynchronous operation to execute.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ExecuteAsync(Func<Task> operation);
    }
}
