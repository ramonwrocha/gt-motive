using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Abstractions.Interfaces
{
    /// <summary>
    /// Defines a presenter that exposes an <see cref="IActionResult"/> for Web API responses.
    /// </summary>
    public interface IWebApiPresenter
    {
        /// <summary>
        /// Gets the <see cref="IActionResult"/> representing the result of the operation.
        /// </summary>
        IActionResult ActionResult { get; }
    }
}
